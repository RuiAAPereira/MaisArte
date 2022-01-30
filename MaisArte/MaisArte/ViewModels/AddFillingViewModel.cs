using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;

namespace MaisArte.ViewModels
{
    public class AddFillingViewModel : ViewModelBase
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

        private decimal _preco = 0;
        private decimal _peso = 0;
        private decimal _pesoGasto = 0;
        private string _resultado = string.Empty;
        private double _custo = 0;

        public decimal Preco
        {
            get { return _preco; }
            set
            {
                if (_preco != value)
                {
                    _preco = value;
                    OnPropertyChanged(nameof(Preco));
                }
            }
        }

        public decimal Peso
        {
            get { return _peso; }
            set
            {
                if (_peso != value)
                {
                    _peso = value;
                    OnPropertyChanged(nameof(Peso));
                }
            }
        }

        public decimal PesoGasto
        {
            get { return _pesoGasto; }
            set
            {
                if (_pesoGasto != value)
                {
                    _pesoGasto = value;
                    OnPropertyChanged(nameof(PesoGasto));
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

        public AddFillingViewModel()
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
            if (Peso <= 0)
                return;
            decimal _precoPorGrama = Preco / Peso;
            decimal _resultado = _precoPorGrama * PesoGasto;
            decimal _total = Math.Round(_resultado, 2);

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
                "Enchimento",
                $"quantidade gasta: {PesoGasto}g",
                Custo);
            await _navigationService.NavigateToItemListPage();
        }
    }
}