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
        public const double DefaultPrice = 100;

        public static HashSet<Courier> Couriers = new HashSet<Courier>();
        public static List<Order> Orders = new List<Order>();

        public static void DistributeOrders(HashSet<Courier> couriers, int countOrders = 0)
        {
            // countOrders кол-во заказов у каждого курьера

            if (couriers.Count == 1)
            {
                var courier = couriers.First();
                var order = Orders.Where(o => courier.CanTakeTheCargo(o)).OrderByDescending(o => courier.GetPriceOrder(o)).FirstOrDefault();
                if (order != null)
                {
                    courier.SetOnOrder(order);
                    Orders.Remove(order);
                }
                else
                {
                    DistributeOrders(Couriers, countOrders + 1);
                }
                
            }

            foreach (var courier in couriers)
            {
                // ищем заказы подходящий по весу и какой заказ предложит большую цену
                var orders = Orders.Where(o => courier.CanTakeTheCargo(o)).OrderByDescending(o => courier.GetPriceOrder(o)).ToList();

                if (orders.Count != 0)
                {
                    // ищем заказ ближаший
                    var order = SearchOrderiByMinDistance(courier, orders);
                    courier.SetOnOrder(order);
                    Orders.Remove(order);
                }

            }

            // если остались заказы и есть свободные курьеры
            if (Orders.Count != 0)
            {
                var findEpmtyCouries = SearchCouriesByCountOrders(couriers, countOrders);
                // если есть свободные курьеры под заказы, даем им заказа
                if (findEpmtyCouries.Count != 0)
                {
                    DistributeOrders(findEpmtyCouries, countOrders);
                }
                // а если нет то countOrders + 1 и передаем всех курьеров
                DistributeOrders(Couriers, countOrders +1);
            }
        }


        // ищет минимальный заказ по расстоянию
        private static Order SearchOrderiByMinDistance(Courier courier, List<Order> orders)
        {
            double minDistance = courier.Location.GetDistance(orders[0].From);
            Order searchOrder = orders[0];
            foreach (var order in orders)
            {
                if (courier.Location.GetDistance(order.From) < minDistance)
                {
                    searchOrder = order;
                }
            }
            return searchOrder;
        }
        private static HashSet<Courier> SearchCouriesByCountOrders(HashSet<Courier> couriers, int countOrders)
        {
            if (couriers.Count == 1)
                return couriers;
            return couriers.Where(c => c.CountOrders() == countOrders).ToHashSet();
        }
    }
}
