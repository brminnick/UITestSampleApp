using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
	public class ListPage : BasePage
	{
		#region Constant Fields
		readonly ListView _listView;
		#endregion

		public ListPage()
		{
			var viewModel = new ListViewModel();
			BindingContext = viewModel;

			Title = "List Page";

			_listView = new ListView
			{
				ItemTemplate = new DataTemplate(typeof(WhiteTextImageCell)),
				BackgroundColor = Color.FromHex("#2980b9")
			};
			_listView.SetBinding(ListView.ItemsSourceProperty, "DataList");

			Content = _listView;
		}

		#region Methods
		protected override void OnAppearing()
		{
			base.OnAppearing();
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.LIST_VIEW_PAGE_ON_APPEARING);

			_listView.ItemTapped += HandleListViewItemTapped;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			_listView.ItemTapped -= HandleListViewItemTapped;
		}

		void HandleListViewItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as SampleDataModel;

			AnalyticsHelpers.TrackEvent(AnalyticsConstants.LIST_VIEW_ITEM_TAPPED,
				new Dictionary<string, string> {
					{ AnalyticsConstants.LIST_VIEW_ITEM_NUMBER, item.Number.ToString() }
				}
			);

			DisplayAlert("Number Tapped", $"You Selected Number {item.Number.ToString()}", "OK");
		}
		#endregion
	}
}


