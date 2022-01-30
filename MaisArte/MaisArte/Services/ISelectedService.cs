using MaisArte.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaisArte.Services
{
    public interface ISelectedService
    {
        Task<Selected> GetSelected(string item);

        Task<bool> AddSelected(string item, Guid itemId);

        Task<bool> UpdateSelected(string item, Guid itemId);

        Task<bool> RemoveSelected(Selected selected);
    }
}