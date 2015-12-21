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

```F#

open FSharp.Data.Entity

type AdventureWorks = SqlServer<"Data Source=.;Initial Catalog=AdventureWorks2014;Integrated Security=True", Pluralize = true>

let db = new AdventureWorks()

db.OnModelCreating <- fun modelBuilder -> 
    //override default model here
    ()

db.OnConfiguring <- fun optionsBuilder -> 
    //do runtime configuration here: connection, transactoin etc.
    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=AdventureWorks2014;Integrated Security=True") |> ignore

query {
    for x in db.``HumanResources.Shifts`` do
    where (x.ShiftID > 1uy)
    select x.Name
}
|> Seq.iter (printfn "Shift: %s")

//insert
let newShift = 
    new AdventureWorks.``HumanResources.Shift``(
        Name = "French coffee break", 
        StartTime = TimeSpan.FromHours 10., 
        EndTime = TimeSpan.FromHours 12.,
        ModifiedDate = DateTime.Now
    )

let change = db.``HumanResources.Shifts``.Add(newShift) 
let recordsAffrected = db.SaveChanges()

```
