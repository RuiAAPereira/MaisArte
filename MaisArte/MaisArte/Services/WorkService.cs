using MaisArte.Helpers;
using MaisArte.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MaisArte.Services
{
    public class WorkService : IWorkService
    {
        private readonly DatabaseHelper _databaseHelper;

        public WorkService()
        {
            _databaseHelper = new DatabaseHelper();
        }

        public async Task AddWork(Guid categoryId, string title, string description, double price, FileResult photo)
        {
            string image = "";

            if (photo != null)
            {
                image = await ImageService.SaveToDiskAsync(photo);
            }
            Work work = new Work
            {
                CategoryId = categoryId,
                Title = title,
                Description = description,
                Image = image,
                Price = price
            };

            _ = await _databaseHelper.db.InsertAsync(work);
        }

        public async Task<bool> RemoveWork(Guid id)
        {
            Work work = await _databaseHelper.db.GetAsync<Work>(id);
            string image = work.Image.ToString();

            if (image != string.Empty)
            {
                File.Delete(image);
            }

            int amountOfAffectedRows = await _databaseHelper.db.DeleteAsync<Work>(id);
            if (amountOfAffectedRows > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Work>> GetWorks()
        {
            List<Work> works = await _databaseHelper.db.Table<Work>().ToListAsync();
            return works;
        }

        public async Task<IEnumerable<Work>> GetWorksByCategory(Guid categoryId)
        {
            List<Work> works = await _databaseHelper.db.Table<Work>()
                            .Where(i => i.CategoryId == categoryId)
                            .ToListAsync();
            return works;
        }

        public async Task<Work> GetWork(Guid id)
        {
            Work work = await _databaseHelper.db.Table<Work>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
            return work;
        }

        public async Task UpdateWork(Work work)
        {
            _ = await _databaseHelper.db.UpdateAsync(work);
        }

        public async Task<IEnumerable<Work>> GetFavoriteWorks(bool favorite)
        {
            List<Work> works = await _databaseHelper.db.Table<Work>()
                .Where(i => i.Favorite == favorite)
                .ToListAsync();
            return works;
        }
    }
}