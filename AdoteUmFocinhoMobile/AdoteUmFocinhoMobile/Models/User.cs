using System;

namespace AdoteUmFocinhoMobile.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Block { get; set; }

        public string IdSocial { get; set; }

        public DateTime Date { get; set; }
    }
}
