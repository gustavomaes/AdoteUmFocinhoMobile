using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //public Command MyFavoritesCommand { get; set; }
        //public Command FeedCommand { get; set; }
        //public Command MyFeedCommand { get; set; }
        //public Command AboutCommand { get; set; }
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
            //MyFavoritesCommand = new Command(ExecuteMyFavoritesCommand);
            //FeedCommand = new Command(ExecuteFeedCommand);
            //MyFeedCommand = new Command(ExecuteMyFeedCommand);
            //AboutCommand = new Command(ExecuteAboutCommand);
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
