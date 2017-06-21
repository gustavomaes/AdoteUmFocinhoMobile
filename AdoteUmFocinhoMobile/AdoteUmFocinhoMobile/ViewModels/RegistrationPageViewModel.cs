using AdoteUmFocinhoMobile.Models;
using AdoteUmFocinhoMobile.Util;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class RegistrationPageViewModel : LoadingController, INavigationAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        public User NewUser { get; set; }
        public Command BackCommand { get; }
        public Command RegisterCommand { get; }

        public RegistrationPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            NewUser = new User();

            RegisterCommand = new Command(async () => await ExecuteRegisterCommand());

            BackCommand = new Command(ExecuteBackCommand);
        }

        async Task ExecuteRegisterCommand()
        {
            Inicia("Realizando Cadastro...");

            try
            {
                if (String.IsNullOrEmpty(NewUser.Name))
                {
                    await _dialogService.DisplayAlertAsync("Atenção", "Informe um Nome.", "OK");
                    Finaliza();
                    return;
                }
                if (String.IsNullOrEmpty(NewUser.Email))
                {
                    await _dialogService.DisplayAlertAsync("Atenção", "Informe um E-mail.", "OK");
                    Finaliza();
                    return;
                }
                if (String.IsNullOrEmpty(NewUser.Password))
                {
                    await _dialogService.DisplayAlertAsync("Atenção", "Informe uma Senha.", "OK");
                    Finaliza();
                    return;
                }

            }
            catch (HTTPException EX)
            {
                await _dialogService.DisplayAlertAsync("Atenção", EX.Message, "OK");
                Finaliza();
            }
            catch
            {
                await _dialogService.DisplayAlertAsync("Erro", "Ocorreu um erro. Tente novamente mais tarde.", "OK");
                Finaliza();
            }

            using (APIHelper API = new APIHelper())
            {
                try
                {
                    App.UsuarioLogado = await API.POST<User>("api/users", NewUser);

                    if (App.Logado)
                    {
                        Dictionary<String, String> Headers = API.HeadersAllRequests;

                        //Add token usuário
                        if (API.HeadersLastResponse.ContainsKey("Token"))
                            Headers.Add("Token", API.HeadersLastResponse["Token"]);

                        API.HeadersAllRequests = Headers;

                        await _navigationService.NavigateAsync("app:///MasterPage/NavigationPage/FeedPage");
                    }
                }
                catch (Exception)
                {
                    Finaliza();
                }
            }
        }

        async void ExecuteBackCommand(object obj)
        {
            await _navigationService.GoBackAsync();
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
