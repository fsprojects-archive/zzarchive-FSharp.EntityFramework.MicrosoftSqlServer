# EntityFramework 7 Sql Server specific DbContext scaffolding for F#.

### Background

https://ef.readthedocs.org/en/latest/

### Aim

EF7 ships with [default CLI-based scaffolding] (https://ef.readthedocs.org/en/latest/getting-started/full-dotnet/existing-db.html). It works but F# developers can do better by leveraging unique design time mechamism called ["Type Providers"](https://msdn.microsoft.com/en-us/library/hh156509.aspx).

##### Constrains of current version 

1. EF7 or later (EF6 is not supporrted)
2. Sql Server only
3. Full .NET framework only (Core CLR version will come once F# compiler provides supports for it)
4. Limitations inherited from current RC1 version of EF7: 
  * no VIEWs support
  * hierarchyid, sql_variant, geography, geometry, xml are not yet supported
  * others

*Worth noting that the library doesn't attempt to extend or alter EF7 runtime semantics - it's purely design time tool.* 

### Nuget package 

https://www.nuget.org/packages/FSharp.EntityFramework.MicrosoftSqlServer

### Sample
