using MaisArte.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MaisArte.Services
{
    public interface IWorkService
    {
        Task<IEnumerable<Work>> GetWorks();

        Task AddWork(Guid categoryId,
            string title,
            string description,
            double price,
            FileResult photo);

        Task<bool> RemoveWork(Guid id);

        Task<Work> GetWork(Guid id);

        Task<IEnumerable<Work>> GetWorksByCategory(Guid categoryId);

        Task<IEnumerable<Work>> GetFavoriteWorks(bool favorite);

        Task UpdateWork(Work work);
    }
}