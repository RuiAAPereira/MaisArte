using MvvmHelpers.Commands;
using System;
using System.Windows.Input;

namespace MaisArte.ViewModels
{
    public class FillingCalculatorViewModel : ViewModelBase
    {
        private decimal _preco = 0;
        private decimal _peso = 0;
        private decimal _pesoGasto = 0;

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

        public ICommand CalcularCommand { get; private set; }

        public FillingCalculatorViewModel()
        {
            CalcularCommand = new Command(CalcularCommandExecute);
        }

        private void CalcularCommandExecute()
        {
            if (Peso <= 0)
                return;
            decimal _precoPorGrama = Preco / Peso;
            decimal _resultado = _precoPorGrama * PesoGasto;
            var _custo = Math.Round(_resultado, 2);

            Resultado = $"O valor gasto foi de {_custo}€";
        }
    }
}