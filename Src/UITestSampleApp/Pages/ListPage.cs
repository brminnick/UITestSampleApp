using System;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class ListPage : BaseContentPage<ListViewModel>
    {
        readonly ListView _listView;

        public ListPage() : base(PageTitleConstants.ListPage)
        {
            _listView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = new DataTemplate(typeof(WhiteTextImageCell)),
                BackgroundColor = Color.FromHex("#2980b9"),
                IsPullToRefreshEnabled = true,
                RefreshControlColor = Device.RuntimePlatform is Device.iOS ? Color.White : Color.Black
            };
            _listView.ItemTapped += HandleListViewItemTapped;
            _listView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.DataList));
            _listView.SetBinding(ListView.IsRefreshingProperty, nameof(ViewModel.IsRefreshing));
            _listView.SetBinding(ListView.RefreshCommandProperty, nameof(ViewModel.PullToRefreshCommand));

            Content = _listView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppCenterHelpers.TrackEvent(AppCenterConstants.ListViewPageAppeared);

            Device.BeginInvokeOnMainThread(_listView.BeginRefresh);
        }

        async void HandleListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView listView)
            {
                if (e?.Item is ListPageDataModel tappedListPageDataModel)
                {
                    AppCenterHelpers.TrackEvent(AppCenterConstants.ListViewItemTapped,
                        new Dictionary<string, string> {
                    { AppCenterConstants.ListViewItemNumber, tappedListPageDataModel.Detail.ToString() }
                        }
                    );

                    await DisplayAlert("Number Tapped", $"You Selected Number {tappedListPageDataModel.Detail}", "OK");
                }

                listView.SelectedItem = null;
            }
        }

        class WhiteTextImageCell : ImageCell
        {
            public WhiteTextImageCell()
            {
                TextColor = Color.White;
                DetailColor = Color.White;
                ImageSource = "Hash";

                this.SetBinding(DetailProperty, nameof(ListPageDataModel.Detail));
                this.SetBinding(TextProperty, nameof(ListPageDataModel.Text));
            }
        }
    }
}


