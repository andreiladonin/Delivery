

using SamgtuProject;

Order order = new Order(50, new Point(0, 2), new Point(-2, 9));

Courier courier1 = new Courier(2, 70);

courier1.AssignToOrder(order);


//Console.WriteLine(order.GetDistance());