using AdoteUmFocinhoMobile.Models;
using AdoteUmFocinhoMobile.Services;
using AdoteUmFocinhoMobile.Util;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class LoginPageViewModel : LoadingController, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        //Azure
        AzureService azureService;
        
        //Props
        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        //Commands
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command FacebookCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            azureService = Xamarin.Forms.DependencyService.Get<AzureService>();
            
            User = new User();

            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            RegisterCommand = new Command(ExecuteRegisterCommand);
            FacebookCommand = new Command(ExecuteFacebookCommand);

        }

        async void ExecuteFacebookCommand(object obj)
        {
            Inicia("Aguarde um momento...");
            var logged = await azureService.LoginAsync();
            if (logged)
            {
                User NewUser = new User();
                NewUser.Name = App.SocialName;
                NewUser.Email = App.SocialEmail;
                NewUser.IdSocial = App.UserId;

                using (APIHelper API = new APIHelper())
                {
                    try
                    {
                        App.UsuarioLogado = await API.POST<User>("api/users/facebook", NewUser);

                        if (App.Logado)
                        {
                            Dictionary<String, String> Headers = API.HeadersAllRequests;

                            //Add token usuário
                            if (API.HeadersLastResponse.ContainsKey("Token"))
                                Headers.Add("Token", API.HeadersLastResponse["Token"]);

                            API.HeadersAllRequests = Headers;

                            Finaliza();

                            await _navigationService.NavigateAsync("PrivacyPolicyPage");
                        }
                    }
                    catch (Exception)
                    {
                        Finaliza();
                    }
                }
            }
            //api/users/facebook
            Finaliza();
        }

        async Task ExecuteLoginCommand()
        {
            Inicia("Validando Usuário");

            try
            {
                if (String.IsNullOrEmpty(User.Email))
                {
                    await _dialogService.DisplayAlertAsync("Atenção", "Informe um e-mail.", "OK");
                    Finaliza();
                    return;
                }
                if (String.IsNullOrEmpty(User.Password))
                {
                    await _dialogService.DisplayAlertAsync("Atenção", "Informe uma senha.", "OK");
                    Finaliza();
                    return;
                }
            }
            catch (HTTPException EX)
            {
                Finaliza();
                await _dialogService.DisplayAlertAsync("Atenção", EX.Message, "OK");
            }
            catch (Exception)
            {
                Finaliza();
                await _dialogService.DisplayAlertAsync("Erro", "Ocorreu um erro inesperado. Tente novamente mais tarde.", "OK");
            }

            //Efetua o Login
            using (APIHelper API = new APIHelper())
            {
                try
                {
                    App.UsuarioLogado = await API.POST<User>("api/user/login", User);

                    if (App.Logado)
                    {
                        Dictionary<String, String> Headers = API.HeadersAllRequests;
                        //Add Token
                        if (API.HeadersLastResponse.ContainsKey("Token"))
                            Headers.Add("Token", API.HeadersLastResponse["Token"]);

                        API.HeadersAllRequests = Headers;//SET

                        Finaliza();

                        await _navigationService.NavigateAsync("PrivacyPolicyPage");

                    }
                }
                catch (Exception)
                {
                    Finaliza();
                }
            }
        }

        async void ExecuteRegisterCommand(object obj)
        {
            await _navigationService.NavigateAsync("RegistrationPage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
