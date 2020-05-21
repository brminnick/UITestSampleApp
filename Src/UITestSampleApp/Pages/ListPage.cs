using System;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;
using System.Linq;
using Xamarin.Forms.Markup;
using Xamarin.Essentials;

namespace UITestSampleApp
{
    public class ListPage : BaseContentPage<ListViewModel>
    {
        readonly RefreshView _collectionRefreshView;

        public ListPage() : base(PageTitleConstants.ListPage)
        {
            Content = new RefreshView
            {
                RefreshColor = Device.RuntimePlatform is Device.iOS ? Color.White : Color.Black,

                Content = new CollectionView
                {
                    ItemTemplate = new ListPageDataTemplate(),
                    BackgroundColor = Color.FromHex("#2980b9"),
                    SelectionMode = SelectionMode.Single
                }.Assign(out CollectionView collectionView)
                 .Bind(CollectionView.ItemsSourceProperty, nameof(ListViewModel.DataList))

            }.Assign(out _collectionRefreshView)
             .Bind(RefreshView.IsRefreshingProperty, nameof(ListViewModel.IsRefreshing))
             .Bind(RefreshView.CommandProperty, nameof(ListViewModel.PullToRefreshCommand));

            collectionView.SelectionChanged += HandleCollectionViewSelectionChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AppCenterHelpers.TrackEvent(AppCenterConstants.ListViewPageAppeared);

            await MainThread.InvokeOnMainThreadAsync(() => _collectionRefreshView.IsRefreshing = true);
        }

        async void HandleCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var collectionView = (CollectionView)sender;
            collectionView.SelectedItem = null;

            if (e?.CurrentSelection.FirstOrDefault() is ListPageDataModel tappedListPageDataModel)
            {
                AppCenterHelpers.TrackEvent(AppCenterConstants.ListViewItemTapped,
                    new Dictionary<string, string> { { AppCenterConstants.ListViewItemNumber, tappedListPageDataModel.Detail.ToString() } }
                );

                await DisplayAlert("Number Tapped", $"You Selected Number {tappedListPageDataModel.Detail}", "OK");
            }
        }
    }
}


