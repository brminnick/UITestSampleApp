using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

#if DEBUG
using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public static class BackdoorMethodHelpers
    {
        #region Properties
        static Page CurrentPage => Application.Current?.MainPage?.Navigation?.NavigationStack?.LastOrDefault();
        #endregion

        #region Methods
        public static async Task BypassLoginScreen()
        {
            await App.Navigation.PopToRootAsync();
            await App.Navigation.PushAsync(new FirstPage(), false);
        }

        public static async Task OpenListViewPage()
        {
            switch (AppLinkHelpers.IsDeepLinkingSupported)
            {
                case true:
                    var app = Application.Current as App;
                    app.OpenListViewPageUsingDeepLinking();
                    break;
                default:
                    await NavigateToListViewPage();
                    break;
            }
        }

        public static Task NavigateToListViewPage()
        {
            var tcs = new TaskCompletionSource<object>();
            // Navigate to List View Page by recreating the Navigation Stack to mimic the user journey
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new ListPage());

                tcs.SetResult(null);
            });

            return tcs.Task;
        }

        public static string GetListViewPageDataAsBase64String()
        {
            var listPageData = BackdoorMethodHelpers.GetListPageData();

            var listPageDataAsBase64String = ConverterHelpers.SerializeObject(listPageData);

            return listPageDataAsBase64String;
        }

        static List<ListPageDataModel> GetListPageData()
        {
            var currentNavigationPage = CurrentPage;

            if (!(currentNavigationPage is ListPage listPage))
                return null;

            var listViewModel = listPage.BindingContext as ListViewModel;

            return listViewModel.DataList;
        }
        #endregion
    }
}
#endif
