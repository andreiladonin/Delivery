using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public class OrderHelper
    {

        public Order Order { get; private set; }
        public OrderHelper(Order order)
        {
            Order = order;
        }
        public Courier InstructOrder(List<Courier> couriers)
        {
            // сначала сортируется по весу, потом сортируется по свободно ли есть место у курьеров, потом сможет ли взять по весу
            var find = couriers
                .OrderBy(c => c.CarryingCapacity)
                .OrderBy(c => c.CountOrders())
                .Where(c => c.CanTakeTheCargo(Order))
                .First();
            return find;
        }

        // не работает
        public Courier SearchCourierByDistance(List<Courier> couriers)
        {
            Courier courier1 = couriers[0];
            var minDistance = couriers[0].Location.GetDistance(Order.From);
            foreach(Courier courier in couriers)
            {
                var distance = courier.Location.GetDistance(Order.From);
                if (minDistance < distance)
                {
                    courier1 = courier;
                    minDistance = distance;
                }
            }
            return courier1;
        }

    }
    
}
