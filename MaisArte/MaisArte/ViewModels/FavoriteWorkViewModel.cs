﻿using MaisArte.Models;
using MaisArte.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaisArte.ViewModels
{
    public class FavoriteWorkViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Work> Work { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Guid> RemoveCommand { get; }
        public AsyncCommand<Guid> FavoriteCommand { get; }
        public AsyncCommand<Guid> GoToItemListCommand { get; }

        public FavoriteWorkViewModel()
        {
            Title = "Trabalhos";

            _selectedService = new SelectedService();
            _workService = new WorkService();

            Work = new ObservableRangeCollection<Work>();

            IsBusy = false;

            _ = Task.Run(async () => await Refresh());

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
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

            Work.Clear();

            IEnumerable<Work> works = await _workService.GetFavoriteWorks(true);

            Work.AddRange(works);

            IsBusy = false;
        }
    }
}