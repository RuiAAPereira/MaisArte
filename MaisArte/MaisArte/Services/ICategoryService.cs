using MaisArte.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MaisArte.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();

        Task AddCategory(string title,
            string description,
            FileResult photo);

        Task<bool> RemoveCategory(Guid id);

        Task<Category> GetCategory(Guid id);

        Task<bool> UpdateCategory(Category category);
    }
}