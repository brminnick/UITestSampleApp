using System;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
	public class ListPage : BasePage
	{
		#region Constant Fields
		readonly ListView _listView;
		readonly ListViewModel _viewModel;
		#endregion

		#region Constructors
		public ListPage()
		{
			_viewModel = new ListViewModel();
			BindingContext = _viewModel;

			var loadingAzureDataActivityIndicator = new ActivityIndicator
			{
				AutomationId = AutomationIdConstants.LoadingDataFromBackendActivityIndicator,
				Color = Color.White
			};
			loadingAzureDataActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsDataLoadingFromBackend");
			loadingAzureDataActivityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsDataLoadingFromBackend");

			_listView = new ListView(ListViewCachingStrategy.RetainElement)
			{
				//ToDo Change to ListViewCachingStrategy.RecycleElement once this bug has been fixed https://bugzilla.xamarin.com/show_bug.cgi?id=42678
				ItemTemplate = new DataTemplate(typeof(WhiteTextImageCell)),
				BackgroundColor = Color.FromHex("#2980b9"),
				IsPullToRefreshEnabled = true
			};
			_listView.SetBinding(ListView.ItemsSourceProperty, "DataList");
			_listView.SetBinding(ListView.RefreshCommandProperty, "PullToRefreshCommanded");

			Title = "List Page";

			var relativeLayout = new RelativeLayout();

			Func<RelativeLayout, double> getloadingAzureDataActivityIndicatorWidth = (p) => loadingAzureDataActivityIndicator.Measure(relativeLayout.Width, relativeLayout.Height).Request.Width;
			Func<RelativeLayout, double> getloadingAzureDataActivityIndicatorHeight = (p) => loadingAzureDataActivityIndicator.Measure(relativeLayout.Width, relativeLayout.Height).Request.Height;

			relativeLayout.Children.Add(_listView,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent(parent => parent.Width),
				Constraint.RelativeToParent(parent => parent.Height)
		   	);
			relativeLayout.Children.Add(loadingAzureDataActivityIndicator,
				Constraint.RelativeToParent((parent) => parent.Width / 2 - getloadingAzureDataActivityIndicatorWidth(parent) / 2),
				Constraint.RelativeToParent((parent) => parent.Height / 2 - getloadingAzureDataActivityIndicatorHeight(parent) / 2)
		  	);
			Content = relativeLayout;
		}
		#endregion

		#region Methods
		protected override void OnAppearing()
		{
			base.OnAppearing();
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.ListViewPageAppeared);

			_listView.ItemTapped += HandleListViewItemTapped;
			_viewModel.LoadingDataFromBackendCompleted += HandleLoadingDataFromBackendCompleted;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			_listView.ItemTapped -= HandleListViewItemTapped;
			_viewModel.LoadingDataFromBackendCompleted -= HandleLoadingDataFromBackendCompleted;
		}

		void HandleListViewItemTapped(object sender, ItemTappedEventArgs e)
		{
			var tappedListPageDataModel = e.Item as ListPageDataModel;

			AnalyticsHelpers.TrackEvent(AnalyticsConstants.ListViewItemTapped,
				new Dictionary<string, string> {
					{ AnalyticsConstants.ListViewItemNumber, tappedListPageDataModel.DetailProperty }
				}
			);

			DisplayAlert("Number Tapped", $"You Selected Number {tappedListPageDataModel.DetailProperty}", "OK");
		}

		void HandleLoadingDataFromBackendCompleted(object sender, EventArgs e)
		{
			_listView.EndRefresh();
		}
		#endregion

	}
}


