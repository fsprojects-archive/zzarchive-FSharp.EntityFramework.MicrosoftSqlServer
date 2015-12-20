module AdventureWorks

open System
open System.Data.SqlClient
open Xunit
open Microsoft.Data.Entity
open FSharp.Data.Entity

type AdventureWorks = SqlServer<"Data Source=.;Initial Catalog=AdventureWorks2014;Integrated Security=True", Pluralize = true>

let db = new AdventureWorks()

[<Fact>]
let getTableContent() = 
    let expected = [|
        ( 1uy, "Day",  TimeSpan.Parse("07:00:00"), TimeSpan.Parse("15:00:00"), DateTime.Parse("2008-04-30"))
        ( 2uy, "Evening", TimeSpan.Parse("15:00:00"), TimeSpan.Parse("23:00:00"), DateTime.Parse("2008-04-30"))
        ( 3uy, "Night", TimeSpan.Parse("23:00:00"), TimeSpan.Parse("07:00:00"), DateTime.Parse("2008-04-30"))
    |]

    let actual = [| for x in db.``HumanResources.Shifts`` -> x.ShiftID, x.Name, x.StartTime, x.EndTime, x.ModifiedDate |]

    Assert.Equal<_ []>(expected, actual)

[<Fact>]
let linqFilterOnServer() = 
    let actual = 
        query {
            for x in db.``HumanResources.Shifts`` do
            where (x.ShiftID > 1uy)
            select x.Name
        }
        |> Seq.toArray
    
    Assert.Equal<_[]>(actual, [| "Evening"; "Night" |])

[<Fact>]
let linqPartiallyInLocalMemory() = 
    let actual = 
        query {
            for x in db.``HumanResources.Shifts`` do
            where (x.ShiftID > 1uy)
            where (x.StartTime.Hours > 15) //this is executed currently in local memory
            select x.Name
        }
        |> Seq.exactlyOne
    
    Assert.Equal<string>("Night", actual)


[<Fact>]
let insertData() = 
    let newShift = 
        new AdventureWorks.``HumanResources.Shift``(
            Name = "French coffee break", 
            StartTime = TimeSpan.FromHours 10., 
            EndTime = TimeSpan.FromHours 12.,
            ModifiedDate = DateTime.Now
        )

    let change = db.``HumanResources.Shifts``.Add(newShift) 
    try
        let recordsAffrected = db.SaveChanges()
        Assert.Equal(1, recordsAffrected)
        Assert.True(change.Entity.ShiftID > 0uy)
    finally
        db.``HumanResources.Shifts``.Remove change.Entity |> ignore
        let recordsAffrected = db.SaveChanges()
        Assert.Equal(1, recordsAffrected)

open FSharp.Linq.NullableOperators

[<Fact>]
let nullableColumn() = 
    let upperManagement = 
        query {
            for x in db.``HumanResources.Employees`` do
            where (x.OrganizationLevel ?< 2s)
            select x.LoginID
        }
        |> Seq.toList

    let expected = [
        @"adventure-works\terri0";
        @"adventure-works\david0";
        @"adventure-works\james1";
        @"adventure-works\laura1";
        @"adventure-works\jean0";
        @"adventure-works\brian3"
    ]

    Assert.Equal<_ list>(expected, upperManagement)

[<Fact>]
let innerJoin() = 
    let actual =
        query {
            for e in db.``HumanResources.Employees`` do
            join p in db.``Person.People`` on (e.BusinessEntityID = p.BusinessEntityID)
            where (e.OrganizationLevel = Nullable(1s))
            sortBy p.LastName
            select(e.HireDate, String.Format("{0} {1}", p.FirstName, p.LastName))
        }
        |> Seq.toArray

    let expected = [|
        DateTime( 2007, 12, 20),  "David Bradley"
        DateTime( 2008, 01, 31),  "Terri Duffy"
        DateTime( 2009, 02, 03),  "James Hamilton"
        DateTime( 2009, 01, 31),  "Laura Norman"
        DateTime( 2008, 12, 11),  "Jean Trenary"
        DateTime( 2011, 02, 15), "Brian Welcker"
    |]
    
    Assert.Equal<_[]>(expected, actual)

