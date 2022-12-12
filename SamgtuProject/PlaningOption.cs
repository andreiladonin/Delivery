using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamgtuProject
{
    public class PlaningOption
    {
        public double Price { get; set; }
        public DateTime HowManyTime { get; set; }
        public bool ReadyAcept { get; set; }
        public Courier Courier { get; set; }
    }
}
