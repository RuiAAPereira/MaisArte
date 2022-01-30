using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MaisArte.Services
{
    internal static class ImageService
    {
        public static async Task<string> SaveToDiskAsync(FileResult photo)
        {
            string newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

            using (Stream stream = await photo.OpenReadAsync())
            using (FileStream newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            return newFile;
        }
    }
}