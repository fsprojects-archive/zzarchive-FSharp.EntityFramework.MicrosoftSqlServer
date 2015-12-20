[<AutoOpen>]
module internal FSharp.Data.Entity.SqlServer.InformationSchema

open System
open System.Data.SqlClient
open System.Data
open System.Collections.Generic
open ProviderImplementation.ProvidedTypes

let typeMapping = 
    dict [
        // exact numerics
        "bigint", lazy Type.GetType "System.Int64"
        "bit", lazy Type.GetType "System.Boolean" 
        "decimal", lazy Type.GetType "System.Decimal" 
        "int", lazy Type.GetType "System.Int32" 
        "money", lazy Type.GetType "System.Decimal" 
        "numeric", lazy Type.GetType "System.Decimal" 
        "smallint", lazy Type.GetType "System.Int16" 
        "smallmoney", lazy Type.GetType "System.Decimal" 
        "tinyint", lazy Type.GetType "System.Byte" 

        // approximate numerics
        "float", lazy Type.GetType "System.Double" // This is correct. SQL Server 'float' type maps to double
        "real", lazy Type.GetType "System.Single"

        // date and time
        "date", lazy Type.GetType "System.DateTime"
        "datetime", lazy Type.GetType "System.DateTime"
        "datetime2", lazy Type.GetType "System.DateTime"
        "datetimeoffset", lazy Type.GetType "System.DateTimeOffset"
        "smalldatetime", lazy Type.GetType "System.DateTime"
        "time", lazy Type.GetType "System.TimeSpan"

        // character strings
        "char", lazy Type.GetType "System.String"
        "text", lazy Type.GetType "System.String"
        "varchar", lazy Type.GetType "System.String"

        // unicode character strings
        "nchar", lazy Type.GetType "System.String"
        "ntext", lazy Type.GetType "System.String"
        "nvarchar", lazy Type.GetType "System.String"
        "sysname", lazy Type.GetType "System.String"

        // binary
        "binary", lazy Type.GetType "System.Byte[]" 
        "image", lazy Type.GetType "System.Byte[]" 
        "varbinary", lazy Type.GetType "System.Byte[]" 

        //spatial
        "geography", lazy Type.GetType("Microsoft.SqlServer.Types.SqlGeography, Microsoft.SqlServer.Types", throwOnError = true)
        "geometry", lazy Type.GetType("Microsoft.SqlServer.Types.SqlGeometry, Microsoft.SqlServer.Types", throwOnError = true) 

        //other
        "hierarchyid", lazy Type.GetType("Microsoft.SqlServer.Types.SqlHierarchyId, Microsoft.SqlServer.Types", throwOnError = true) 
        "sql_variant", lazy Type.GetType "System.Object" 

        "timestamp", lazy Type.GetType "System.Byte[]"  // note: rowversion is a synonym but SQL Server stores the data type as 'timestamp'
        "uniqueidentifier", lazy Type.GetType "System.Guid" 
        "xml", lazy Type.GetType "System.String"

        //TODO 
        //"cursor", typeof<TODO>
        //"table", typeof<TODO>
    ]

let unicodeTypes = HashSet [ "nchar"; "ntext"; "nvarchar"; "sysname"]
let unsupportedColumnTypes = HashSet [ "hierarchyid"; "sql_variant"; "geography"; "geometry"; "xml" ]

type Table = {
    Schema: string
    Name: string
}   
    with
    member this.TwoPartName = sprintf "%s.%s" this.Schema this.Name

type Column = {
    Name: string
    DataType: string
    IsNullable: bool
    IsIdentity: bool
    IsComputed: bool
    MaxLength: int
    NonDefaultScale: int option    
}   
    with
    member this.ClrType = 
        let t = typeMapping.[this.DataType].Value
        if this.IsNullable && t.IsValueType
        then ProvidedTypeBuilder.MakeGenericType(typedefof<_ Nullable>, [ t ])
        else t

type ForeignKey = {
    Name: string
    Columns: string[]
    Parent: Table 
}

type Index = {
    Name: string
    Table: Table
    Columns: string[] 
    IsPrimaryKey: bool
    IsUnique: bool
}

