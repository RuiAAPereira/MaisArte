using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MaisArte.ViewModels.CategoryViewModels
{
    public class AddCategoryViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Category> Category { get; set; }

        private string _title;
        private string _description;
        private ImageSource _image;
        public FileResult MyFileResult { get; set; }

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

        public AddCategoryViewModel()
        {
            Title = "Adicionar categoria";
            BottomButtonText = "Gravar";

            _categoryService = new CategoryService();

            Category = new ObservableRangeCollection<Category>();

            BottomButtonCommand = new AsyncCommand(Add);
            CameraCommand = new AsyncCommand(CameraCommandExecute);
            SelecionarCommand = new AsyncCommand(SelecionarCommandExecute);
        }

        private async Task Add()
        {
            if (CategoryTitle == null)
                return;

            await _categoryService.AddCategory(CategoryTitle, Description, MyFileResult);
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
    }
}