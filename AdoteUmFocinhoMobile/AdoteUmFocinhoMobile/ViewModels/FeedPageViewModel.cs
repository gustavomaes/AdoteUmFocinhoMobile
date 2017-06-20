using AdoteUmFocinhoMobile.Models;
using AdoteUmFocinhoMobile.Util;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class FeedPageViewModel : LoadingController, INavigationAware
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

        private string _headerText;

        public string HeaderText
        {
            get { return _headerText; }
            set { SetProperty(ref _headerText, value); }
        }


        private ObservableCollection<Pet> _pets;

        public ObservableCollection<Pet> Pets
        {
            get { return _pets; }
            set { SetProperty(ref _pets, value); }
        }

        private Filter _filters;

        public Filter Filters
        {
            get { return _filters; }
            set { SetProperty(ref _filters, value); }
        }

        public int HeightDP { get; set; }

        //Commands
        public Command<Pet> ItemTappedCommand { get; set; }
        public Command FilterCommand { get; set; }

        public FeedPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            HeightDP = App.LarguraDP / 2;

            Filters = new Filter();
            Filters.Radius = 50;
            Filters.Specie = new List<int>(new int[] { 1,2 });
            Filters.LifeStage = new List<int>(new int[] { 1, 2, 3, 4 }); ;

            
            ItemTappedCommand = new Command<Pet>(ExecuteItemTappedCommand);

            FilterCommand = new Command(ExecuteFilterCommand);

        }

        async void ExecuteFilterCommand()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("filter", Filters);

            await _navigationService.NavigateAsync("FiltersPage", navigationParams);
        }

        async Task SearchPets()
        {
            try
            {

                using (APIHelper API = new APIHelper())
                {
                    TextAwait = "Estamos pegando a sua localização, aguarde um momento.";
                    StackVisible = true;
                    FlvVisible = false;

                    await GetPosition();

                    TextAwait = "Agora estamos procurando Focinhos próximos a você, espera só mais um pouquinho.";
                    API.HeadersRequest.Add("widthscreen", App.LarguraTela.ToString());
                    var ReturnPets = await API.POST<ObservableCollection<Pet>>("api/pets/feed", Filters);

                    if (ReturnPets != null)
                    {
                        TextAwait = "";
                        HeaderText = "Encontramos " + ReturnPets.Count + " focinhos para adoção próximos a você";
                        StackVisible = false;
                        FlvVisible = true;

                        Pets = ReturnPets;
                    }
                    else
                        TextAwait = "Infelizmente não achamos nenhum focinho próximo a você :(";



                }
            }
            catch (HTTPException) { }
            catch (Exception) { }
        }

        private void ExecuteItemTappedCommand(Pet pet)
        {
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if ((Filter)parameters["filter"] != null)
            { 
                Filters = (Filter)parameters["filter"];
            }
            await SearchPets();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        #region Localizacao

        IGeolocator geolocator;

        public static String PositionStatus { get; set; }

        async Task GetPosition()
        {
            if (geolocator == null)
                geolocator = DependencyService.Get<IGeolocator>() ?? Resolver.Resolve<IGeolocator>();

            await this.geolocator.GetPositionAsync(timeout: 10000)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
                    else if (t.IsCanceled)
                        PositionStatus = "Cancelado";
                    else
                    {
                        PositionStatus = t.Result.Timestamp.ToString("G");
                        Filters.Latitude = t.Result.Latitude;
                        Filters.Longitude = t.Result.Longitude;

                    }

                });
        }


        #endregion
    }
}
