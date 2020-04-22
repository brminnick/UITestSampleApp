#if DEBUG
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public static class BackdoorMethodHelpers
    {
        public static void BypassLoginScreen() => Application.Current.MainPage.Navigation.PopAsync();

        public static Task OpenListViewPage() => AppLinkHelpers.NavigateToListViewPage();

        public static IReadOnlyList<ListPageDataModel> GetListViewPageData()
        {
            if (GetCurrentPage() is ListPage listPage
                && listPage.BindingContext is ListViewModel listViewModel)
            {
                return listViewModel.DataList;
            }

            return Enumerable.Empty<ListPageDataModel>().ToList();
        }

        static Page GetCurrentPage()
        {
            if (Application.Current.MainPage.Navigation.ModalStack.Any())
                return Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();

            return Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
        }
    }
}
#endif