[<Fact>]
let fromSql() =
    let customers = db.``Sales.Customers``
    let actual = 
        let query = 
            "SELECT 
                CustomerID
                ,PersonID
                ,StoreID
                ,TerritoryID
                ,AccountNumber
                ,rowguid
                ,ModifiedDate 
            FROM 
                Sales.Customer 
            WHERE 
                CustomerID <= {0}"

        customers.FromSql( query, 2) |> Seq.toArray

    let expected = 
        query {
            for x in customers do
            where (x.CustomerID <= 2) 
            select x
        }
        |> Seq.toArray

    Assert.Equal<_ []>(expected, actual)

[<Fact>]
let ``Failing From Sql Invocation That Can Be Caught At Compile Time``() =
    let customers = db.``HumanResources.Shifts``

    do //incorrect runtime parameter type: int instead of time span
        let xs = customers.FromSql("
            SELECT ShiftID, Name, StartTime, EndTime, ModifiedDate
            FROM HumanResources.Shift 
            WHERE StartTime > {0}", 42
        )
        let error = Assert.Throws<SqlException>(fun () -> xs |> Seq.length |> ignore)
        Assert.Equal<string>("Operand type clash: time is incompatible with int", error.Message)
    
    do //missing required output column: Name
        let xs = customers.FromSql("
            SELECT ShiftID, StartTime, EndTime, ModifiedDate
            FROM HumanResources.Shift"
        )
        let error = Assert.Throws<InvalidOperationException>(fun () -> xs |> Seq.toArray |> ignore)
        Assert.Equal<string>(
            "The required column 'Name' was not present in the results of a 'FromSql' operation.", 
            error.Message
        )
    
    do //invalid t-sql: "ORDER StartTime" instead of "ORDER BY StartTime"
        let xs = customers.FromSql("
            SELECT ShiftID, StartTime, EndTime, ModifiedDate
            FROM HumanResources.Shift
            ORDER StartTime
        ")
        let error = Assert.Throws<SqlException>(fun () -> xs |> Seq.toArray |> ignore)
        Assert.Equal<string>(
            "Incorrect syntax near 'StartTime'.", 
            error.Message
        )

//open System.Linq
//        
//[<Fact>]
//let navigationQuery() = 
//        query {
//            for o in db.``Sales.SalesOrderHeaders`` do
//            let customer = o.FK_SalesOrderHeader_Customer_CustomerID
//            let personLastName = customer.FK_Customer_Person_PersonID.LastName
//            where (personLastName = "Zhou")
//            //groupBy customer.PersonID into g
//            //sortByNullableDescending g.Key
//            //sumBy (o.TotalDue)
//        }
//        |> Seq.take 3
//        |> Seq.toArray
//        |> printfn "Result %A"

//[<Fact(Skip="do not work :(")>]
//let navigationQuery2() = 
//        query {
//            for c in 
//                db.``Sales.Customers``
//                .Include(fun x -> x.FK_Customer_Person_PersonID)
//                .Include(fun x -> x.FK_SalesOrderHeader_Customer_CustomerID) do
//            let person = c.FK_Customer_Person_PersonID
//            where (person.LastName = "Zhou")
//            //let totalOrders = query { for x in c.FK_SalesOrderHeader_Customer_CustomerID do sumBy x.TotalDue }
//            //let totalOrders = c.FK_SalesOrderHeader_Customer_CustomerID |> Seq.toArray |> Array.sumBy (fun x -> x.TotalDue)
//            //sortByDescending totalOrders
//            for h in c.FK_SalesOrderHeader_Customer_CustomerID do
//            select((person.FirstName, person.LastName), h.TotalDue)
//        }
//        |> Seq.toArray
//        |> Seq.groupBy(fun x -> fst x)
//        |> Seq.map(fun (name, xs) -> xs |> Seq.sumBy snd)
//        //|> Seq.sortDescending
//        |> Seq.take 3
//        |> Seq.toArray
//        |> printfn "Result %A"
//
////select top 3 sum(h.TotalDue)
////from Sales.Customer c 
////	left join Sales.SalesOrderHeader h on h.CustomerID = c.CustomerID
////	left join Person.Person p on c.PersonID = p.BusinessEntityID
////where 
////	p.LastName = 'Zhou'
////group by p.FirstName, p.LastName
////order by sum(h.TotalDue) desc
////
////
