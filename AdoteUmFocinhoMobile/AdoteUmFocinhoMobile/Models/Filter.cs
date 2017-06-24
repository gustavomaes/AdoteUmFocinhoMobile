using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoteUmFocinhoMobile.Models
{
    public class Filter : BindableBase
    {
        private int _radius;

        public int Radius
        {
            get { return _radius; }
            set { SetProperty(ref _radius, value); }
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<int> Specie;

        public List<int> LifeStage;

        public List<int> Genders;
    }
}
