using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X} | Y: {Y}";
        }
    }

    static class PointHelper
    {
        public static double GetDistance(this Point from, Point to)
        {
            double s = Math.Sqrt(Math.Pow(to.X - from.X, 2) + Math.Pow(to.Y - from.Y, 2));
            return Math.Round(s, 2);
        }

        public static Point GetRandomPoint()
        {
            return new Point(new Random().Next(20), new Random().Next(20));
        }
    }
}
