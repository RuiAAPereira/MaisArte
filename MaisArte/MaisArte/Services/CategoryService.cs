using Android.Util;
using MaisArte.Helpers;
using MaisArte.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MaisArte.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseHelper _databaseHelper;

        public CategoryService()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public async Task AddCategory(string title,
            string description,
            FileResult photo)
        {
            try
            {
                string defaultImage = "noImage.png";
                string image = photo != null ?
                    await ImageService.SaveToDiskAsync(photo) : defaultImage;

                Category category = new Category
                {
                    Title = title,
                    Description = description,
                    Image = image
                };

                _ = await _databaseHelper.db.InsertAsync(category);
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public async Task<bool> RemoveCategory(Guid id)
        {
            try
            {
                Category category = await _databaseHelper.db.GetAsync<Category>(id);
                string image = category.Image.ToString();

                DeleteImage(image);

                int amountOfAffectedRows = await _databaseHelper.db.DeleteAsync<Category>(id);
                if (amountOfAffectedRows > 0)
                {
                    await DeleteWorks(id);
                    return true;
                }

                return false;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteWorks(Guid id)
        {
            try
            {
                List<Work> worksToDelete = await _databaseHelper.db.Table<Work>()
                    .Where(i => i.CategoryId == id)
                    .ToListAsync();
                foreach (Work work in worksToDelete)
                {
                    string workImage = work.Image.ToString();
                    DeleteImage(workImage);

                    await _databaseHelper.db.DeleteAsync<Work>(work.Id);

                    await DeleteItems(work.Id);
                }

                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteItems(Guid id)
        {
            try
            {
                List<Item> itemsToDelete = await _databaseHelper.db.Table<Item>()
                    .Where(i => i.WorkId == id)
                    .ToListAsync();
                foreach (Item item in itemsToDelete)
                {
                    await _databaseHelper.db.DeleteAsync<Item>(item.Id);
                }

                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        private static void DeleteImage(string image)
        {
            if (!string.IsNullOrEmpty(image))
            {
                File.Delete(image);
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = await _databaseHelper.db.Table<Category>().ToListAsync();
                return categories;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public async Task<Category> GetCategory(Guid id)
        {
            try
            {
                Category category = await _databaseHelper.db.Table<Category>()
                                .Where(i => i.Id == id)
                                .FirstOrDefaultAsync();
                return category;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            try
            {
                await _databaseHelper.db.UpdateAsync(category);
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