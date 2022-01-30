using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace MaisArte.ViewModels
{
    public class ItemListViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Item> Item { get; set; }

        private bool displayPopup = false;

        public bool DisplayPopup
        {
            get
            {
                return displayPopup;
            }
            set
            {
                displayPopup = value;
                OnPropertyChanged("DisplayPopup");
            }
        }

        #region currentWork

        private Guid _selectedWorkId;

        public Guid SelectedWorkId
        {
            get { return _selectedWorkId; }
            set
            {
                if (_selectedWorkId != value)
                {
                    _selectedWorkId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedWorkTitle;

        public string SelectedWorkTitle
        {
            get { return _selectedWorkTitle; }
            set
            {
                if (_selectedWorkTitle != value)
                {
                    _selectedWorkTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedWorkDescription;

        public string SelectedWorkDescription
        {
            get { return _selectedWorkDescription; }
            set
            {
                if (_selectedWorkDescription != value)
                {
                    _selectedWorkDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        private ImageSource _selectedWorkImage;

        public ImageSource SelectedWorkImage
        {
            get { return _selectedWorkImage; }
            set
            {
                if (_selectedWorkImage != value)
                {
                    _selectedWorkImage = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _selectedWorkPrice;

        public double SelectedWorkPrice
        {
            get { return _selectedWorkPrice; }
            set
            {
                if (_selectedWorkPrice != value)
                {
                    _selectedWorkPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion currentWork

        public ItemListViewModel()
        {
            Title = "Artigos";

            _selectedService = new SelectedService();
            _itemService = new ItemService();
            _workService = new WorkService();

            Item = new ObservableRangeCollection<Item>();

            IsBusy = false;

            _ = Task.Run(async () => await Refresh());

            OpenPopupCommand = new Command(OpenPopup);
            AddFabricCommand = new AsyncCommand(AddFabrics);
            AddFillingCommand = new AsyncCommand(AddFilling);
            AddRibbonCommand = new AsyncCommand(AddRibbon);
            AddOthersCommand = new AsyncCommand(AddOthers);
            DeleteCommand = new AsyncCommand<Guid>(Delete);
        }

        public Command OpenPopupCommand { get; set; }

        private void OpenPopup()
        {
            DisplayPopup = true;
        }

        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(100);

            Item.Clear();

            Selected selectedWork = await _selectedService.GetSelected("Work");

            var work = await _workService.GetWork(selectedWork.ItemId);
            if (work != null)
            {
                SelectedWorkId = work.Id;
                SelectedWorkTitle = work.Title;
                SelectedWorkDescription = work.Description;
                SelectedWorkImage = work.Image;
                SelectedWorkPrice = work.Price;
            }

            var items = await _itemService.GetItemsByWork(selectedWork.ItemId);

            Item.AddRange(items);

            IsBusy = false;
        }

        public AsyncCommand<Guid> DeleteCommand { get; }

        private async Task Delete(Guid id)
        {
            _ = await _itemService.RemoveItem(id, SelectedWorkId);
            await Refresh();
        }

        public AsyncCommand AddFabricCommand { get; }

        private async Task AddFabrics()
        {
            _ = await _selectedService.AddSelected("Work", SelectedWorkId);
            await _navigationService.NavigateToAddFabrics();
        }

        public AsyncCommand AddFillingCommand { get; }

        private async Task AddFilling()
        {
            _ = await _selectedService.AddSelected("Work", SelectedWorkId);
            await _navigationService.NavigateToAddFillings();
        }

        public AsyncCommand AddRibbonCommand { get; }

        private async Task AddRibbon()
        {
            await _navigationService.NavigateToAddRibbons();
        }

        public AsyncCommand AddOthersCommand { get; }

        private async Task AddOthers()
        {
            await _navigationService.NavigateToAddOthers();
        }
    }
}