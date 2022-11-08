
using SamgtuProject;
using System.Collections.Immutable;
using System.Linq;

// create couriers
TransportCourier transportCourier1 = new TransportCourier(60, "Vasya", PointHelper.GetRandomPoint());
FootCourier footCourier1 = new FootCourier(25, "Ivan", PointHelper.GetRandomPoint());
CargoCourier cargoCourier1 = new CargoCourier(100, "Andrei", PointHelper.GetRandomPoint());

CargoCourier cargoCourier2 = new CargoCourier(50, "Peter", PointHelper.GetRandomPoint());
TransportCourier transportCourier2 = new TransportCourier(40, "Vova", PointHelper.GetRandomPoint());
FootCourier footCourier2 = new FootCourier(20, "Alex", PointHelper.GetRandomPoint());


Company.Couriers.Add(cargoCourier1);
Company.Couriers.Add(footCourier1);
Company.Couriers.Add(transportCourier1);

Company.Couriers.Add(footCourier2);
Company.Couriers.Add(cargoCourier2);
Company.Couriers.Add(transportCourier2);

Order order1 = new Order(13, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order2 = new Order(45, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order3 = new Order(56, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order4 = new Order(20, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order5 = new Order(80, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order6 = new Order(34, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order7 = new Order(19, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order8 = new Order(45, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());
Order order9 = new Order(89, PointHelper.GetRandomPoint(), PointHelper.GetRandomPoint());


Company.Orders.Add(order1);
Company.Orders.Add(order2);
Company.Orders.Add(order3);
Company.Orders.Add(order4);
Company.Orders.Add(order5);
Company.Orders.Add(order7);
Company.Orders.Add(order8);
Company.Orders.Add(order9);


Company.DistributeOrders(Company.Couriers);


foreach (var courier in Company.Couriers)
{
    Console.WriteLine(courier);
    courier.PrintOrders();
    Console.WriteLine();
}