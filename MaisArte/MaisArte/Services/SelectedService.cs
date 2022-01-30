using Android.Util;
using MaisArte.Helpers;
using MaisArte.Models;
using SQLite;
using System;
using System.Threading.Tasks;

namespace MaisArte.Services
{
    public class SelectedService : ISelectedService
    {
        private readonly DatabaseHelper _databaseHelper;

        public SelectedService()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public async Task<bool> AddSelected(string item, Guid itemId)
        {
            try
            {
                Selected exist = await GetSelected(item);
                if (exist != null)
                {
                    await UpdateSelected(item, itemId);
                }
                else
                {
                    Selected selected = new Selected()
                    {
                        Item = item,
                        ItemId = itemId
                    };
                    await _databaseHelper.db.InsertAsync(selected);
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public async Task<Selected> GetSelected(string item)
        {
            try
            {
                Selected selected = await _databaseHelper.db
                    .Table<Selected>()
                    .Where(p => p.Item == item)
                    .FirstOrDefaultAsync();

                if (selected != null)
                    return selected;

                return null;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public async Task<bool> RemoveSelected(Selected selected)
        {
            try
            {
                await _databaseHelper.db.DeleteAsync(selected);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateSelected(string item, Guid itemId)
        {
            try
            {
                var selected = await GetSelected(item);
                if (selected != null)
                {
                    selected.ItemId = itemId;

                    await _databaseHelper.db.UpdateAsync(selected);
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
    }
}