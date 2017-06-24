using AdoteUmFocinhoMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class FiltersPageViewModel : BindableBase, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;

        //Props
        private Filter _filter;

        public Filter Filters
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }

        //Specie
        private bool _dog;

        public bool Dog
        {
            get { return _dog; }
            set { SetProperty(ref _dog, value); }
        }

        private bool _cat;

        public bool Cat
        {
            get { return _cat; }
            set { SetProperty(ref _cat, value); }
        }

        //Gender
        private bool _male;

        public bool Male
        {
            get { return _male; }
            set { SetProperty(ref _male, value); }
        }

        private bool _female;

        public bool Female
        {
            get { return _female; }
            set { SetProperty(ref _female, value); }
        }

        //LifeStage
        private bool _puppy;

        public bool Puppy
        {
            get { return _puppy; }
            set { SetProperty(ref _puppy, value); }
        }

        private bool _teenager;

        public bool Teenager
        {
            get { return _teenager; }
            set { SetProperty(ref _teenager, value); }
        }

        private bool _adult;

        public bool Adult
        {
            get { return _adult; }
            set { SetProperty(ref _adult, value); }
        }

        private bool _senior;

        public bool Senior
        {
            get { return _senior; }
            set { SetProperty(ref _senior, value); }
        }


        public FiltersPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        void SetUpView()
        {
            Male = Filters.Genders.Contains(1);
            Female = Filters.Genders.Contains(2);

            Dog = Filters.Specie.Contains(1);
            Cat = Filters.Specie.Contains(2);

            Puppy = Filters.LifeStage.Contains(1);
            Teenager = Filters.LifeStage.Contains(2);
            Adult = Filters.LifeStage.Contains(3);
            Senior = Filters.LifeStage.Contains(4);
        }

        void SetUpFilter()
        {
            Filters.Genders = new List<int>();
            if (Male) Filters.Genders.Add(1);
            if (Female) Filters.Genders.Add(2);

            Filters.Specie = new List<int>();
            if (Dog) Filters.Specie.Add(1);
            if (Cat) Filters.Specie.Add(2);

            Filters.LifeStage = new List<int>();
            if (Puppy) Filters.LifeStage.Add(1);
            if (Teenager) Filters.LifeStage.Add(2);
            if (Adult) Filters.LifeStage.Add(3);
            if (Senior) Filters.LifeStage.Add(4);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            SetUpFilter();
            parameters.Add("filter", Filters);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Filters = (Filter)parameters["filter"];
            SetUpView();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

    }
}
