# EntityFramework 7 Sql Server specific DbContext scaffolding for F#.

### Background

https://ef.readthedocs.org/en/latest/

### Aim

The latest and greatest version of ORM from Microsoft EF7 ships with [default CLI-based scaffolding] (https://ef.readthedocs.org/en/latest/getting-started/full-dotnet/existing-db.html). It works but F#
developers can do better by leveraging unique design time mechamism called ["Type Providers"](https://msdn.microsoft.com/en-us/library/hh156509.aspx).

__Key constrains of current version__:
* EF7 or later (EF6 is not supporrted)
* Sql Server only
* Full .NET framework only (Core CLR vesrion will come once F# compiler supports it)

### Nuget package 

https://www.nuget.org/packages/FSharp.EntityFramework.MicrosoftSqlServer

### Sample
