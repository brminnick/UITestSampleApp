using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public static class BackdoorMethodHelpers
    {
        static Page CurrentPage => GetCurrentPage();

#if DEBUG
        public static void BypassLoginScreen() => Application.Current.MainPage.Navigation.PopAsync();

        public static Task OpenListViewPage() => NavigateToListViewPage();

        public static string GetSerializedListViewPageData()
        {
            var listPageData = GetListPageData();

            var listPageDataAsBase64String = ConverterHelpers.SerializeObject(listPageData);

            return listPageDataAsBase64String;
        }
#endif

        internal static Task NavigateToListViewPage()
        {
            // Navigate to List View Page by recreating the Navigation Stack to mimic the user journey
            return Device.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new ListPage());
            });
        }
#if DEBUG

        static IEnumerable<ListPageDataModel> GetListPageData()
        {
            if (CurrentPage is ListPage listPage
                && listPage.BindingContext is ListViewModel listViewModel)
            {
                return listViewModel.DataList;
            }

            return Enumerable.Empty<ListPageDataModel>();
        }

#endif
        static Page GetCurrentPage()
        {
            if (Application.Current.MainPage.Navigation.ModalStack.Any())
                return Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();

            return Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
        }
    }
}
