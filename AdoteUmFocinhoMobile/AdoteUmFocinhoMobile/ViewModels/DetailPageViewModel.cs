using AdoteUmFocinhoMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using Prism.Services;
using AdoteUmFocinhoMobile.Util;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class DetailPageViewModel : BindableBase, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        //Props
        bool Deleted;

        private Pet _petSelected;

        public Pet PetSelected
        {
            get { return _petSelected; }
            set { SetProperty(ref _petSelected, value); }
        }

        private bool _emailVisible;

        public bool EmailVisible
        {
            get { return _emailVisible; }
            set { SetProperty(ref _emailVisible, value); }
        }

        private bool _phoneVisible;

        public bool PhoneVisible
        {
            get { return _phoneVisible; }
            set { SetProperty(ref _phoneVisible, value); }
        }

        private bool _whatsappVisible;

        public bool WhatsappVisible
        {
            get { return _whatsappVisible; }
            set { SetProperty(ref _whatsappVisible, value); }
        }


        private string _specieText;

        public string SpecieText
        {
            get { return _specieText; }
            set { SetProperty(ref _specieText, value); }
        }

        private string _lifeStageText;

        public string LifeStageText
        {
            get { return _lifeStageText; }
            set { SetProperty(ref _lifeStageText, value); }
        }

        private string _genderText;

        public string GenderText
        {
            get { return _genderText; }
            set { SetProperty(ref _genderText, value); }
        }

        //Commands
        public Command ReportCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public DetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            ReportCommand = new Command(async () => await ExecuteReportCommand());
            DeleteCommand = new Command(async () => await ExecuteDeleteCommand());
        }

        private async Task ExecuteDeleteCommand()
        {
            var answer = await _dialogService.DisplayAlertAsync("Atenção", "Tem certeza que deseja excluir esse anuncio ?", "Sim", "Não");

            if (answer)
            {
                try
                {
                    using (APIHelper API = new APIHelper())
                    {
                        await API.DELETE("api/pets/" + PetSelected.Id);
                        Deleted = true;
                    }

                    await _navigationService.GoBackAsync();
                }
                catch (HTTPException) { }
            }
        }

        private async Task ExecuteReportCommand()
        {
            var answer = await _dialogService.DisplayAlertAsync("Atenção", "Tem certeza que deseja denunciar esse anuncio ?", "Sim", "Não");

            if (answer)
            {
                try
                {
                    using (APIHelper API = new APIHelper())
                    {
                        await API.PUT("api/pets/report/" + PetSelected.Id, null);
                    }
                }
                catch (HTTPException) { }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (Deleted)
                parameters.Add("delete", PetSelected);
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            PetSelected = (Pet)parameters["pet"];

            switch (PetSelected.Specie)
            {
                case Pet.SpecieAnimals.Dog:
                    SpecieText = "Cachorro";
                    break;
                case Pet.SpecieAnimals.Cat:
                    SpecieText = "Gato";
                    break;
            }

            switch (PetSelected.type)
            {
                case Pet.LifeStages.Puppy:
                    LifeStageText = "Filhote";
                    break;
                case Pet.LifeStages.Teenager:
                    LifeStageText = "Jovem";
                    break;
                case Pet.LifeStages.Adult:
                    LifeStageText = "Adulto";
                    break;
                case Pet.LifeStages.Senior:
                    LifeStageText = "3ª Idade";
                    break;
            }

            switch (PetSelected.Gender)
            {
                case Pet.GenderTypes.Male:
                    GenderText = "Macho";
                    break;
                case Pet.GenderTypes.Famele:
                    GenderText = "Fêmea";
                    break;
            }

            EmailVisible = !String.IsNullOrEmpty(PetSelected.Email);
            PhoneVisible = !String.IsNullOrEmpty(PetSelected.Phone);
            WhatsappVisible = !String.IsNullOrEmpty(PetSelected.Whatsapp);

        }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }
}
