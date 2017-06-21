using AdoteUmFocinhoMobile.Models;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AdoteUmFocinhoMobile.Services.AzureService))]
namespace AdoteUmFocinhoMobile.Services
{
    public class AzureService
    {
        List<AppServiceIdentity> identities = null;
        public static MobileServiceClient Client { get; set; } = null;

        public void Initialize()
        {
            Client = new MobileServiceClient(App.URLSocial);
        }

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            App.AuthToken = string.Empty;
            App.UserId = string.Empty;

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível efetuar login, tente novamente.", "OK");
                });
                return false;
            }
            else
            {
                App.UserId = user.UserId;
                identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
                App.SocialName = identities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
                App.SocialEmail = identities[0].UserId;

            }
            return true;
        }
    }
}
