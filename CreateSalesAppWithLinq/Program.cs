
using CreateSalesAppWithLinq.Controllers;
using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

Console.WriteLine("Create Sales App");


AppDbContext _context = new();

CustomersController custCtrl = new(_context);
ProductsController prodCtrl = new(_context);
OrdersController ordCtrl = new(_context);
OrdersLinesController ordlineCtrl = new(_context);

var orderline = await ordlineCtrl.GetByPK(1);

orderline.Quantity = 2;

await ordlineCtrl.Update(orderline.Id, orderline);


//var Fred = await ordCtrl.GetByPK(1);
//await ordCtrl.SetStatusToInProcess(Fred.Id, Fred);
//Console.WriteLine(Fred);

#region Order SetStatusToClosed
//var maxOrder = await ordCtrl.GetByPK(1);
//Console.WriteLine($" B4 change, status is {maxOrder.Status}");
//
//await ordCtrl.SetStatusToClosed(maxOrder.Id, maxOrder);
//Console.WriteLine($"After change, status is {maxOrder.Status}");
#endregion



//(await ordCtrl.GetOrdersByProductId(1)).ToList().ForEach(o => Console.WriteLine($"{o.Description} | {o.}"));
//
//var fred = await ordCtrl.GetOrdersByProductId(1);
//foreach(Order o in fred)
//{
//    Console.WriteLine($"{o.Description}");
//}


#region Async GetOrderByCode
//async Get Order by Code.
//var fred = await ordCtrl.GetOrdersByCustomerCode("MAX");
//foreach(Order o in fred)
//{
//Console.WriteLine($"{o.Id} | {o.Description} | {o.Total}");
//
//}
//    //or can do it this way Chaining them together. Put on multi lines so easier to read
//(await ordCtrl.GetOrdersByCustomerCode("MAX"))
//    .ToList()
//    .ForEach(o => Console.WriteLine($"{o.Id} | {o.Description} | {o.Total}"));
#endregion





