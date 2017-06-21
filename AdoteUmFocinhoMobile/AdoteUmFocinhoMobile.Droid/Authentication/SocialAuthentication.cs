using AdoteUmFocinhoMobile.Droid.Authentication;
using AdoteUmFocinhoMobile.Services;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SocialAuthentication))]
namespace AdoteUmFocinhoMobile.Droid.Authentication
{
    public class SocialAuthentication : IAuthentication
    {

        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client,
                                                        MobileServiceAuthenticationProvider provider,
                                                        IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

                App.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                App.UserId = user?.UserId;

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}