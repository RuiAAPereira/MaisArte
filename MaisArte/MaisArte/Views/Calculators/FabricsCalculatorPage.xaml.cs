using MaisArte.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaisArte.Views.Calculators
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FabricsCalculatorPage : ContentPage
    {
        public FabricsCalculatorPage()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            BindingContext = new FabricsCalculatorViewModel();
            _ = (FabricsCalculatorViewModel)BindingContext;
        }
    }
}