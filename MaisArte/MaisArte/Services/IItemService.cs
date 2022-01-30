using MaisArte.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaisArte.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItems();

        Task AddItem(
            Guid workId,
            string title,
            string description,
            double price);

        Task<bool> RemoveItem(Guid id, Guid workId);

        Task<Item> GetItem(Guid id);

        Task<IEnumerable<Item>> GetItemsByWork(Guid workId);

        Task UpdateItem(Guid id);
    }
}