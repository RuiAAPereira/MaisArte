using MaisArte.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaisArte.Views.ItemPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRibbonPage : ContentPage
    {
        public AddRibbonPage()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            BindingContext = new AddRibbonViewModel();
            _ = (AddRibbonViewModel)BindingContext;
        }
    }
}