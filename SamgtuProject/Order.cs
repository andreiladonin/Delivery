using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    class Order
    {
        private double weight;

        public double Weight { get => weight;}

        private Point place_of_departure;
        public Point PlaceOfDeparture { get => place_of_departure; }

        private Point place_of_delevery;
        public Point PlaceOfDelevery { get => place_of_delevery; }

        public Order(double weight, Point place_of_departure, Point place_of_delevery)
        {
            this.weight = weight;
            this.place_of_departure = place_of_departure;
            this.place_of_delevery = place_of_delevery;
        }

        public double GetDistance ()
        {
            double s = Math.Sqrt(Math.Pow(place_of_delevery.X - place_of_departure.X, 2) + Math.Pow(place_of_delevery.Y - place_of_departure.Y, 2));
            return Math.Round(s, 2);
        }
    }
}
