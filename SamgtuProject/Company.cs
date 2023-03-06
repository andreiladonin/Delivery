using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics.SymbolStore;
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
                List<PlaningOption> planings = new List<PlaningOption>();
                foreach (var courier in Couriers)
                {
                    var planingOptions = courier.CanAcceptOrder(order);

                    if (planingOptions.ReadyAcept)
                    {
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


            for (int i = 0; i < couriers.Count; i++)
            {
                for (int j = 0; j < couriers.Count - 1; j++)
                {
                    if (HowManyTime(couriers[i], order) < HowManyTime(couriers[j], order))
                    {

                        if (couriers[i].CountOrders() > couriers[j].CountOrders())
                        {
                            bestPlaningOption.Courier = couriers[j];
                        }
                        else
                        {
                            bestPlaningOption.Courier = couriers[i];
                        }
                         
                    }
                    else
                    {
                        if (couriers[j].CountOrders() > couriers[i].CountOrders())
                        {
                            bestPlaningOption.Courier = couriers[i];
                        }
                        else
                        {
                            bestPlaningOption.Courier = couriers[j];
                        }
                        
                    }
                }
            }

            bestPlaningOption.Courier.SetOnOrder(order);
        }

        public static DateTime HowManyTime(Courier courrier, Order order)
        {
            double t1;
            double t2;
            var beforeDistance = order.From.GetDistance(courrier.GetLocation());
            var afterDistance = order.To.GetDistance(order.To);

            t1 = Math.Round(beforeDistance / courrier.Speed, 2);
            t2 = Math.Round(afterDistance / courrier.Speed, 2);


            var sum_t = DateTime.Now.AddMinutes(t1 + t2);
            return sum_t;
        }
    }

}

