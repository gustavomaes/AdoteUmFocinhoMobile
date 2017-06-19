using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoteUmFocinhoMobile.Models
{
    public class Filter
    {
        public int Radius { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Specie { get; set; }

        public int LifeStage { get; set; }
    }
}
