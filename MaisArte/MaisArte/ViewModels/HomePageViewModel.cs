using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;

namespace MaisArte.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Category> Categories { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Guid> DeleteCommand { get; }
        public AsyncCommand<Guid> EditCommand { get; }
        public AsyncCommand<Guid> GoToWorkListCommand { get; }
        public AsyncCommand BottomButtonCommand { get; }

        private string _bottomButtonText;

        public string BottomButtonText
        {
            get { return _bottomButtonText; }
            set
            {
                if (_bottomButtonText != value)
                {
                    _bottomButtonText = value;
                    OnPropertyChanged(nameof(BottomButtonText));
                }
            }
        }

        public HomePageViewModel()
        {
            Title = "Início";
            BottomButtonText = "Adicionar categoria";

            _selectedService = new SelectedService();
            _categoryService = new CategoryService();

            Categories = new ObservableRangeCollection<Category>();

            IsBusy = false;

            _ = Task.Run(async () => await Refresh());

            RefreshCommand = new AsyncCommand(Refresh);
            DeleteCommand = new AsyncCommand<Guid>(Delete);
            EditCommand = new AsyncCommand<Guid>(GotoEditPage);
            GoToWorkListCommand = new AsyncCommand<Guid>(GotoWorkListPage);
            BottomButtonCommand = new AsyncCommand(GoToAddCategoryPage);
        }

        private async Task Delete(Guid id)
        {
            bool queryUser = await _messageService.ShowAsyncBool("Apagar?",
                "Deseja apagar esta categoria ? \n" +
                "Todos os trabalhos relacionados serão também apagados. \n" +
                "Esta ação não é reversível!", "OK", "Cancelar");
            if (queryUser)
            {
                if (await _categoryService.RemoveCategory(id))
                {
                    await Refresh();
                    await _messageService.ShowAsync("Sucesso", "Categoria apagada com sucesso!", "Ok");
                }
                else
                {
                    await _messageService.ShowAsync("Erro", "Ocorreu um erro a categoria não foi apagada!", "Ok");
                }
            }
        }

        private async Task GotoWorkListPage(Guid id)
        {
            await _selectedService.AddSelected("Category", id);
            await _navigationService.NavigateToWorkListPage();
        }

        private async Task GotoEditPage(Guid id)
        {
            await _selectedService.AddSelected("Category", id);
            await _navigationService.NavigateToEditCategory();
        }

        private async Task GoToAddCategoryPage()
        {
            await _navigationService.NavigateToAddCategory();
        }

        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(100);

            Categories.Clear();

            var categories = await _categoryService.GetCategories();

            Categories.AddRange(categories);

            IsBusy = false;
        }
    }
}