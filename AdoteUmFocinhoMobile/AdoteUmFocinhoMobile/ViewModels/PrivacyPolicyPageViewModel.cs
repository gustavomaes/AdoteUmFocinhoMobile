using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class PrivacyPolicyPageViewModel : BindableBase, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;

        //Props
        private bool _stackVisible;

        public bool StackVisible
        {
            get { return _stackVisible; }
            set { SetProperty(ref _stackVisible, value); }
        }


        //Commands
        public Command RefuseCommand { get; set; }
        public Command AcceptCommand { get; set; }

        public PrivacyPolicyPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            StackVisible = true;

            RefuseCommand = new Command(ExecuteRefuseCommand);
            AcceptCommand = new Command(ExecuteAcceptCommand);
        }

        async void ExecuteAcceptCommand()
        {
            await _navigationService.NavigateAsync("app:///MasterPage/NavigationPage/FeedPage");
        }

        async void ExecuteRefuseCommand()
        {
            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            StackVisible = (bool)parameters["visible"];
        }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }
}
