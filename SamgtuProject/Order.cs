using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public class Order
    {
        private static int id = 0;

        static int GenerateOrder()
        {
            id += 1;

            return id;
        }

        private int _id;
        public int Id { get { return _id; } }

        private double weight;
       
        public double Weight { get => weight;}

        private Point from;
        public Point From { get => from; }

        private Point to;
        public Point To { get => to; } 
        public Order(double weight, Point from, Point to)
        {
            this._id = GenerateOrder(); 
            this.weight = weight;
            this.from = from;
            this.to = to;
        }

        public override string ToString()
        {
            return $"Id {this.Id} | ВЕС {weight} | ИЗ ({from.X}, {from.Y}) | КУДА ({to.X}, {to.Y})";
        }
    }
}
