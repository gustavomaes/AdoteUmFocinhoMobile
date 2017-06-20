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

        //Commands
        public Command MyFavoritesCommand { get; set; }
        public Command FeedCommand { get; set; }
        public Command MyFeedCommand { get; set; }
        public Command AboutCommand { get; set; }

        public MasterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            MyFavoritesCommand = new Command(ExecuteMyFavoritesCommand);
            FeedCommand = new Command(ExecuteFeedCommand);
            MyFeedCommand = new Command(ExecuteMyFeedCommand);
            AboutCommand = new Command(ExecuteAboutCommand);
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
}
