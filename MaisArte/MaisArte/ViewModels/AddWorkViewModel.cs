using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MaisArte.ViewModels
{
    public class AddWorkViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Work> Work { get; set; }

        private string _title;
        private string _description;
        private ImageSource _image;
        public FileResult MyFileResult { get; set; }

        public string WorkTitle
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(_title));
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
                    OnPropertyChanged(nameof(_description));
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
                OnPropertyChanged();
            }
        }

        public AddWorkViewModel()
        {
            Title = "Adicionar trabalhos";

            _selectedService = new SelectedService();
            _workService = new WorkService();

            Work = new ObservableRangeCollection<Work>();

            AddCommand = new AsyncCommand(Add);
            CameraCommand = new AsyncCommand(CameraCommandExecute);
            SelecionarCommand = new AsyncCommand(SelecionarCommandExecute);
        }

        private async Task Add()
        {
            if (WorkTitle == null)
                return;

            Selected selectedCategory = await _selectedService.GetSelected("Category");
            await _workService.AddWork(selectedCategory.ItemId, WorkTitle, Description, 0, MyFileResult);
            await _navigationService.NavigateToWorkListPage();
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

        public AsyncCommand AddCommand { get; }
        public AsyncCommand CameraCommand { get; }
        public AsyncCommand SelecionarCommand { get; }
    }
}