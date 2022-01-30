using MaisArte.Models;
using SQLite;
using System.IO;
using Xamarin.Essentials;

namespace MaisArte.Helpers
{
    public class DatabaseHelper
    {
        public SQLiteAsyncConnection db;

        public const string DbFileName = "MaisArteDb.db";

        public DatabaseHelper()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory,
                DbFileName);

            db = new SQLiteAsyncConnection(databasePath);

            db.CreateTableAsync<Category>();
            db.CreateTableAsync<Work>();
            db.CreateTableAsync<Selected>();
            db.CreateTableAsync<Item>();
        }
    }
}