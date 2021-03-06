﻿using Prism.Unity;
using AdoteUmFocinhoMobile.Views;
using Xamarin.Forms;
using Newtonsoft.Json;
using System;
using AdoteUmFocinhoMobile.Models;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace AdoteUmFocinhoMobile
{
    public partial class App : PrismApplication
    {
        #region Preferences Data
        //http://192.168.25.135:4000
        public const string URL = "http://adoteumfocinhoweb.azurewebsites.net";
        public const string URLSocial = "https://adoteumfocinho.azurewebsites.net/";

        public static App Current;

        public static double Latitude;
        public static double Longitude;

        //Social Login
        public static string AuthToken { get; set; }
        public static string UserId { get; set; }
        public static string SocialName { get; set; }
        public static string SocialEmail { get; set; }

        public string PushToken
        {
            get { return App.PreferenceGet<String>("PushToken"); }
            set { App.PreferenceAdd("PushToken", value); }
        }

        public static User UsuarioLogado
        {
            get { return App.PreferenceGet<User>("UsuarioLogado"); }
            set { App.PreferenceAdd("UsuarioLogado", value); }
        }

        public static bool Logado
        {
            get { return UsuarioLogado != null; }
        }

        public static int LarguraTela
        {
            get { return App.PreferenceGet<int>("LarguraTela"); }
            set { App.PreferenceAdd("LarguraTela", value); }
        }
        public static int AlturaTela
        {
            get { return App.PreferenceGet<int>("AlturaTela"); }
            set { App.PreferenceAdd("AlturaTela", value); }
        }
        public static int _LarguraDP;
        public static int LarguraDP
        {
            get { return App.PreferenceGet<int>("LarguraDP"); }
            set { App.PreferenceAdd("LarguraDP", value); }
        }

        #endregion


        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            Current = this;

            LarguraTela = Resolver.Resolve<IDevice>().Display.Width;
            AlturaTela = Resolver.Resolve<IDevice>().Display.Height;
            LarguraDP = _LarguraDP;

            if (Logado)
                NavigationService.NavigateAsync("MasterPage/NavigationPage/FeedPage");
            else
                NavigationService.NavigateAsync("LoginPage");

        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<RegistrationPage>();
            Container.RegisterTypeForNavigation<PrivacyPolicyPage>();
            Container.RegisterTypeForNavigation<FeedPage>();
            Container.RegisterTypeForNavigation<MyFeedPage>();
            Container.RegisterTypeForNavigation<FavoriteFeedPage>();
            Container.RegisterTypeForNavigation<AboutPage>();
            Container.RegisterTypeForNavigation<FiltersPage>();
            Container.RegisterTypeForNavigation<DetailPage>();
            Container.RegisterTypeForNavigation<RegisterPetPage>();
            Container.RegisterTypeForNavigation<MasterPage>();
        }


        #region Preferences

        public static void PreferenceAdd(string Name, object Data)
        {
            string Serializable = JsonConvert.SerializeObject(Data);

            Current.Properties[Name] = Serializable;
            Current.SavePropertiesAsync();

        }

        public static bool PreferenceExist(string Name)
        {
            return Current.Properties.ContainsKey("Name");
        }

        public static void PreferenceRemove(string Name)
        {
            Current.Properties.Remove(Name);
            Current.SavePropertiesAsync();
        }

        public static T PreferenceGet<T>(string Name)
        {
            if (Current.Properties.ContainsKey(Name))
            {
                string Dado = Current.Properties[Name].ToString();

                return JsonConvert.DeserializeObject<T>(Dado);

            }

            return default(T);
        }

        #endregion
    }
}
