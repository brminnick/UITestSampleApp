﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UITestSampleApp
{
	public class ListPage : BaseContentPage<ListViewModel>
	{
		#region Constant Fields
		readonly ListView _listView;
        #endregion

        #region Constructors
        public ListPage()
		{
			_listView = new ListView(ListViewCachingStrategy.RecycleElement)
			{
				ItemTemplate = new DataTemplate(typeof(WhiteTextImageCell)),
				BackgroundColor = Color.FromHex("#2980b9"),
				IsPullToRefreshEnabled = true
			};
			_listView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.DataList));
			_listView.SetBinding(ListView.RefreshCommandProperty, nameof(ViewModel.PullToRefreshCommand));

			Title = "List Page";

			Content = _listView;
		}
		#endregion

		#region Methods
		protected override void OnAppearing()
		{
			base.OnAppearing();
			MobileCenterHelpers.TrackEvent(MobileCenterConstants.ListViewPageAppeared);

            Device.BeginInvokeOnMainThread(_listView.BeginRefresh);
		}

        protected override void SubscribeEventHandlers()
        {
			_listView.ItemTapped += HandleListViewItemTapped;
			ViewModel.LoadingDataFromBackendCompleted += HandleLoadingDataFromBackendCompleted;
        }

        protected override void UnsubscribeEventHandlers()
        {
			_listView.ItemTapped -= HandleListViewItemTapped;
			ViewModel.LoadingDataFromBackendCompleted -= HandleLoadingDataFromBackendCompleted;
        }

		async void HandleListViewItemTapped(object sender, ItemTappedEventArgs e)
		{
			var listView = sender as ListView;
			var tappedListPageDataModel = e.Item as ListPageDataModel;

			MobileCenterHelpers.TrackEvent(MobileCenterConstants.ListViewItemTapped,
				new Dictionary<string, string> {
					{ MobileCenterConstants.ListViewItemNumber, tappedListPageDataModel.DetailProperty }
				}
			);

			await DisplayAlert("Number Tapped", $"You Selected Number {tappedListPageDataModel.DetailProperty}", "OK");

			listView.SelectedItem = null;
		}

		void HandleLoadingDataFromBackendCompleted(object sender, EventArgs e)
		{
            Device.BeginInvokeOnMainThread(_listView.EndRefresh);
		}
		#endregion

	}
}


