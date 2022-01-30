using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace MaisArte.ViewModels
{
    public class AddOtherViewModel : ViewModelBase
    {
        private string _otherTitle;
        private string _description;
        private double _valueSpent;

        public string OtherTitle
        {
            get { return _otherTitle; }
            set
            {
                if (_otherTitle != value)
                {
                    _otherTitle = value;
                    OnPropertyChanged(nameof(OtherTitle));
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

        public double ValueSpent
        {
            get { return _valueSpent; }
            set
            {
                if (_valueSpent != value)
                {
                    _valueSpent = value;
                    OnPropertyChanged(nameof(ValueSpent));
                }
            }
        }

        public AsyncCommand AddCommand { get; private set; }

        public AddOtherViewModel()
        {
            Title = "Adicionar outros";

            _selectedService = new SelectedService();
            _itemService = new ItemService();

            AddCommand = new AsyncCommand(Add);
        }

        private async Task Add()
        {
            Selected selectedWork = await _selectedService.GetSelected("Work");
            if (selectedWork == null || OtherTitle == null)
            {
                return;
            }

            await _itemService.AddItem(selectedWork.ItemId,
                OtherTitle,
                Description,
                ValueSpent);
            await _navigationService.NavigateToItemListPage();
        }
    }
}