using AdoteUmFocinhoMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class FavoriteFeedPageViewModel : BindableBase
    {
        private List<Pet> _pets;

        public List<Pet> Pets
        {
            get { return _pets; }
            set { SetProperty(ref _pets, value); }
        }

        public int HeightDP { get; set; }

        public FavoriteFeedPageViewModel()
        {
            Pets = new List<Pet>();
            HeightDP = App.LarguraDP / 2;

            Pet temp = new Pet();
            temp.Description = "Teste layout 01";
            temp.PhotoUrl = "http://www.dhresource.com/0x0s/f2-albu-g3-M00-65-49-rBVaHVZ-fPOAHufBAALXPo3WVlA919.jpg/comfortable-cozy-dog-cat-puppy-sleeping-bag.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 02";
            temp.PhotoUrl = "https://ae01.alicdn.com/kf/HTB1H_LELFXXXXXBapXXq6xXFXXXv/2016-Snow-White-Puppy-Dog-Princess-Dress-Spring-and-Summer-Pet-Clothes-Skirt-For-Small-Dogs.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 03";
            temp.PhotoUrl = "http://www.101dogbreeds.com/wp-content/uploads/2014/11/Louisiana-Catahoula-Leopard-Dog-Images.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 01";
            temp.PhotoUrl = "http://www.dhresource.com/0x0s/f2-albu-g3-M00-65-49-rBVaHVZ-fPOAHufBAALXPo3WVlA919.jpg/comfortable-cozy-dog-cat-puppy-sleeping-bag.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 02";
            temp.PhotoUrl = "https://ae01.alicdn.com/kf/HTB1H_LELFXXXXXBapXXq6xXFXXXv/2016-Snow-White-Puppy-Dog-Princess-Dress-Spring-and-Summer-Pet-Clothes-Skirt-For-Small-Dogs.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 03";
            temp.PhotoUrl = "http://www.101dogbreeds.com/wp-content/uploads/2014/11/Louisiana-Catahoula-Leopard-Dog-Images.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 01";
            temp.PhotoUrl = "http://www.dhresource.com/0x0s/f2-albu-g3-M00-65-49-rBVaHVZ-fPOAHufBAALXPo3WVlA919.jpg/comfortable-cozy-dog-cat-puppy-sleeping-bag.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 02";
            temp.PhotoUrl = "https://ae01.alicdn.com/kf/HTB1H_LELFXXXXXBapXXq6xXFXXXv/2016-Snow-White-Puppy-Dog-Princess-Dress-Spring-and-Summer-Pet-Clothes-Skirt-For-Small-Dogs.jpg";
            Pets.Add(temp);

            temp = new Pet();
            temp.Description = "Teste layout 03";
            temp.PhotoUrl = "http://www.101dogbreeds.com/wp-content/uploads/2014/11/Louisiana-Catahoula-Leopard-Dog-Images.jpg";
            Pets.Add(temp);
        }
    }
}
