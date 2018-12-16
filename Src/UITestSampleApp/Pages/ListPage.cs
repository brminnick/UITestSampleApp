using System;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class ListPage : BaseContentPage<ListViewModel>
    {
        #region Constant Fields
        readonly ListView _listView;
        #endregion

        #region Constructors
        public ListPage() : base(PageTitleConstants.ListPage)
        {
            _listView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = new DataTemplate(typeof(WhiteTextImageCell)),
                BackgroundColor = Color.FromHex("#2980b9"),
                IsPullToRefreshEnabled = true
            };
            _listView.ItemTapped -= HandleListViewItemTapped;
            _listView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.DataList));
			_listView.SetBinding(ListView.IsRefreshingProperty, nameof(ViewModel.IsRefreshing));
            _listView.SetBinding(ListView.RefreshCommandProperty, nameof(ViewModel.PullToRefreshCommand));

            Content = _listView;
        }
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppCenterHelpers.TrackEvent(AppCenterConstants.ListViewPageAppeared);

            Device.BeginInvokeOnMainThread(_listView.BeginRefresh);
        }

        async void HandleListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            var tappedListPageDataModel = e.Item as ListPageDataModel;

            AppCenterHelpers.TrackEvent(AppCenterConstants.ListViewItemTapped,
                new Dictionary<string, string> {
                    { AppCenterConstants.ListViewItemNumber, tappedListPageDataModel.Detail.ToString() }
                }
            );

            await DisplayAlert("Number Tapped", $"You Selected Number {tappedListPageDataModel.Detail}", "OK");

            listView.SelectedItem = null;
        }
        #endregion
    }
}


