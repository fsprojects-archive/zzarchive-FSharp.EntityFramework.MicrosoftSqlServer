namespace FSharp.Data.Entity

open System
open System.Reflection
open System.IO
open System.Data
open System.Data.SqlClient
open System.Collections.Generic

open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open Microsoft.Data.Entity

open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Quotations

open ProviderImplementation.ProvidedTypes

open Inflector

open FSharp.Data.Entity.SqlServer

[<AutoOpen; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module private ProvidedTypes = 
    let inline addCustomAttribute<'T, ^P when 'T :> Attribute and ^P : (member AddCustomAttribute : System.Reflection.CustomAttributeData -> unit)> (provided: ^P, ctorArgs: obj list, namedArgs: list<string * obj>) = 
        let attrData = { 
            new CustomAttributeData() with
                member __.Constructor = typeof<'T>.GetConstructor [| for value in ctorArgs -> value.GetType() |]
                member __.ConstructorArguments = upcast [| for value in ctorArgs -> CustomAttributeTypedArgument value |]
                member __.NamedArguments = 
                    upcast [| 
                        for propName, value in namedArgs do 
                            let property = typeof<'T>.GetProperty propName
                            yield CustomAttributeNamedArgument(property, value) 
                    |] 
        }
        (^P : (member AddCustomAttribute : System.Reflection.CustomAttributeData -> unit) (provided, attrData))

[<TypeProvider>]
type public SqlServerDbContextTypeProvider(config: TypeProviderConfig) as this = 
    inherit TypeProviderForNamespaces()

    let nameSpace = this.GetType().Namespace
    let assembly = Assembly.LoadFrom( config.RuntimeAssembly)
    let providerType = ProvidedTypeDefinition(assembly, nameSpace, "SqlServer", Some typeof<obj>, HideObjectMethods = true, IsErased = false)

    let getProvidedAssembly() = 
        let assemblyFileName = Path.ChangeExtension( Path.GetTempFileName(), "dll")
        ProvidedAssembly( assemblyFileName)

    let addToProvidedTempAssembly types = 
        getProvidedAssembly().AddTypes types

    let getAutoProperty(name: string, clrType) = 
        let backingField = ProvidedField(name.Camelize(), clrType)
        let property = ProvidedProperty(name, clrType)
        property.GetterCode <- fun args -> Expr.FieldGet( args.[0], backingField)
        property.SetterCode <- fun args -> Expr.FieldSet( args.[0], backingField, args.[1])
        property, backingField 

    let getAutoPropertyAsList(name, clrType): MemberInfo list = 
        let p, f = getAutoProperty(name, clrType)
        [ p; f ]
                                
    let hardToMapDatatypes = 
        set [
            "binary"; "image"; "timestamp"; "varbinary"
            "char"; "nchar"; "ntext"; "text"; "varchar"
            "money"; "decimal"; "numeric"; "smallmoney"
            "date"; "time"; "datetime2"; "datetimeoffset"; "datetime"; "smalldatetime"
        ]
    let typesToSpecifyScale = set [ "time"; "datetime2"; "datetimeoffset" ]

    do
        addToProvidedTempAssembly [ providerType ]

    do 
        providerType.DefineStaticParameters(
            parameters = [ 
                ProvidedStaticParameter("ConnectionString", typeof<string>) 
                ProvidedStaticParameter("Pluralize", typeof<bool>, false) 
                ProvidedStaticParameter("SuppressForeignKeyProperties", typeof<bool>, false) 
            ],             
            instantiationFunction = (fun typeName args ->   
                this.CreateDbContextType(typeName, unbox args.[0], unbox args.[1], unbox args.[2])
            )        
        )

        this.AddNamespace( nameSpace, [ providerType ])

    override __.ResolveAssembly args =
        let missing = AssemblyName(args.Name)

        config.ReferencedAssemblies
        |> Seq.tryPick(fun assemblyFile ->
            let reference = Assembly.ReflectionOnlyLoadFrom( assemblyFile).GetName()
            if AssemblyName.ReferenceMatchesDefinition(reference, missing)
            then assemblyFile |> Assembly.LoadFrom |> Some
            else None
        )
        |> defaultArg <| base.ResolveAssembly( args)

    member internal this.CreateDbContextType( typeName, connectionString, pluralize, suppressForeignKeyProperties) = 
        let dbContextType = ProvidedTypeDefinition(assembly, nameSpace, typeName, baseType = Some typeof<DbContext>, HideObjectMethods = true, IsErased = false)

        do
            addToProvidedTempAssembly [ dbContextType ]

        do 
            dbContextType.AddMembersDelayed <| fun() ->
                [
                    let parameterlessCtor = ProvidedConstructor([], InvokeCode = fun _ -> <@@ () @@>)
                    parameterlessCtor.BaseConstructorCall <- fun args -> 
                        let baseCtor = typeof<DbContext>.GetConstructor(BindingFlags.Instance ||| BindingFlags.NonPublic, null, [||], null) 
                        baseCtor, args
                    yield parameterlessCtor

                    for baseCtor in typeof<DbContext>.GetConstructors() do
                        let paremeters = [ for p in baseCtor.GetParameters() -> ProvidedParameter(p.Name, p.ParameterType) ]
                        let ctor = ProvidedConstructor (paremeters, InvokeCode = fun _ -> <@@ () @@>)
                        ctor.BaseConstructorCall <- fun args -> baseCtor, args
                        yield ctor
                ]

        this.AddEntityTypesAndDataSets(dbContextType, connectionString, pluralize, suppressForeignKeyProperties)

        do 
            dbContextType.AddMembersDelayed <| fun() ->
                [
                    let name = "OnConfiguring"
                    let field = ProvidedField(name.Camelize(), typeof<DbContextOptionsBuilder -> unit>)
                    yield field :> MemberInfo

                    let property = ProvidedProperty(name, field.FieldType)
                    property.SetterCode <- fun args -> Expr.FieldSet(args.[0], field, args.[1])
                    yield upcast property

                    let vTableHandle = typeof<DbContext>.GetMethod(name, BindingFlags.Instance ||| BindingFlags.NonPublic)
                    let impl = ProvidedMethod(vTableHandle.Name, [ ProvidedParameter("optionsBuilder", typeof<DbContextOptionsBuilder>) ], typeof<Void>)
                    impl.SetMethodAttrs(vTableHandle.Attributes ||| MethodAttributes.Virtual)
                    yield upcast  impl
                    impl.InvokeCode <- fun args -> 
                        <@@ 
                            let configuring = %%Expr.FieldGet(args.Head, field)
                            let optionsBuilder: DbContextOptionsBuilder = %%args.[1]
                            optionsBuilder.UseSqlServer(connectionString: string) |> ignore
                            if box configuring <> null
                            then configuring optionsBuilder
                        @@>
                    dbContextType.DefineMethodOverride(impl, vTableHandle)
                ]

        do 
            dbContextType.AddMembersDelayed <| fun() ->
                [
                    let name = "OnModelCreating"
                    let field = ProvidedField(name.Camelize(), typeof<ModelBuilder -> unit>)
                    yield field :> MemberInfo

                    let property = ProvidedProperty(name, field.FieldType)
                    property.SetterCode <- fun args -> Expr.FieldSet(args.[0], field, args.[1])
                    yield upcast property

                    let vTableHandle = typeof<DbContext>.GetMethod(name, BindingFlags.Instance ||| BindingFlags.NonPublic)
                    let impl = ProvidedMethod(vTableHandle.Name, [ ProvidedParameter("modelBuilder", typeof<ModelBuilder>) ], typeof<Void>)
                    impl.SetMethodAttrs(vTableHandle.Attributes ||| MethodAttributes.Virtual)
                    yield upcast impl
                    impl.InvokeCode <- fun args -> 
                        use conn = new SqlConnection( connectionString)
                        conn.Open()
                
                        let getDefaultValues = 
                            async {
                                use conn = new SqlConnection( connectionString)
                                do! conn.OpenAsync() |> Async.AwaitTask
                                return! conn.GetDefaultColumnValues()
                            }
                            |> Async.StartAsTask

                        let indeces = 
                            let elements = [ 
                                for index in conn.GetIndexes() do 
                                    let table = Expr.Value( index.Table.TwoPartName)
                                    let name = Expr.Value index.Name
                                    let isPrimaryKey = Expr.Value( index.IsPrimaryKey)
                                    let isUnique = Expr.Value( index.IsUnique)
                                    let columns = Expr.NewArray( typeof<string>, [ for col in index.Columns -> Expr.Value( col) ])
                                    yield Expr.NewTuple [ table; name; isPrimaryKey; isUnique; columns ] 
                            ]
                            Expr.NewArray(typeof<string * string * bool * bool * string[]>, elements)

                        let defaultColumnValues = 
                            let elements = 
                                getDefaultValues.Result
                                |> List.groupBy fst
                                |> List.map (fun (tbl, xs) -> 
                                    let twoPartsTableName = Expr.Value tbl.TwoPartName
                                    let columnsWithDeafult =  
                                        let elements = [ 
                                            for  _, (column: string, defaultValue: string) in xs do
                                                let x = ref defaultValue
                                                while x.Value.StartsWith("(") && x.Value.EndsWith(")") do
                                                    x := x.Value.Substring(1, x.Value.Length - 2)
                                                yield Expr.NewTuple [ Expr.Value column; Expr.Value( x.Value) ]
                                        ]
                                        Expr.NewArray( typeof<string * string>, elements)
                                    Expr.NewTuple [ twoPartsTableName; columnsWithDeafult ]
                                )
                            Expr.NewArray(typeof<string * (string * string)[]>, elements)
                    
                        <@@ 
                            let modelBuilder: ModelBuilder = %%args.[1]
                            let dbContext: DbContext = %%Expr.Coerce(args.[0], typeof<DbContext>)
                            let entityTypes = 
                                dbContext.GetType().GetNestedTypes() |> Array.filter (fun t -> t.IsDefined(typeof<TableAttribute>))

                            let indecesByTable = %%indeces |> Array.groupBy (fun (tableName, _, _, _, _) -> tableName) |> dict

                            let columnsWithDefaultValuebyTable = (%%defaultColumnValues: (string * (string * string)[])[]) |> dict

                            for t in entityTypes do
                                let e = modelBuilder.Entity(t)
                                let relational = e.Metadata.Relational()
                                let twoPartTableName = sprintf "%s.%s" relational.Schema relational.TableName

                                if indecesByTable.ContainsKey(twoPartTableName)
                                then 
                                    let indeces = indecesByTable.[twoPartTableName]
                                    for _, name, isPrimaryKey, isUnique, columns in indeces do
                                        if isPrimaryKey
                                        then e.HasKey( columns) |> ignore
                                        elif isUnique 
                                        then e.HasIndex(columns).HasName(name).IsUnique() |> ignore
                                        else e.HasIndex(columns).HasName(name) |> ignore
                                    
                                if columnsWithDefaultValuebyTable.ContainsKey(twoPartTableName)
                                then 
                                    for col, defaultValue in columnsWithDefaultValuebyTable.[twoPartTableName] do
                                        let p = e.Property( t.GetProperty(col).PropertyType, col)
                                        try 
                                            let value = Convert.ChangeType(defaultValue, p.Metadata.ClrType)
                                            p.ForSqlServerHasDefaultValue( value) |> ignore
                                        with _ ->
                                            p.ForSqlServerHasDefaultValueSql( defaultValue) |> ignore
                    
                            let modelCreating = %%Expr.FieldGet(args.[0], field)
                            if box modelCreating <> null
                            then 
                                modelCreating modelBuilder
                        @@>
                    dbContextType.DefineMethodOverride(impl, vTableHandle)
            ]
            
        dbContextType

    member internal this.AddEntityTypesAndDataSets(dbConTextType: ProvidedTypeDefinition, connectionString, pluralize, suppressForeignKeyProperties) = 
        dbConTextType.AddMembersDelayed <| fun () ->
            
            use conn = new SqlConnection( connectionString)
            conn.Open()

            let entityTypes = [

                for table in conn.GetTables() do   
                   
                    let twoPartTableName = table.TwoPartName
                    let tableType = ProvidedTypeDefinition( twoPartTableName, baseType = Some typeof<obj>, IsErased = false)
                    addCustomAttribute<TableAttribute, _>(tableType, [ table.Name ], [ "Schema", box table.Schema ])

                    do //Tables
                        let ctor = ProvidedConstructor([], InvokeCode = fun _ -> <@@ () @@>)
                        ctor.BaseConstructorCall <- fun args -> typeof<obj>.GetConstructor([||]), args
                        tableType.AddMember ctor

                    do 
                        tableType.AddMembersDelayed <| fun() -> 
                            [
                                use conn = new SqlConnection( connectionString)
                                conn.Open()
                                
                                for col in conn.GetColumns( table) do
                                    if not(unsupportedColumnTypes.Contains col.DataType)
                                    then 
                                        let prop, field = getAutoProperty( col.Name, col.ClrType)

                                        let requiredRefTypeColumn = not col.IsNullable && not col.ClrType.IsValueType
                                        if requiredRefTypeColumn
                                        then addCustomAttribute<RequiredAttribute, _>(prop, [], [])

                                        if col.DataType = "timestamp" || col.IsComputed
                                        then 
                                            addCustomAttribute<DatabaseGeneratedAttribute, _>(prop, [ DatabaseGeneratedOption.Computed ], [])

                                        let maybeMaxLength =
                                            match col.DataType with
                                            | "binary" | "varbinary" 
                                            | "char" | "text" | "varchar" -> Some col.MaxLength
                                            | "nchar" | "ntext" | "nvarchar" | "sysname"  -> Some( col.MaxLength / 2)
                                            | _ -> None
                                        
                                        maybeMaxLength |> Option.iter (fun maxLength ->
                                            addCustomAttribute<MaxLengthAttribute, _>(prop, [ maxLength ], [])
                                        )

                                        if hardToMapDatatypes.Contains col.DataType 
                                        then 
                                            let scale = 
                                                match col.NonDefaultScale, col.DataType with
                                                | Some x, "time" 
                                                | Some x, "datetime2"
                                                | Some x, "datetimeoffset" -> sprintf "(%i)" x
                                                | _ -> ""
                                            let typename = col.DataType + string scale 
                                            addCustomAttribute<ColumnAttribute, _>(prop, [], [ "TypeName", box typename ])
                                            
                                        yield prop :> MemberInfo
                                        yield upcast field

                                if not suppressForeignKeyProperties
                                then 
                                    for fk in conn.GetForeignKeys( table) do   
                                        let parent: ProvidedTypeDefinition = downcast dbConTextType.GetNestedType( fk.Parent.TwoPartName) 
                                        let prop, field = getAutoProperty( fk.Name, parent)
                                        let columns = fk.Columns |> String.concat ","
                                        addCustomAttribute<ForeignKeyAttribute, _>(prop, [ columns ], [])
                                        addCustomAttribute<InversePropertyAttribute, _>(prop, [ table.Name ], [])
                                        
                                        yield prop :> _
                                        yield field :> _
                                    
                                        parent.AddMembersDelayed <| fun () -> 
                                            let collectionType = ProvidedTypeBuilder.MakeGenericType(typedefof<_ List>, [ tableType ])
                                            let prop, field = getAutoProperty( table.Name, collectionType)
                                            addCustomAttribute<InversePropertyAttribute, _>(prop, [ fk.Name ], [])
                                            [ prop :> MemberInfo; field :> _ ]
                            ]

                    yield tableType
            ]
            
            do  
                addToProvidedTempAssembly entityTypes 

            let props = [
                for e in entityTypes do
                    let name = if pluralize then e.Name.Pluralize() else e.Name
                    let t = ProvidedTypeBuilder.MakeGenericType( typedefof<_ DbSet>, [ e ])
                    yield! getAutoPropertyAsList( name, t)
            ]

            [ for x in entityTypes -> x :> MemberInfo ] @ props

[<assembly:TypeProviderAssembly()>]
do()

