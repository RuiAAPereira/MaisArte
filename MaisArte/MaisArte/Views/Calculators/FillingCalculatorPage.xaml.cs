using MaisArte.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaisArte.Views.Calculators
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillingCalculatorPage : ContentPage
    {
        public FillingCalculatorPage()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            BindingContext = new FillingCalculatorViewModel();
            _ = (FillingCalculatorViewModel)BindingContext;
        }
    }
}