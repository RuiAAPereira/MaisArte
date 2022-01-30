using MaisArte.Views;
using MaisArte.Views.Calculators;
using MaisArte.Views.CategoryPages;
using MaisArte.Views.ItemPages;
using MaisArte.Views.WorkPages;
using System.Threading.Tasks;

namespace MaisArte.Services
{
    public class NavigationService : INavigationService
    {
        public async Task NavigateToHome()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new HomePage());
        }

        public async Task NavigateToAddCategory()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new AddCategoryPage());
        }

        public async Task NavigateToEditCategory()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new EditCategoryPage());
        }

        public async Task NavigateToCalcutationSelection()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new CalculationSelection());
        }

        public async Task NavigateToWorkListPage()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new WorkListPage());
        }

        public async Task NavigateToFavoriteWorkPage()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new FavoriteWorkPage());
        }

        public async Task NavigateToAddWork()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new AddWorkPage());
        }

        public async Task NavigateToItemListPage()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new ItemListPage());
        }

        public async void PopAsyncService()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PopAsync();
        }

        public async Task NavigateToFabricsCalculator()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new FabricsCalculatorPage());
        }

        public async Task NavigateToAddFabrics()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new AddFabricPage());
        }

        public async Task NavigateToFillingCalculator()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new FillingCalculatorPage());
        }

        public async Task NavigateToAddFillings()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new AddFillingPage());
        }

        public async Task NavigateToRibbonCalculator()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new RibbonCalculatorPage());
        }

        public async Task NavigateToAddRibbons()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new AddRibbonPage());
        }

        public async Task NavigateToAddOthers()
        {
            await Xamarin.Forms.Application.Current
                .MainPage.Navigation.PushAsync(new AddOtherPage());
        }
    }
}