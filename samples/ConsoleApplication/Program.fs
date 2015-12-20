open FSharp.Data.Entity

type AdventureWorks = SqlServer<"Data Source=.;Initial Catalog=AdventureWorks2014;Integrated Security=True", Pluralize = true>

let db = new AdventureWorks()

for x in db.``HumanResources.Shifts`` do 
    printfn "%A" (x.ShiftID, x.Name, x.StartTime, x.EndTime, x.ModifiedDate)

