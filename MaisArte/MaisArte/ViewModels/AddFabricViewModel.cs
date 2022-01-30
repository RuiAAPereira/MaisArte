using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;

namespace MaisArte.ViewModels
{
    public class AddFabricViewModel : ViewModelBase
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

        public Command CalcularCommand { get; private set; }
        public AsyncCommand AddCommand { get; private set; }

        public AddFabricViewModel()
        {
            Title = "Adicionar tecido";

            _selectedService = new SelectedService();
            _itemService = new ItemService();

            IsButtonAddVisible = false;
            IsButtonCalculateVisible = true;

            CalcularCommand = new Command(CalcularCommandExecute);
            AddCommand = new AsyncCommand(Add);
        }

        private decimal _preco = 0;
        private decimal _largura = 0;
        private decimal _altura = 0;
        private decimal _larguraGasta = 0;
        private decimal _alturaGasta = 0;
        private double _custo = 0;

        private string _resultado = string.Empty;

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

        public decimal Largura
        {
            get { return _largura; }
            set
            {
                if (_largura != value)
                {
                    _largura = value;
                    OnPropertyChanged(nameof(Largura));
                }
            }
        }

        public decimal Altura
        {
            get { return _altura; }
            set
            {
                if (_altura != value)
                {
                    _altura = value;
                    OnPropertyChanged(nameof(Altura));
                }
            }
        }

        public decimal LarguraGasta
        {
            get { return _larguraGasta; }
            set
            {
                if (_larguraGasta != value)
                {
                    _larguraGasta = value;
                    OnPropertyChanged(nameof(LarguraGasta));
                }
            }
        }

        public decimal AlturaGasta
        {
            get { return _alturaGasta; }
            set
            {
                if (_alturaGasta != value)
                {
                    _alturaGasta = value;
                    OnPropertyChanged(nameof(AlturaGasta));
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

        private void CalcularCommandExecute()
        {
            decimal _areaDoTecidoComprado = Largura * Altura;
            if (_areaDoTecidoComprado <= 0)
                return;
            decimal _precoMetroQuadrado = Preco / _areaDoTecidoComprado;
            decimal _areaDoTecidoGasto = LarguraGasta * AlturaGasta;
            decimal _resultado = _precoMetroQuadrado * _areaDoTecidoGasto;
            var _total = Math.Round(_resultado, 2);

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
                "Tecido",
                $"quantidade gasta: {LarguraGasta}cm x {AlturaGasta}cm",
                Custo);
            await _navigationService.NavigateToItemListPage();
        }
    }
}