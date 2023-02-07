using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    class Company
    {
        public const double DefaultPrice = 50;

        public static HashSet<Courier> Couriers = new HashSet<Courier>();
        public static List<Order> Orders = new List<Order>();
        private static double _sumTotal;

        public static double SumTotal { get { return _sumTotal; } }
        public static void DistributeOrders()
        {

            // распределение заказов
            while (Orders.Count != 0)
            {
                var order = Orders.First();
                Console.WriteLine(order);
                List<PlaningOption> planings = new List<PlaningOption>();
                foreach (var courier in Couriers)
                {
                    var planingOptions = courier.CanAcceptOrder(order);

                    if (planingOptions.ReadyAcept)
                    {
                        Console.WriteLine($"\t Курьер {courier.Name} готов принять заказ. Стоимость курьера на заказ {planingOptions.Price}");

                        Console.WriteLine($"\t до какого времени он сделает заказ {planingOptions.HowManyTime}");
                        Console.WriteLine();
                        planings.Add(planingOptions);

                    }
                }
                if (planings.Count == 1)
                    planings[0].Courier.SetOnOrder(order);
                else
                {
                    CheckToBestOption(order, planings);
                }
                
                Orders.Remove(order);
            }

            double sumTotal = 0;

            foreach (var courier in Couriers)
            {
                sumTotal += courier.SumTotal();
            }

            _sumTotal += sumTotal;
        }


        private static void CheckToBestOption(Order order, List<PlaningOption> planingOptions)
        {

            PlaningOption bestPlaningOption = new PlaningOption();
            bestPlaningOption.Price = planingOptions[0].Price;
            bestPlaningOption.HowManyTime = planingOptions[0].HowManyTime;
            bestPlaningOption.Courier = planingOptions[0].Courier;

            List<Courier> couriers = new();

            foreach (PlaningOption planingOption in planingOptions)
            {
                couriers.Add(planingOption.Courier);
            }

            couriers = couriers.OrderBy(x => x.CountOrders()).ToList();



            for (int i = couriers.Count - 1; i >= 0; i--)
            {
                for (int j = couriers.Count - 2; j >= 1; j--)
                {
                    if (couriers[j].HowManyTime(order) < couriers[i].HowManyTime(order))
                    {
                        if (couriers[j].CountOrders() < couriers[i].CountOrders())
                            bestPlaningOption.Courier = couriers[j];
                        else
                            bestPlaningOption.Courier = couriers[i];
                    }
                    else if (couriers[i].HowManyTime(order) < couriers[j].HowManyTime(order))
                    {
                        if (couriers[i].CountOrders() < couriers[j].CountOrders())
                            bestPlaningOption.Courier = couriers[i];
                        else
                            bestPlaningOption.Courier = couriers[j];
                    }
                }
            }

            bestPlaningOption.Courier.SetOnOrder(order);
        }



        public static void AddOrder()
        {
            Console.WriteLine("Введите вес заказа ");

            double weight = Convert.ToDouble(Console.ReadLine());

            Point from = PointHelper.GetRandomPoint();
            Point to = PointHelper.GetRandomPoint();


            Order order = new(weight, from, to);

            Console.WriteLine("Заказ создан");
            Console.WriteLine("\t " + order);
            Orders.Add(order);
        }
    }
}
