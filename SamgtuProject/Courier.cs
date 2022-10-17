using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    class Courier
    {
        private Point location = new Point(0, 0);

        public Point Location { get { return location; } }

        // километры в минуты
        public double Speed { get; set; }

        // цена за километр
        public double Price { get; set; }

        private List<Order> Orders = new List<Order>();

        public Courier(double speed, double price)
        {
            Speed = speed;
            Price = price;
        }



        public void AssignToOrder(Order order)
        {
            double distanceToOrders = Math.Sqrt(Math.Pow(order.PlaceOfDeparture.X - location.X, 2) + Math.Pow(order.PlaceOfDeparture.Y - location.Y, 2));
            Console.WriteLine($"Расстояние курьера до заказа {distanceToOrders} км");

            double distance = Math.Round(Math.Round(distanceToOrders, 2) + order.GetDistance(), 2);
            Console.WriteLine($"Общее расстояние {distance} км");

            double cost = Math.Round(Price * distance, 2);
            Console.WriteLine($"Стоимость доставки {cost} рублей");
            double time = distance / Speed;

            Console.WriteLine($"Время в минутах {time}");
        
        }
    }
}
