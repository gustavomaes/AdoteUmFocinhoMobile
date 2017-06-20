using AdoteUmFocinhoMobile.Models;
using AdoteUmFocinhoMobile.Util;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class FavoriteFeedPageViewModel : BindableBase, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;

        //Prop
        private bool _flvVisible;

        public bool FlvVisible
        {
            get { return _flvVisible; }
            set { SetProperty(ref _flvVisible, value); }
        }

        private string _textAwait;

        public string TextAwait
        {
            get { return _textAwait; }
            set { SetProperty(ref _textAwait, value); }
        }

        private bool _stackVisible;

        public bool StackVisible
        {
            get { return _stackVisible; }
            set { SetProperty(ref _stackVisible, value); }
        }

        private bool _actVisible;

        public bool ActVisible
        {
            get { return _actVisible; }
            set { SetProperty(ref _actVisible, value); }
        }


        private ObservableCollection<Pet> _pets;

        public ObservableCollection<Pet> Pets
        {
            get { return _pets; }
            set { SetProperty(ref _pets, value); }
        }

        //Commands
        public Command<Pet> ItemTappedCommand { get; set; }

        public int HeightDP { get; set; }

        public FavoriteFeedPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            HeightDP = App.LarguraDP / 2;

            ItemTappedCommand = new Command<Pet>(ExecuteItemTappedCommand);

            SearchPets();
        }

        async Task SearchPets()
        {
            try
            {

                using (APIHelper API = new APIHelper())
                {
                    TextAwait = "Estamos buscando seus Focinhos favoritos...";
                    StackVisible = true;
                    FlvVisible = false;
                    ActVisible = true;

                    API.HeadersRequest.Add("widthscreen", App.LarguraTela.ToString());
                    var ReturnPets = await API.GET<ObservableCollection<Pet>>("api/pets/favorite");

                    if (ReturnPets != null)
                    {
                        TextAwait = "";
                        StackVisible = false;
                        FlvVisible = true;
                        
                        Pets = ReturnPets;
                    }
                    else
                        TextAwait = "Você ainda não marcou nenhum Focinho como favorito.";

                    ActVisible = false;
                }
            }
            catch (HTTPException) { }
            catch (Exception) { }
        }

        private async void ExecuteItemTappedCommand(Pet pet)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("pet", pet);

            await _navigationService.NavigateAsync("DetailPage", navigationParams);
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters) { }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }
}
