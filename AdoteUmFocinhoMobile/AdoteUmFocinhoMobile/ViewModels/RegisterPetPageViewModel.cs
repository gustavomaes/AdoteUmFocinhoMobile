using AdoteUmFocinhoMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using AdoteUmFocinhoMobile.Util;
using Prism.Services;

namespace AdoteUmFocinhoMobile.ViewModels
{
    public class RegisterPetPageViewModel : LoadingController, INavigationAware
    {
        //Prism
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        //Props
        Pet PostGravado;

        private Pet _newPet;

        public Pet NewPet
        {
            get { return _newPet; }
            set { SetProperty(ref _newPet, value); }
        }

        private ImageSource _imagePet;

        public ImageSource ImagePet
        {
            get { return _imagePet; }
            set { SetProperty(ref _imagePet, value); }
        }

        private string _textSpecie;

        public string TextSpecie
        {
            get { return _textSpecie; }
            set { SetProperty(ref _textSpecie, value); }
        }

        private string _textLifeStage;

        public string TextLifeStage
        {
            get { return _textLifeStage; }
            set { SetProperty(ref _textLifeStage, value); }
        }

        private string _genderText;

        public string GenderText
        {
            get { return _genderText; }
            set { SetProperty(ref _genderText, value); }
        }


        //Commands
        public Command TakePhotoCommand { get; set; }
        public Command GalleryPhotoCommand { get; set; }
        public Command SpecieCommand { get; set; }
        public Command LifeStageCommand { get; set; }
        public Command PublishCommand { get; set; }
        public Command GenderCommand { get; set; }



        public RegisterPetPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            NewPet = new Pet();
            ImagePet = ImageSource.FromFile("foto.png");

            TextSpecie = "Espécie";
            TextLifeStage = "Idade";
            GenderText = "Sexo";

            TakePhotoCommand = new Command(async () => await ExecuteTakePhotoCommand(), () => true);
            GalleryPhotoCommand = new Command(async () => await ExecuteGalleryPhotoCommand(), () => true);
            SpecieCommand = new Command(ExecuteSpecieCommand);
            LifeStageCommand = new Command(ExecuteLifeStageCommand);
            PublishCommand = new Command(ExecutePublishCommand);
            GenderCommand = new Command(ExecuteGenderCommand);
        }

        async void ExecuteGenderCommand()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Qual o sexo do Focinho ?", "Cancelar", null, "Macho", "Fêmea");

            switch (action)
            {
                case "Macho":
                    NewPet.Gender = Pet.GenderTypes.Male;
                    GenderText = action;
                    break;
                case "Fêmea":
                    NewPet.Gender = Pet.GenderTypes.Famele;
                    GenderText = action;
                    break;
            }
        }

        async void ExecutePublishCommand()
        {
            //Valida Foto
            if (NewPet.Photo == null)
            {
                await _dialogService.DisplayAlertAsync("Atenção", "Precisa de uma foto para cadastrar.", "OK");
                return;
            }

            //Valida Descrição
            if (String.IsNullOrEmpty(NewPet.Description))
            {
                await _dialogService.DisplayAlertAsync("Atenção", "Informe uma descrição.", "OK");
                return;
            }

            if (String.IsNullOrEmpty(NewPet.Email) &&
                String.IsNullOrEmpty(NewPet.Phone) &&
                String.IsNullOrEmpty(NewPet.Whatsapp))
            {
                await _dialogService.DisplayAlertAsync("Atenção", "Informe pelo menos uma forma de contato.", "OK");
                return;
            }

            if (NewPet.Gender == 0)
            {
                await _dialogService.DisplayAlertAsync("Atenção", "Informe o sexo do focinho.", "OK");
                return;
            }

            if (NewPet.Specie == 0)
            {
                await _dialogService.DisplayAlertAsync("Atenção", "Informe a espécie do focinho.", "OK");
                return;
            }

            if (NewPet.type == 0)
            {
                await _dialogService.DisplayAlertAsync("Atenção", "Informe a idade do focinho.", "OK");
                return;
            }

            Inicia("Cadastrando o Focinho...");

            try
            {
                using (APIHelper API = new APIHelper())
                {
                    NewPet.UserId = App.UsuarioLogado.Id;
                    NewPet.Latitude = App.Latitude;
                    NewPet.Longitude = App.Longitude;

                    API.HeadersRequest.Add("widthscreen", App.LarguraTela.ToString());
                    PostGravado = await API.POST<Pet>("api/pets", NewPet);

                    Finaliza();
                    await _dialogService.DisplayAlertAsync("Cadastrado!", "Focinho cadastrado com sucesso.", "OK");

                    await _navigationService.GoBackAsync();
                }
            }
            catch (Exception)
            {
                Finaliza();
            }
        }

        async void ExecuteSpecieCommand()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Qual a espécie do Focinho ?", "Cancelar", null, "Cachorro", "Gato");

            switch (action)
            {
                case "Cachorro":
                    NewPet.Specie = Pet.SpecieAnimals.Dog;
                    TextSpecie = action;
                    break;
                case "Gato":
                    NewPet.Specie = Pet.SpecieAnimals.Cat;
                    TextSpecie = action;
                    break;
            }
        }

        async void ExecuteLifeStageCommand()
        {
            var action = await _dialogService.DisplayActionSheetAsync("Qual a idade do Focinho ?", "Cancelar", null, "Filhote (Até 1 ano)", 
                                                                                                                      "Adolescente (De 1 até 3 anos)",
                                                                                                                      "Adulto (De 3 até 8 anos)",
                                                                                                                      "3ª Idade (Mais de 8 anos)");

            switch (action)
            {
                case "Filhote (Até 1 ano)":
                    NewPet.type = Pet.LifeStages.Puppy;
                    TextLifeStage = "Filhote";
                    break;
                case "Adolescente (De 1 até 3 anos)":
                    NewPet.type = Pet.LifeStages.Teenager;
                    TextLifeStage = "Adolescente";
                    break;
                case "Adulto (De 3 até 8 anos)":
                    NewPet.type = Pet.LifeStages.Adult;
                    TextLifeStage = "Adulto";
                    break;
                case "3ª Idade (Mais de 8 anos)":
                    NewPet.type = Pet.LifeStages.Senior;
                    TextLifeStage = "3ª Idade";
                    break;
            }
        }

        async Task ExecuteTakePhotoCommand()
        {
            using (PhotoHelper Photo = new PhotoHelper())
            {
                await Photo.TirarFoto(App.LarguraTela, App.LarguraTela);
                if (Photo.FotoColetada)
                {
                    NewPet.Photo= Photo.ImagemBytes;
                    ImagePet = Photo.GetImageSource();
                }
            }
        }

        async Task ExecuteGalleryPhotoCommand()
        {
            using (PhotoHelper Photo = new PhotoHelper())
            {
                await Photo.BuscarFoto(App.LarguraTela, App.LarguraTela);
                if (Photo.FotoColetada)
                {
                    NewPet.Photo = Photo.ImagemBytes;
                    ImagePet = Photo.GetImageSource();
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (PostGravado != null)
            {
                parameters.Add("new", PostGravado);
            }
        }

        public void OnNavigatedTo(NavigationParameters parameters) { }

        public void OnNavigatingTo(NavigationParameters parameters) { }
    }
}
