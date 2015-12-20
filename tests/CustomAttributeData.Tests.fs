module CustomAttributeDataTests

open Xunit
open System.ComponentModel.DataAnnotations
open System.ComponentModel.DataAnnotations.Schema
open System.Reflection
open System
open FSharp.Quotations
open FSharp.Quotations.Patterns

open System.Runtime.CompilerServices

type CustomAttributeData with
    static member Of<'T when 'T :> Attribute>([<ReflectedDefinition>] attr: Expr<'T>) = 
        let rec parseAttributePropertySetters attrInstance expr =
            seq {
                match expr with
                | unit when unit = <@@ () @@> -> ()
                | Sequential( left, PropertySet( Some( Var this), propertyInfo, [], Value( value, typ))) 
                | Sequential( left, PropertySet( Some( Var this), propertyInfo, [], ValueWithName( value, typ, _))) when this = attrInstance ->
                    yield! parseAttributePropertySetters attrInstance left
                    yield CustomAttributeNamedArgument(propertyInfo, CustomAttributeTypedArgument( typ, value))
                | _ -> failwithf "Unexpected expression: %A" expr
            }

        let rec (|AttributeCtorCall|) expr =
            match expr with
            | Patterns.WithValue(value, attrType, expr)
                -> (|AttributeCtorCall|) expr
            | NewObject(ctor, args) ->  
                let ctorArgs = 
                    args  
                    |> List.choose (
                        function 
                        | Value(value, typ) | ValueWithName(value, typ, _) -> Some( CustomAttributeTypedArgument( typ, value)) 
                        | _ -> None 
                    ) 
                    |> List.toArray

                ctor, ctorArgs, Array.empty
            | Let(binding, AttributeCtorCall(ctor, ctorArgs, _), Sequential(setters, Var result)) when binding = result ->
                ctor, ctorArgs, setters |> parseAttributePropertySetters binding |> Array.ofSeq 
            | _ -> failwithf "Unexpected expression %A" attr

        let ctor, ctorArgs, namedArgs = (|AttributeCtorCall|) attr

        {
            new CustomAttributeData() with 
                member __.Constructor = ctor
                member __.ConstructorArguments = upcast ctorArgs
                member __.NamedArguments  = upcast namedArgs
        }



[<Fact>]
let fullyInlinedValue() = 
    let attrData = CustomAttributeData.Of( TableAttribute( "Shift"))
    Assert.Equal(typeof<TableAttribute>, attrData.AttributeType)
    Assert.Equal(typeof<TableAttribute>.GetConstructor([| typeof<string> |]), attrData.Constructor)
    Assert.Equal<_ list>(
        [ typeof<string>, box "Shift" ] , 
        [ for x in attrData.ConstructorArguments -> x.ArgumentType, x.Value ]
    )

//[<Fact>]
//let refToLocal() = 
//    let tableAttr = TableAttribute( "Shift")
//    let attrData = CustomAttributeData.Of tableAttr
//    Assert.Equal(typeof<TableAttribute>, attrData.AttributeType)
//    Assert.Equal(typeof<TableAttribute>.GetConstructor([| typeof<string> |]), attrData.Constructor)
//    Assert.Equal<_ list>(
//        [ typeof<string>, box "Shift" ] , 
//        [ for x in attrData.ConstructorArguments -> x.ArgumentType, x.Value ]
//    )

[<Fact>]
let ctorArgs() = 
    let tableName = "Shift"
    let attrData = CustomAttributeData.Of( TableAttribute( tableName))
    Assert.Equal(typeof<TableAttribute>, attrData.AttributeType)
    Assert.Equal(typeof<TableAttribute>.GetConstructor([| typeof<string> |]), attrData.Constructor)
    Assert.Equal<_ list>(
        [ typeof<string>, box "Shift" ] , 
        [ for x in attrData.ConstructorArguments -> x.ArgumentType, x.Value ]
    )

//[<Fact>]
//let ctorArgs2() = 
//    let tableName = "Shift"
//    let tableAttr = TableAttribute( tableName)
//    let attrData = CustomAttributeData.Of( tableAttr)
//    Assert.Equal(typeof<TableAttribute>, attrData.AttributeType)
//    Assert.Equal(typeof<TableAttribute>.GetConstructor([| typeof<string> |]), attrData.Constructor)
//    Assert.Equal<_ list>(
//        [ typeof<string>, box "Shift" ] , 
//        [ for x in attrData.ConstructorArguments -> x.ArgumentType, x.Value ]
//    )

[<Fact>]
let ctorAndNamedArgs() = 
    let tableName = "Shift"
    let schema = "HumanResources"
    let attrData = CustomAttributeData.Of( TableAttribute( tableName, Schema = schema))
    Assert.Equal(typeof<TableAttribute>, attrData.AttributeType)
    Assert.Equal(typeof<TableAttribute>.GetConstructor([| typeof<string> |]), attrData.Constructor)
    Assert.Equal<_ list>(
        [ typeof<string>, box "Shift" ] , 
        [ for x in attrData.ConstructorArguments -> x.ArgumentType, x.Value ]
    )
    Assert.Equal<_ list>(
        [ "Schema", typeof<string>, box "HumanResources" ] , 
        [ for x in attrData.NamedArguments -> x.MemberName, x.TypedValue.ArgumentType, x.TypedValue.Value ]
    )

[<Fact>]
let ctorAndNamedArgs2() = 
    let name = "ShiftId"
    let order = 1
    let attrData = CustomAttributeData.Of( ColumnAttribute(name, TypeName = "int", Order = order))
    Assert.Equal(typeof<ColumnAttribute>, attrData.AttributeType)
    Assert.Equal<_ list>(
        [ typeof<string>, box "ShiftId" ] , 
        [ for x in attrData.ConstructorArguments -> x.ArgumentType, x.Value ]
    )
    Assert.Equal<_ list>(
        [ 
            "TypeName", typeof<string>, box "int" 
            "Order", typeof<int>, box order
        ], 
        [ for x in attrData.NamedArguments -> x.MemberName, x.TypedValue.ArgumentType, x.TypedValue.Value ]
    )
