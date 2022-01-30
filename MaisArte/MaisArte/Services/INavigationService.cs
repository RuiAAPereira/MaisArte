using System;
using System.Threading.Tasks;

namespace MaisArte.Services
{
    public interface INavigationService
    {
        Task NavigateToHome();

        Task NavigateToAddCategory();

        Task NavigateToEditCategory();

        Task NavigateToWorkListPage();

        Task NavigateToFavoriteWorkPage();

        Task NavigateToAddWork();

        Task NavigateToItemListPage();

        Task NavigateToCalcutationSelection();

        Task NavigateToFabricsCalculator();

        Task NavigateToAddFabrics();

        Task NavigateToFillingCalculator();

        Task NavigateToAddFillings();

        Task NavigateToRibbonCalculator();

        Task NavigateToAddRibbons();

        Task NavigateToAddOthers();

        void PopAsyncService();
    }
}