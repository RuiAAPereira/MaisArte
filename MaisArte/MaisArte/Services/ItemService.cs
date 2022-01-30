using Android.Database.Sqlite;
using Android.Util;
using MaisArte.Helpers;
using MaisArte.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaisArte.Services
{
    public class ItemService : IItemService
    {
        private readonly DatabaseHelper _databaseHelper;

        public ItemService()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public async Task AddItem(Guid workId, string title,
            string description, double price)
        {
            var item = new Item
            {
                WorkId = workId,
                Title = title,
                Description = description,
                Price = price
            };

            await _databaseHelper.db.InsertAsync(item);
            await UpdateCost(workId);
        }

        public async Task<Item> GetItem(Guid id)
        {
            var item = await _databaseHelper.db.Table<Item>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            var items = await _databaseHelper.db.Table<Item>().ToListAsync();
            return items;
        }

        public async Task<IEnumerable<Item>> GetItemsByWork(Guid workId)
        {
            var items = await _databaseHelper.db.Table<Item>()
                .Where(i => i.WorkId == workId)
                .ToListAsync();
            return items;
        }

        public async Task<bool> RemoveItem(Guid id, Guid workId)
        {
            int amountOfAffectedRows = await _databaseHelper.db
                .DeleteAsync<Item>(id);
            if (amountOfAffectedRows > 0)
            {
                await UpdateCost(workId);
                return true;
            }

            return false;
        }

        public Task UpdateItem(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateCost(Guid id)
        {
            try
            {
                var cost = await _databaseHelper.db
                    .ExecuteScalarAsync<double>
                    ($"SELECT SUM(Price) FROM Item WHERE WorkId=?", id);

                var work = await _databaseHelper.db.Table<Work>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
                if (work != null)
                {
                    work.Price = cost;

                    await _databaseHelper.db.UpdateAsync(work);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }
    }
}