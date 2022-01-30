using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;

namespace MaisArte.ViewModels
{
    public class AddRibbonViewModel : ViewModelBase
    {
        private bool _isButtonAddVisible;

        public bool IsButtonAddVisible
        {
            get { return _isButtonAddVisible; }
            set
            {
                if (_isButtonAddVisible != value)
                {
                    _isButtonAddVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isButtonCalculateVisible;

        public bool IsButtonCalculateVisible
        {
            get { return _isButtonCalculateVisible; }
            set
            {
                if (_isButtonCalculateVisible != value)
                {
                    _isButtonCalculateVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _price = 0;
        private decimal _lenght = 0;
        private decimal _lenghtSpent = 0;
        private string _resultado = string.Empty;
        private double _custo = 0;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        public decimal Lenght
        {
            get { return _lenght; }
            set
            {
                if (_lenght != value)
                {
                    _lenght = value;
                    OnPropertyChanged(nameof(Lenght));
                }
            }
        }

        public decimal LenghtSpent
        {
            get { return _lenghtSpent; }
            set
            {
                if (_lenghtSpent != value)
                {
                    _lenghtSpent = value;
                    OnPropertyChanged(nameof(LenghtSpent));
                }
            }
        }

        public string Resultado
        {
            get { return _resultado; }
            set
            {
                if (_resultado != value)
                {
                    _resultado = value;
                    OnPropertyChanged(nameof(Resultado));
                }
            }
        }

        public double Custo
        {
            get { return _custo; }
            set
            {
                if (_custo != value)
                {
                    _custo = value;
                    OnPropertyChanged(nameof(Custo));
                }
            }
        }

        public Command CalcularCommand { get; private set; }
        public AsyncCommand AddCommand { get; private set; }

        public AddRibbonViewModel()
        {
            Title = "Adicionar enchimento";

            _selectedService = new SelectedService();
            _itemService = new ItemService();

            IsButtonAddVisible = false;
            IsButtonCalculateVisible = true;

            CalcularCommand = new Command(CalcularCommandExecute);
            AddCommand = new AsyncCommand(Add);
        }

        private void CalcularCommandExecute()
        {
            if (Lenght <= 0)
                return;
            decimal _pircePerCm = Price / Lenght;
            decimal _result = _pircePerCm * LenghtSpent;
            var _total = Math.Round(_result, 2);

            Custo = decimal.ToDouble(_total);

            Resultado = $"O valor gasto foi de {_total}€";

            IsButtonCalculateVisible = false;
            IsButtonAddVisible = true;
        }

        private async Task Add()
        {
            Selected selectedWork = await _selectedService.GetSelected("Work");
            if (selectedWork == null)
                return;

            await _itemService.AddItem(selectedWork.ItemId,
                "Fita",
                $"comprimento gasto: {LenghtSpent}cm",
                Custo);
            await _navigationService.NavigateToItemListPage();
        }
    }
}