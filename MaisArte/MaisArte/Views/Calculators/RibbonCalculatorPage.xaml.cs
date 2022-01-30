using MaisArte.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaisArte.Views.Calculators
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RibbonCalculatorPage : ContentPage
    {
        public RibbonCalculatorPage()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            BindingContext = new RibbonCalculatorViewModel();
            _ = (RibbonCalculatorViewModel)BindingContext;
        }
    }
}