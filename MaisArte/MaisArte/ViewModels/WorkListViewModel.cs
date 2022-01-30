using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaisArte.ViewModels
{
    public class WorkListViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Work> Works { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand BottomButtonCommand { get; }
        public AsyncCommand<Guid> RemoveCommand { get; }
        public AsyncCommand<Guid> FavoriteCommand { get; }
        public AsyncCommand<Guid> GoToItemListCommand { get; }

        private string _bottomButtonText;

        public string BottomButtonText
        {
            get { return _bottomButtonText; }
            set
            {
                if (_bottomButtonText != value)
                {
                    _bottomButtonText = value;
                    OnPropertyChanged(nameof(_bottomButtonText));
                }
            }
        }

        public WorkListViewModel()
        {
            Title = "Trabalhos";
            BottomButtonText = "Adicionar trabalho";

            _selectedService = new SelectedService();
            _workService = new WorkService();

            Works = new ObservableRangeCollection<Work>();

            IsBusy = false;

            _ = Task.Run(async () => await Refresh());

            RefreshCommand = new AsyncCommand(Refresh);
            BottomButtonCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Guid>(Delete);
            FavoriteCommand = new AsyncCommand<Guid>(AddFavorite);

            GoToItemListCommand = new AsyncCommand<Guid>(GotoItemListPage);
        }

        private async Task Add()
        {
            await _navigationService.NavigateToAddWork();
        }

        private async Task Delete(Guid id)
        {
            bool queryUser = await _messageService.ShowAsyncBool("Apagar?",
                "Deseja apagar este trabalho ? \n\n" +
                "Esta ação não é reversível!", "OK", "Cancelar");
            if (queryUser)
            {
                if (await _workService.RemoveWork(id))
                {
                    await Refresh();
                    await _messageService.ShowAsync("Sucesso", "Trabalho apagado com sucesso!", "Ok");
                }
                else
                {
                    await _messageService.ShowAsync("Erro", "Ocorreu um erro o trabalho não foi apagado!", "Ok");
                }
            }
        }

        private async Task AddFavorite(Guid id)
        {
            Work selected = await _workService.GetWork(id);
            if (selected != null)
            {
                switch (selected.Favorite)
                {
                    case true:
                        selected.Favorite = false;
                        selected.FavoriteIcon = "FavoriteEmpty.png";
                        break;

                    default:
                        selected.Favorite = true;
                        selected.FavoriteIcon = "Favorite.png";
                        break;
                }

                await _workService.UpdateWork(selected);
            }
            await Refresh();
        }

        private async Task GotoItemListPage(Guid id)
        {
            await _selectedService.AddSelected("Work", id);
            await _navigationService.NavigateToItemListPage();
        }

        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(100);

            Works.Clear();

            Selected selectedCategory = await _selectedService.GetSelected("Category");

            IEnumerable<Work> works = await _workService.GetWorksByCategory(selectedCategory.ItemId);

            Works.AddRange(works);

            IsBusy = false;
        }
    }
}