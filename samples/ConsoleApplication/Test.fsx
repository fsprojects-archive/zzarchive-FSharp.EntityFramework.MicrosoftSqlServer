#I @"..\..\packages"

#r @"EntityFramework.Core.7.0.0-rc1-final\lib\net451\EntityFramework.Core.dll"
#r @"EntityFramework.MicrosoftSqlServer.7.0.0-rc1-final\lib\net451\EntityFramework.MicrosoftSqlServer.dll"
#r @"EntityFramework.Relational.7.0.0-rc1-final\lib\net451\EntityFramework.Relational.dll"
#r @"Inflector.1.0.0.0\lib\net45\Inflector.dll"
#r @"Microsoft.Extensions.Caching.Abstractions.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Caching.Abstractions.dll"
#r @"Microsoft.Extensions.Caching.Memory.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Caching.Memory.dll"
#r @"Microsoft.Extensions.Configuration.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Configuration.dll"
#r @"Microsoft.Extensions.Configuration.Abstractions.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Configuration.Abstractions.dll"
#r @"Microsoft.Extensions.Configuration.Binder.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Configuration.Binder.dll"
#r @"Microsoft.Extensions.DependencyInjection.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.DependencyInjection.dll"
#r @"Microsoft.Extensions.Logging.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Logging.dll"
#r @"Microsoft.Extensions.Logging.Abstractions.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Logging.Abstractions.dll"
#r @"Microsoft.Extensions.OptionsModel.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.OptionsModel.dll"
#r @"Microsoft.Extensions.Primitives.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.Primitives.dll"
#r @"Remotion.Linq.2.0.1\lib\net45\Remotion.Linq.dll"
#r @"System.Collections.Immutable.1.1.36\lib\portable-net45+win8+wp8+wpa81\System.Collections.Immutable.dll"
#r @"System.Diagnostics.DiagnosticSource.4.0.0-beta-23516\lib\dotnet5.2\System.Diagnostics.DiagnosticSource.dll"
#r @"Ix-Async.1.2.5\lib\net45\System.Interactive.Async.dll"

// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//Bizarre, but full path to Microsoft.Extensions.DependencyInjection.Abstractions.dll is required. #I directive doesn’t work :(
#r @"C:\Users\mitekm\Documents\GitHub\Sandbox\FSharp.EntityFramework.MicrosoftSqlServer\packages\Microsoft.Extensions.DependencyInjection.Abstractions.1.0.0-rc1-final\lib\net451\Microsoft.Extensions.DependencyInjection.Abstractions.dll"

#r @"FSharp.EntityFramework.MicrosoftSqlServer.0.0.2.0-alpha\lib\net451\FSharp.EntityFramework.MicrosoftSqlServer.dll"

open FSharp.Data.Entity
open Microsoft.Data.Entity

type AdventureWorks = SqlServer<"Data Source=.;Initial Catalog=AdventureWorks2014;Integrated Security=True", Pluralize = true>

let db = new AdventureWorks()

db.OnConfiguring <- fun optionsBuilder -> 
    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=AdventureWorks2014;Integrated Security=True") |> ignore

db.OnModelCreating <- fun modelBuilder -> 
    modelBuilder.Entity<AdventureWorks.``HumanResources.Shift``>().ToTable("Shift", "HumanResources") |> ignore

query {
    for x in db.``HumanResources.Shifts`` do
    where (x.ShiftID > 1uy)
    select x.Name
}
|> Seq.iter (printfn "Shift: %s")



