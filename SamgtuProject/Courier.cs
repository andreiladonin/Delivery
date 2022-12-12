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

        public Point GetLocation ()
        {
            if (_orders.Count != 0)
                return _orders[_orders.Count - 1].To;
            else
                return Location;
        }
        public void PrintOrders()
        {
            Console.WriteLine("Заказы курера");
            foreach (var order in _orders)
            {
                Console.WriteLine("Расстояние до закказа");
                Console.WriteLine(order.From.GetDistance(this.Location));
                Console.WriteLine(order);
            }
        }

        public double GetPriceOrder(Order order)
        {
            return Price * order.Weight;
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

        public PlaningOption CanAcceptOrder(Order order)
        {
            var planingOption = new PlaningOption();

            bool canCargo = CanTakeTheCargo(order);

            if (canCargo)
            {
                var price = GetPriceOrder(order);
                planingOption.Price = price;
                planingOption.ReadyAcept = true;
                planingOption.HowManyTime = HowManyTime(order);
                planingOption.Courier = this;
            }
                
            return planingOption;
        }
        // до какого времени он сделает заказ
        public DateTime HowManyTime(Order order)
        {
            double t1;
            double t2;
            var beforeDistance = order.From.GetDistance(this.GetLocation());
            var afterDistance = order.To.GetDistance(order.To);

            t1 = Math.Round(beforeDistance / this.Speed, 2)*180;
            t2 = Math.Round(afterDistance/ this.Speed, 2)*180;


            var sum_t = DateTime.Now.AddMinutes(t1 + t2);
            return sum_t;
        }


        // есть или у курьера заказы
        public bool ThereAreOrders ()
        {
            return _orders.Count > 0;
        }

        public void SortOrders()
        {
            List<Order> sortOrders = new List<Order>();
            double minDistance = _orders[0].From.GetDistance(this.Location);

            foreach (var order in _orders)
            {
                double distance = order.From.GetDistance(this.Location);
                if (distance < minDistance)
                {
                    sortOrders.Insert(0, order);
                    minDistance = distance;
                } else
                {
                    sortOrders.Add(order);
                }
            }
            _orders = sortOrders;
        }
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
            return this.CountOrders() - other.CountOrders();
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
            Price = Company.DefaultPrice * 5;

        }
    }

}
