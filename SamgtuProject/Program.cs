using SamgtuProject;
using System.Collections.Immutable;
using System.Linq;

// create couriers
TransportCourier transportCourier1 = new TransportCourier(50, "Vasya", PointHelper.GetRandomPoint());
FootCourier footCourier1 = new FootCourier(25, "Ivan", PointHelper.GetRandomPoint());
CargoCourier cargoCourier2 = new CargoCourier(50, "Peter", PointHelper.GetRandomPoint());
CargoCourier cargoCourier1 = new CargoCourier(90, "Andrei", PointHelper.GetRandomPoint());


Company.Couriers.Add(cargoCourier1);
Company.Couriers.Add(footCourier1);
Company.Couriers.Add(transportCourier1);
Company.Couriers.Add(cargoCourier2);

Order order6 = new Order(60, new Point(10, 2), new Point(1, 2));
Order order7 = new Order(19, new Point(5, 5), new Point(10, 10));
Order order8 = new Order(45, new Point(7, 2), new Point(4, 2));
Order order9 = new Order(9, new Point(6, 4), new Point(1, 2));
Order order5 = new Order(20, new Point(8, 6), new Point(1, 2));
Company.Orders.Add(order5);
Company.Orders.Add(order7);
Company.Orders.Add(order8);
Company.Orders.Add(order9);
Company.Orders.Add(order6);

Company.DistributeOrders();


Console.WriteLine("Текущее время : " + DateTime.Now);
Console.WriteLine();
Console.WriteLine();

foreach(var courier in Company.Couriers)
{
    Console.WriteLine(courier);
    courier.PrintOrders();
    Console.WriteLine();
}
Console.ReadLine();