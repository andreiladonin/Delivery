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

        public static void DistributeOrders()
        {
            // распределение заказов
            foreach(var order in Orders)
            {
                OrderHelper orderHelper = new OrderHelper(order);
                Courier courier = orderHelper.InstructOrder(Couriers.ToList());
                courier.SetOnOrder(order);
            }
        }

    }
}
