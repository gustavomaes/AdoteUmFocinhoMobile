using System;

namespace AdoteUmFocinhoMobile.Models
{
    public class Pet
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

        public bool Favorite { get; set; }
    }
}
