using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class AboutPageViewModel : BindableBase, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;

        //Command
        public Command PrivacyPolicyCommand { get; set; }

        public AboutPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            PrivacyPolicyCommand = new Command(ExecutePrivacyPolicyCommand);
        }

        async void ExecutePrivacyPolicyCommand(object obj)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("visible", false);

            await _navigationService.NavigateAsync("PrivacyPolicyPage", navigationParams);
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters) { }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }
}
