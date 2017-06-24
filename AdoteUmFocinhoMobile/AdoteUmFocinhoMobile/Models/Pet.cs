using AdoteUmFocinhoMobile.Util;
using System;

namespace AdoteUmFocinhoMobile.Models
{
    public class Pet : PetController
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool Block { get; set; }

        public enum LifeStages
        {
            Puppy = 1,
            Teenager = 2,
            Adult = 3,
            Senior = 4
        }

        public LifeStages type { get; set; }

        public enum SpecieAnimals
        {
            Dog = 1,
            Cat = 2
        }

        public SpecieAnimals Specie { get; set; }

        public enum GenderTypes
        {
            Male = 1,
            Famele = 2
        }

        public GenderTypes Gender { get; set; }

        public byte[] Photo { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public string Phone { get; set; }

        public string Whatsapp { get; set; }

        public string Email { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Date { get; set; }

        public int AmountReports { get; set; }

        private bool _favorite;
        public bool Favorite
        {
            get { return _favorite; }
            set
            {
                SetProperty(ref _favorite, value);
                RaisePropertyChanged("IconFavorite");
            }
        }


        public bool MyPet { get; set; }
        public bool NotMyPet { get { return !MyPet; } }

        public string IconFavorite { get { return (Favorite ? "fa-heart" : "fa-heart-o"); } }
    }
}
