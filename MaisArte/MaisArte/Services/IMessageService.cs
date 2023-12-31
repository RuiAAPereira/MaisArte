﻿using System.Threading.Tasks;

namespace MaisArte.Services
{
    public interface IMessageService
    {
        Task ShowAsync(string message);

        Task ShowAsync(string title, string message, string text);

        Task ShowAsync(string title, string message, string text1, string text2);

        Task<bool> ShowAsyncBool(string title, string message, string text1, string text2);
    }
}