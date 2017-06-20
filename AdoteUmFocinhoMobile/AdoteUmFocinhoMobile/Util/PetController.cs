using AdoteUmFocinhoMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdoteUmFocinhoMobile.Util
{
    public class PetController : BindableBase
    {

        public DelegateCommand<Pet> FavoriteCommand { get; set; }

        public PetController()
        {
            FavoriteCommand = new DelegateCommand<Pet>(ExecuteFavoriteCommand);
        }

        private async void ExecuteFavoriteCommand(Pet pet)
        {
            try
            {
                using (APIHelper API = new APIHelper())
                {
                    if (pet.Favorite)
                    {
                        pet.Favorite = false;
                        await API.PUT("api/pets/favorite/" + pet.Id + "/?Favorite=false", null);
                    }
                    else
                    {
                        pet.Favorite = true;
                        await API.PUT("api/pets/favorite/" + pet.Id + "/?Favorite=true", null);
                    }
                }
            }
            catch (HTTPException) { }
        }
    }
}
