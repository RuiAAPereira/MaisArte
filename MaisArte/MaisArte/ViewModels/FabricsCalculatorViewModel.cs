using MvvmHelpers.Commands;
using System;
using System.Windows.Input;

namespace MaisArte.ViewModels
{
    public class FabricsCalculatorViewModel : ViewModelBase
    {
        private decimal _preco = 0;
        private decimal _largura = 0;
        private decimal _altura = 0;
        private decimal _larguraGasta = 0;
        private decimal _alturaGasta = 0;

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

        public ICommand CalcularCommand { get; private set; }

        public FabricsCalculatorViewModel()
        {
            CalcularCommand = new Command(CalcularCommandExecute);
        }

        private void CalcularCommandExecute()
        {
            decimal _areaDoTecidoComprado = Largura * Altura;
            if (_areaDoTecidoComprado <= 0)
                return;
            decimal _precoMetroQuadrado = Preco / _areaDoTecidoComprado;
            decimal _areaDoTecidoGasto = LarguraGasta * AlturaGasta;
            decimal _resultado = _precoMetroQuadrado * _areaDoTecidoGasto;
            var _custo = Math.Round(_resultado, 2);

            Resultado = $"O valor gasto foi de {_custo}€";
        }
    }
}