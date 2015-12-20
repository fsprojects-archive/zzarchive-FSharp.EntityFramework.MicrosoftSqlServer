using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultReverseEngineering
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AdventureWorks2014Context())
            {
                int?[] ids = { 707, 708, 709 };
                var resultset =
                    from soh in db.SalesOrderHeader
                    //where soh.OrderDate > new DateTime(2013, 5, 1)
                    //join sod in db.SalesOrderDetail on soh.SalesOrderID equals sod.SalesOrderID
                    //select new { soh.SalesOrderID, sod.ProductID } into lineItems
                    //join p in db.Product on lineItems.ProductID equals p.ProductID into xs
                    //from x in xs
                    ////where ids.Contains(x.ProductSubcategoryID)
                    //select new { OrderId = lineItems.SalesOrderID, Items = xs.Count() };
                    select soh;
                //new { OrderId = g; Total = g.Count() };

                Console.WriteLine($"Total {resultset.Count()}");
                //foreach (var x in resultset)
                //    Console.WriteLine("SalesOrderID {0}, Count: {1}", x.OrderId, x.Items);
            }
        }
    }
}
