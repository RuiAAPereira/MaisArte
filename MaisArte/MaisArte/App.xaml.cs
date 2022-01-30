using MaisArte.Services;
using MaisArte.Views;
using MaisArte.Views.CategoryPages;
using Xamarin.Forms;

namespace MaisArte
{
    public partial class App : Application
    {
        private readonly ICategoryService _categoryService;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider
                .RegisterLicense("NTYxMjMwQDMxMzkyZTM0MmUzMGVqVjF0OXJFVjlNc2x2U3R1eFlWNmdOdEVpMGlIb1lsRkVhQXJSb3g3aEU9");

            InitializeComponent();

            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<INavigationService, NavigationService>();

            _categoryService = new CategoryService();

            OnAppStart();
        }

        private void OnAppStart()
        {
            var getLocalDB = _categoryService.GetCategories();
            MainPage = getLocalDB != null ? new NavigationPage(new HomePage()) :
                new NavigationPage(new AddCategoryPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}