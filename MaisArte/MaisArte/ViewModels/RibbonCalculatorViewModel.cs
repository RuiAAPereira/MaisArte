using MvvmHelpers.Commands;
using System;
using System.Windows.Input;

namespace MaisArte.ViewModels
{
    public class RibbonCalculatorViewModel : ViewModelBase
    {
        private decimal _price = 0;
        private decimal _lenght = 0;
        private decimal _lenghtSpent = 0;

        private string _resultado = string.Empty;

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

        public ICommand CalcularCommand { get; private set; }

        public RibbonCalculatorViewModel()
        {
            CalcularCommand = new Command(CalcularCommandExecute);
        }

        private void CalcularCommandExecute()
        {
            if (Lenght <= 0)
                return;
            decimal _pircePerCm = Price / Lenght;
            decimal _result = _pircePerCm * LenghtSpent;
            var _cost = Math.Round(_result, 2);

            Resultado = $"O valor gasto foi de {_cost}€";
        }
    }
}