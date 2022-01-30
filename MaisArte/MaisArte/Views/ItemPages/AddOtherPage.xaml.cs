using MaisArte.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaisArte.Views.ItemPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOtherPage : ContentPage
    {
        public AddOtherPage()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            BindingContext = new AddOtherViewModel();
            _ = (AddOtherViewModel)BindingContext;
        }
    }
}