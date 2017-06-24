using AdoteUmFocinhoMobile.Models;
using AdoteUmFocinhoMobile.Util;
using Prism.Mvvm;
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
    public class FeedPageViewModel : BindableBase, INavigationAware
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

        private string _helloText;

        public string HelloText
        {
            get { return _helloText; }
            set { SetProperty(ref _helloText, value); }
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

            HelloText = "Olá " + App.UsuarioLogado.Name + ".";

            ItemTappedCommand = new Command<Pet>(ExecuteItemTappedCommand);

            FilterCommand = new Command(ExecuteFilterCommand);

            SearchPets();
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

                    StackVisible = true;
                    FlvVisible = false;
                    ActVisible = true;

                    if (App.Latitude == 0 && App.Longitude == 0)
                    {
                        TextAwait = "Olá, estamos procurando os focinhos próximos a você, espera só um pouco...";
                        await GetPosition();
                    }

                    Filters.Latitude = App.Latitude;
                    Filters.Longitude = App.Longitude;

                    TextAwait = "Só mais um pouco. Queremos ter certeza de que iremos encontrar os melhores amigos. Ops, Focinhos.";
                    API.HeadersRequest.Add("widthscreen", App.LarguraTela.ToString());
                    var ReturnPets = await API.POST<ObservableCollection<Pet>>("api/pets/feed", Filters);

                    if (ReturnPets != null && ReturnPets.Count > 0)
                    {
                        TextAwait = "";
                        HeaderText = "Encontramos " + ReturnPets.Count + " focinhos para adoção próximos a você";
                        StackVisible = false;
                        FlvVisible = true;

                        Pets = ReturnPets;
                    }
                    else
                        TextAwait = "Infelizmente não conseguimos encontrar nenhum Focinho próximo a você :(  Tente usar os filtros para ampliar sua busca.";
                    
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if ((Filter)parameters["filter"] != null)
            { 
                Filters = (Filter)parameters["filter"];

                await SearchPets();
            }
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
                        App.Latitude = t.Result.Latitude;
                        App.Longitude = t.Result.Longitude;
                    }

                });
        }


        #endregion
    }
}
