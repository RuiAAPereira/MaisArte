using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers.Commands;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MaisArte.ViewModels.CategoryViewModels
{
    public class EditCategoryViewModel : ViewModelBase
    {
        public Category CurrentCategory { get; set; }

        private Guid _id;
        private string _title;
        private string _description;
        private ImageSource _image;
        public FileResult MyFileResult { get; set; }

        #region currentCategory

        public Guid CategoryId
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(CategoryId));
                }
            }
        }

        public string CategoryTitle
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(CategoryTitle));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public ImageSource SelectedImage
        {
            get => _image;
            set
            {
                if (_image == value)
                    return;
                _image = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        #endregion currentCategory

        public AsyncCommand BottomButtonCommand { get; }
        public AsyncCommand CameraCommand { get; }
        public AsyncCommand SelecionarCommand { get; }

        private string _bottomButtonText;

        public string BottomButtonText
        {
            get { return _bottomButtonText; }
            set
            {
                if (_bottomButtonText != value)
                {
                    _bottomButtonText = value;
                    OnPropertyChanged(nameof(_bottomButtonText));
                }
            }
        }

        public EditCategoryViewModel()
        {
            Title = "Editar categoria";
            BottomButtonText = "Gravar";

            _selectedService = new SelectedService();
            _categoryService = new CategoryService();

            IsBusy = false;

            _ = Task.Run(async () => await Refresh());

            BottomButtonCommand = new AsyncCommand(Edit);
            CameraCommand = new AsyncCommand(CameraCommandExecute);
            SelecionarCommand = new AsyncCommand(SelecionarCommandExecute);
        }

        private async Task Edit()
        {
            if (CategoryTitle == null)
                return;

            Category category = new Category()
            {
                Title = CategoryTitle,
                Description = Description,
                Image = SelectedImage
            };

            await _categoryService.UpdateCategory(category);
            await _navigationService.NavigateToHome();
        }

        private async Task SelecionarCommandExecute()
        {
            MyFileResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Selecione uma foto"
            });
            if (MyFileResult != null)
            {
                Stream stream = await MyFileResult.OpenReadAsync();
                SelectedImage = ImageSource.FromStream(() => { return stream; });
            }
        }

        private async Task CameraCommandExecute()
        {
            MyFileResult = await MediaPicker.CapturePhotoAsync();

            if (MyFileResult != null)
            {
                Stream stream = await MyFileResult.OpenReadAsync();
                SelectedImage = ImageSource.FromStream(() => { return stream; });
            }
        }

        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(100);

            Selected selected = await _selectedService.GetSelected("Category");
            CurrentCategory = await _categoryService.GetCategory(selected.ItemId);

            if (CurrentCategory != null)
            {
                CategoryId = CurrentCategory.Id;
                CategoryTitle = CurrentCategory.Title;
                Description = CurrentCategory.Description;
                SelectedImage = CurrentCategory.Image;
            }

            IsBusy = false;
        }
    }
}