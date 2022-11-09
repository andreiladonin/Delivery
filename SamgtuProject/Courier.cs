using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public abstract class Courier : IComparable<Courier>
    {
        public Point Location { get; set; }

        // м / мин
        public abstract double Speed { get; } 

        public string Name { get; set; }

        public double Price { get; set; }

        public double CarryingCapacity { get; set; }

        public bool CanTakeTheCargo(Order order)
        {
            return CarryingCapacity >= order.Weight;
        }
        public Courier( double carryingCapacity, string name, Point point)
        {
            CarryingCapacity = carryingCapacity;
            Name = name;
            this.Location = point;
        }

        private List<Order> _orders = new List<Order>();

        public int CountOrders() => _orders.Count;

        public void SetOnOrder(Order order) => _orders.Add(order);


        public void PrintOrders()
        {
            Console.WriteLine("Заказы курера");
            foreach (var order in _orders)
            {
                Console.WriteLine(order);
            }
        }

        public double GetPriceOrder(Order order)
        {
            return Price * order.From.GetDistance(order.To);
        }

        //public void AssignToOrder(Order order)
        //{
        //    double distanceToOrders = Math.Sqrt(Math.Pow(order.PlaceOfDeparture.X - location.X, 2) + Math.Pow(order.PlaceOfDeparture.Y - location.Y, 2));
        //    Console.WriteLine($"Расстояние курьера до заказа {distanceToOrders} км");

        //    double distance = Math.Round(Math.Round(distanceToOrders, 2) + order.GetDistance(), 2);
        //    Console.WriteLine($"Общее расстояние {distance} км");

        //    double cost = Math.Round(Price * distance, 2);
        //    Console.WriteLine($"Стоимость доставки {cost} рублей");
        //    double time = distance / Speed;

        //    Console.WriteLine($"Время в минутах {time}");

        //}
        public override string ToString()
        {
            return string.Format("Курьер: {0}|" +
                " Скорость: {1} |" +
                " Грузоподъмность {2} |" +
                " Находится в {3}",
                Name, Speed, CarryingCapacity, Location.ToString());
        }

        public int CompareTo(Courier? other)
        {
            return this.CarryingCapacity > other.CarryingCapacity ? 1 : -1;
        }
    }

    class FootCourier : Courier
    {
        public FootCourier(double carryingCapacity, string name, Point point) 
            :base(carryingCapacity, name, point)
        {
            Price = Company.DefaultPrice;
        }

        public override double Speed { get { return 100; } }
    }

    class TransportCourier : Courier
    {
        public override double Speed { get { return 1500; } }
        public TransportCourier(double carryingCapacity, string name, Point point) 
            :base(carryingCapacity, name, point)
        {
            Price = Company.DefaultPrice * 3;
        }
    }

    class CargoCourier : TransportCourier
    {
        public override double Speed { get { return 700; } }

        public CargoCourier(double carryingCapacity, string name, Point point) : base(carryingCapacity, name, point)
        {
            Price = Company.DefaultPrice * 7;

        }
    }

}
