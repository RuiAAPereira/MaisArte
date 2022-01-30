using FluentValidation;
using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaisArte.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        public Category _category;
        public IValidator _categoryValidator;
        public ICategoryService _categoryService;
        public IWorkService _workService;
        public IItemService _itemService;
        public ISelectedService _selectedService;

        protected IMessageService _messageService;
        protected INavigationService _navigationService;

        public AsyncCommand GoHomeCommand { get; }
        public AsyncCommand GoToCalculationSelectionCommand { get; }
        public AsyncCommand GoToFabricsCalculatorCommand { get; }
        public AsyncCommand GoToFillingCalculatorCommand { get; }
        public AsyncCommand GoToRibbonCalculatorCommand { get; }
        public AsyncCommand GoToFavoriteWorkCommand { get; }

        public ViewModelBase()
        {
            _messageService = DependencyService.Get<IMessageService>();
            _navigationService = DependencyService.Get<INavigationService>();

            GoHomeCommand = new AsyncCommand(GoHome);
            GoToCalculationSelectionCommand = new AsyncCommand(GoToCalculationSelection);
            GoToFabricsCalculatorCommand = new AsyncCommand(GoToFabricsCalculatorPage);
            GoToFillingCalculatorCommand = new AsyncCommand(GoToFillingCalculatorPage);
            GoToRibbonCalculatorCommand = new AsyncCommand(GoToRibbonCalculatorPage);
            GoToFavoriteWorkCommand = new AsyncCommand(GoToFavoriteWorkPage);
        }

        private async Task GoHome()
        {
            await _navigationService.NavigateToHome();
        }

        private async Task GoToCalculationSelection()
        {
            await _navigationService.NavigateToCalcutationSelection();
        }

        private async Task GoToFabricsCalculatorPage()
        {
            await _navigationService.NavigateToFabricsCalculator();
        }

        private async Task GoToFillingCalculatorPage()
        {
            await _navigationService.NavigateToFillingCalculator();
        }

        private async Task GoToRibbonCalculatorPage()
        {
            await _navigationService.NavigateToRibbonCalculator();
        }

        private async Task GoToFavoriteWorkPage()
        {
            await _navigationService.NavigateToFavoriteWorkPage();
        }
    }
}