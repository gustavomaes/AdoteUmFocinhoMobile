using AdoteUmFocinhoMobile.Util;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class MasterPageViewModel : BindableBase, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;

        //Props
        private List<MasterItem> _pages;

        public List<MasterItem> Pages
        {
            get { return _pages; }
            set { SetProperty(ref _pages, value); }
        }


        //Commands
        public Command LogoffCommand { get; set; }

        public Command<MasterItem> ItemTappedCommand { get; set;
        }
        public MasterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Pages = new List<MasterItem>();
            Pages.Add(new MasterItem("Principal", "fa-home"));
            Pages.Add(new MasterItem("Meus Focinhos", "fa-rss"));
            Pages.Add(new MasterItem("Focinhos Favoritos", "fa-heart"));
            Pages.Add(new MasterItem("Sobre", "fa-info"));

            ItemTappedCommand = new Command<MasterItem>(ExecuteItemTappedCommand);
            LogoffCommand = new Command(ExecuteLogoffCommand);
            
        }

        async void ExecuteLogoffCommand()
        {
            using (APIHelper API = new APIHelper())
            {
                try
                {
                    await API.POST("api/users/logoff", new { });

                    App.UsuarioLogado = null;
                    API.HeadersAllRequests = new Dictionary<string, string>();

                }
                catch (HTTPException EX) { }
                catch (Exception EX) { }

            }

            await _navigationService.NavigateAsync("app:///LoginPage");
        }

        private void ExecuteItemTappedCommand(MasterItem item)
        {
            switch (item.Name)
            {
                case "Principal":
                    ExecuteFeedCommand();
                    break;
                case "Meus Focinhos":
                    ExecuteMyFeedCommand();
                    break;
                case "Focinhos Favoritos":
                    ExecuteMyFavoritesCommand();
                    break;
                case "Sobre":
                    ExecuteAboutCommand();
                    break;
            }
        }

        async void ExecuteAboutCommand()
        {
            await _navigationService.NavigateAsync("NavigationPage/AboutPage");
        }

        async void ExecuteMyFeedCommand()
        {
            await _navigationService.NavigateAsync("NavigationPage/MyFeedPage");
        }

        async void ExecuteFeedCommand()
        {
            await _navigationService.NavigateAsync("NavigationPage/FeedPage");
        }

        async void ExecuteMyFavoritesCommand()
        {
            await _navigationService.NavigateAsync("NavigationPage/FavoriteFeedPage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters) { }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }

    public class MasterItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public MasterItem(string name, string icon)
        {
            this.Name = name;
            this.Icon = icon;
        }
    }
}
