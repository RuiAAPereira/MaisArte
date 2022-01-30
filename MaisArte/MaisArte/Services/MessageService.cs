using System.Text;
using System.Threading.Tasks;

namespace MaisArte.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Mais Arte", message, "Ok");
        }

        public async Task ShowAsync(string title, string message, string text)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, text);
        }

        public async Task ShowAsync(string title, string message, string text1, string text2)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, text1, text2);
        }

        public async Task<bool> ShowAsyncBool(string title, string message, string text1, string text2)
        {
            return await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, text1, text2);
        }
    }
}