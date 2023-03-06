using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public abstract class Courier : IComparable<Courier>
    {
        public Point Location { get; set; }

        private static int id;

        public int Id { get; private set; } 

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
            id += 1;
            Id = id;
            CarryingCapacity = carryingCapacity;
            Name = name;    
            this.Location = point;
        }

        private List<Order> _orders = new List<Order>();

        public List<Order> GetOrders()
        {
            return _orders;
        }

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

            
            Console.Write(new String('-', 35) + $"Заказы курера {this.Name} Грузоподъемность {this.CarryingCapacity}");
            Console.Write(new String('-', 35) + '\n');
            int queueOrder = 0;

            Point tmp_point = Location;
            foreach (var order in _orders)
            {
                Console.Write(order);
                
                Console.Write($"Oчередь в Расписании {++queueOrder} | Растояние  заказа (от последнего положения)" + order.From.GetDistance(tmp_point));
                Console.Write(" | Сделает  к времени: " + Company.HowManyTime(this, order));
                Console.WriteLine();
                tmp_point = order.To;
            }
        }

        public double GetPriceOrder(Order order)
        {
            return Price * order.Weight;
        }

        public PlaningOption CanAcceptOrder(Order order)
        {
            var planingOption = new PlaningOption();

            bool canCargo = CanTakeTheCargo(order);

            if (canCargo)
            {
                var price = GetPriceOrder(order);
                planingOption.Price = price;
                planingOption.ReadyAcept = true;
                planingOption.HowManyTime = Company.HowManyTime(this, order);
                planingOption.Courier = this;
            }
                
            return planingOption;
        }
        // до какого времени он сделает заказ


        // есть или у курьера заказы
        public bool ThereAreOrders ()
        {
            return _orders.Count > 0;
        }

        public override string ToString()
        {
            return string.Format("ID: {0} Курьер: {1}|" +
                " Скорость: {2} |" +
                " Грузоподъмность {3} |" +
                " Находится в {4}", Id,
                Name, Speed, CarryingCapacity, Location.ToString());
        }

        public int CompareTo(Courier? other)
        {
            return this.CountOrders() - other.CountOrders();
        }

        public double SumTotal()
        {
            double sum = 0;
            foreach (var order in _orders)
            {
                sum += GetPriceOrder(order);
            }

            return sum;
        }
    }

    class FootCourier : Courier
    {

        public const int MAX_WEIGHT = 30;
        public FootCourier(double carryingCapacity, string name, Point point) 
            :base(carryingCapacity, name, point)
        {
            Price = Company.DefaultPrice;
        }

        public override double Speed { get { return 5; } }
    }

    class TransportCourier : Courier
    {
        
        public override double Speed { get { return 50; } }
        public TransportCourier(double carryingCapacity, string name, Point point) 
            :base(carryingCapacity, name, point)
        {
            Price = Company.DefaultPrice * 3;
        }
    }


}
