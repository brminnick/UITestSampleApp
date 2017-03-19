using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UITestSampleApp
{

	public class App : Application
	{
		#region Constructors
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
		#endregion

		#region Properties
		public static bool XTCAgent { get; set; }
		public static bool IsLoggedIn { get; set; }
		public static string UserName { get; set; }

		public static NavigationPage Navigation { get; set; }
		#endregion

		#region Methods
		protected override void OnStart()
		{
			int majorVersion, minorVersion;

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					majorVersion = 9;
					minorVersion = 0;
					break;

				case Device.Android:
					majorVersion = 4;
					minorVersion = 2;
					break;
					
				default:
					throw new Exception("Platform Not Supported");
			}

			if (DependencyService.Get<IEnvironment>().IsOperatingSystemSupported(majorVersion, minorVersion))
			{
				var listViewPageLink = AppLinkExtensions.CreateAppLink("List View Page", "Open the List View Page", DeepLinkingIdConstants.ListPageId, "icon");
				AppLinks.RegisterLink(listViewPageLink);
			}
		}
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
		#endregion
	}
}