type SqlDataReader with
    member this.TryGetValue(name: string) = 
        let value = this.[name] 
        if Convert.IsDBNull value then None else Some(unbox<'a> value)

let private (?) (row: SqlDataReader) (name: string) = unbox row.[name]

type SqlConnection with 
    member this.Execute(query, ?parameters) = 
        assert(this.State = ConnectionState.Open)
        seq {
            use cmd = new SqlCommand(query, this)

            cmd.Parameters.AddRange [|
                let ps = defaultArg parameters []
                for name, value in ps do
                    yield SqlParameter(name, value = value) 
            |]
            
            use cursor = cmd.ExecuteReader()
            while cursor.Read() do
                yield cursor
        }

    member this.GetTables() = 
        let query = "
            SELECT TABLE_SCHEMA, TABLE_NAME
            FROM INFORMATION_SCHEMA.TABLES AS X
            WHERE TABLE_TYPE = 'BASE TABLE'

            EXCEPT --tables containing unsuporrted column types as part of primary key

            SELECT DISTINCT X.TABLE_SCHEMA, X.TABLE_NAME
            FROM 
	            INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS X
	            JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS Y ON 
		            X.CONSTRAINT_TYPE = 'PRIMARY KEY'
		            AND X.CONSTRAINT_SCHEMA = Y.CONSTRAINT_SCHEMA 
		            AND X.CONSTRAINT_NAME = Y.CONSTRAINT_NAME
	            JOIN INFORMATION_SCHEMA.COLUMNS AS Z ON
		            Y.TABLE_SCHEMA = Z.TABLE_SCHEMA
		            AND Y.TABLE_NAME = Z.TABLE_NAME
		            AND Y.COLUMN_NAME = Z.COLUMN_NAME
            WHERE Z.DATA_TYPE IN ('geography', 'geometry', 'hierarchyid', 'sql_variant', 'xml')
        "
        this.Execute( query) 
        |> Seq.map ( fun x -> { Schema = x ? TABLE_SCHEMA; Name = x ? TABLE_NAME }) 
        |> Seq.toArray

    member this.GetColumns(table: Table) = 
        let query = 
            sprintf "
                SELECT 
	                columns.name
					,types.name AS type_name
                    ,columns.is_nullable
	                ,columns.is_identity
	                ,columns.is_computed
	                ,columns.max_length
	                ,CAST( NULLIF( columns.scale, types.scale) AS INT) AS scale
                FROM
	                sys.schemas  
	                JOIN sys.tables ON schemas.schema_id = tables.schema_id
	                JOIN sys.columns ON columns.object_id = tables.object_id
	                JOIN sys.types ON columns.system_type_id = types.system_type_id 
	                	AND ((types.is_assembly_type = 1 AND columns.user_type_id = types.user_type_id) 
	                		OR columns.system_type_id = types.user_type_id)
                WHERE
                    schemas.name = '%s' AND tables.name = '%s'
                ORDER BY 
                    columns.column_id
            " table.Schema table.Name
        let xs = this.Execute( query)
        
        xs
        |> Seq.map( fun x -> 
            {
                Name = x ? name
                DataType = x ? type_name
                IsNullable = x ? is_nullable
                IsIdentity = x ? is_identity
                IsComputed = x ? is_computed 
                MaxLength = int<int16> x ? max_length
                NonDefaultScale = x.TryGetValue("scale")
            }
        )
        |> Seq.toArray

    member this.GetForeignKeys( table: Table) = 
        let query = "
            SELECT 
	            FK.name AS Name
				,columns.name AS ColumnName
	            ,OBJECT_SCHEMA_NAME(FKC.referenced_object_id) AS ParentSchema
                ,OBJECT_NAME(FKC.referenced_object_id) AS ParentName
            FROM sys.foreign_keys AS FK
	            JOIN sys.foreign_key_columns AS FKC ON FK.object_id = FKC.constraint_object_id
	            JOIN sys.columns  ON 
                    FKC.parent_column_id = columns.column_id
		            AND FKC.parent_object_id = columns.object_id
				JOIN sys.tables AS T ON FKC.parent_object_id = T.object_id
				JOIN sys.schemas AS S ON T.schema_id = S.schema_id
            WHERE 
                FK.parent_object_id = OBJECT_ID( @tableName)
			ORDER BY 
				FKC.constraint_column_id
        "

        this.Execute( query, [ "@tableName", table.TwoPartName ])
        |> Seq.map(fun x -> (x ? Name, x ? ParentSchema, x ? ParentName), x ? ColumnName)
        |> Seq.groupBy fst
        |> Seq.map (fun ((name, parentSchema, parentName), xs) -> 
            { 
                Name = name
                Columns = [| for _, columnName in xs -> columnName |]
                Parent = { Schema = parentSchema; Name = parentName }; 
            }
        )
        |> Seq.distinctBy (fun x -> x.Parent)
        |> Seq.toArray

    member this.GetIndexes() = 
        let query = "
            SELECT 
	            indexes.name AS Name
	            ,schemas.name AS TableSchema
	            ,tables.name AS TableName
	            ,columns.name AS ColumnName
	            ,index_columns.key_ordinal As ColumnKeyOrdinal
                ,is_primary_key
                ,is_unique
				,TYPE_NAME(columns.system_type_id) AS type_name
            FROM
	            sys.indexes 
	            JOIN sys.tables ON tables.object_id = indexes.object_id 
	            JOIN sys.schemas ON schemas.schema_id = tables.schema_id
	            JOIN sys.index_columns ON 
		            index_columns.object_id = tables.object_id 
		            AND index_columns.index_id = indexes.index_id 
	            JOIN sys.columns ON 
		            columns.object_id = index_columns.object_id 
		            AND columns.column_id = index_columns.column_id
            ORDER BY 
                TableSchema, TableName, ColumnKeyOrdinal
        "
        this.Execute( query)
        |> Seq.map(fun x -> (x ? Name, x ? TableSchema, x ? TableName, x ? is_primary_key, x ? is_unique), (x ? ColumnName, x.TryGetValue("type_name")))
        |> Seq.groupBy fst
        |> Seq.choose (fun ((name, tableSchema, tableName, isPrimaryKey, isUnique), xs) -> 
            let colNames, colTypes = xs |> Seq.map snd |> Seq.toArray |> Array.unzip
            if colTypes |> Array.exists Option.isNone 
                || colTypes |> Array.choose id |> unsupportedColumnTypes.Overlaps
            then 
                None
            else
                Some { 
                    Name = name
                    Table = { Schema = tableSchema; Name = tableName }
                    Columns = colNames
                    IsPrimaryKey = isPrimaryKey
                    IsUnique = isUnique
                }
        )
        |> Seq.toArray


    member this.GetDefaultColumnValues() = 
        async {
            let query = "
                SELECT TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, COLUMN_DEFAULT
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE COLUMN_DEFAULT <> '(NULL)'    
            "
            use cmd = new SqlCommand(query, this)
            use! cursor = cmd.ExecuteReaderAsync() |> Async.AwaitTask
            let result = Dictionary()
            return [ 
                while cursor.Read() do 
                    yield { Schema = cursor ? TABLE_SCHEMA; Name = cursor ? TABLE_NAME }, (cursor ? COLUMN_NAME, cursor ? COLUMN_DEFAULT) 
            ]
        }


