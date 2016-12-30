using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{

	public class App : Application
	{
		public static bool XTCAgent;
		public static bool IsLoggedIn;
		public static string UserName;

		public static NavigationPage Navigation;

		public App()
		{
			DependencyService.Register<IDataService, AzureService>();

			var page = new LoginPage { LogoFileImageSource = "xamarin_logo" };
			Navigation = new NavigationPage(page)
			{
				BarBackgroundColor = Color.FromHex("#3498db"),
				BarTextColor = Color.White,
			};

			NavigationPage.SetHasNavigationBar(page, false);
			MainPage = Navigation;
		}

		protected override void OnStart()
		{
			int majorVersion, minorVersion;

			if (Device.OS == TargetPlatform.iOS)
			{
				majorVersion = 9;
				minorVersion = 0;
			}
			else
			{
				majorVersion = 4;
				minorVersion = 2;
			}

			if (DependencyService.Get<IEnvironment>().IsOperatingSystemSupported(majorVersion, minorVersion))
			{
				var listViewPageLink = AppLinkExtensions.CreateAppLink("List View Page", "Open the List View Page", DeepLinkingIdConstants.ListPageId, "icon");
				AppLinks.RegisterLink(listViewPageLink);
			}
		}
		#region Backdoor Methods
#if DEBUG
		public void OpenListViewPageUsingDeepLinking()
		{
			OnAppLinkRequestReceived(new Uri($"{AppLinkExtensions.BaseUrl}{DeepLinkingIdConstants.ListPageId}"));
		}

		public void OpenListViewPageUsingNavigation()
		{
			NavigateToListViewPage();
		}

		public List<ListPageDataModel> GetListPageData()
		{
			ListPage listPage;

			var currentNavigationPage = GetCurrentPage();

			if (currentNavigationPage is ListPage)
				listPage = currentNavigationPage as ListPage;
			else
				return null;

			var listViewModel = listPage.BindingContext as ListViewModel;

			return listViewModel.DataList;
		}
#endif
		#endregion
		protected override void OnAppLinkRequestReceived(Uri uri)
		{
			if (uri.ToString().Equals($"{AppLinkExtensions.BaseUrl}{DeepLinkingIdConstants.ListPageId}"))
			{
				NavigateToListViewPage();
			}

			base.OnAppLinkRequestReceived(uri);
		}

		void NavigateToListViewPage()
		{
			// Navigate to List View Page by recreating the Navigation Stack to mimic the user journey
			Device.BeginInvokeOnMainThread(async () =>
			{
				await Navigation.PopToRootAsync();
				await Navigation.PushAsync(new ListPage());
			});
		}

		Page GetCurrentPage()
		{
			return Current?.MainPage?.Navigation?.NavigationStack?.LastOrDefault();
		}
	}
}


