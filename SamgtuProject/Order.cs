using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public class Order
    {
        private double weight;

        public double Weight { get => weight;}

        private Point from;
        public Point From { get => from; }

        private Point to;
        public Point To { get => to; }

        
        
        public Order(double weight, Point from, Point to)
        {
            this.weight = weight;
            this.from = from;
            this.to = to;
        }

        public override string ToString()
        {
            return $"Weight {weight} | From ({from.X}, {from.Y}) | To ({from.X}, {from.Y})";
        }
    }
}
