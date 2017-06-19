using AdoteUmFocinhoMobile.Models;
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

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            User = new User();

            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            RegisterCommand = new Command(ExecuteRegisterCommand);

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
                        if (API.HeadersLastResponse.ContainsKey("token"))
                            Headers.Add("token", API.HeadersLastResponse["token"]);

                        API.HeadersAllRequests = Headers;//SET

                        await _navigationService.NavigateAsync("app:///MasterPage/NavigationPage/FeedPage");

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
