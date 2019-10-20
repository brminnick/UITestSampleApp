using System;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Shared;
using System.Linq;

namespace UITestSampleApp
{
    public class ListPage : BaseContentPage<ListViewModel>
    {
        readonly RefreshView _collectionRefreshView;

        public ListPage() : base(PageTitleConstants.ListPage)
        {
            var collectionView = new CollectionView
            {
                ItemTemplate = new ListPageDataTemplate(),
                BackgroundColor = Color.FromHex("#2980b9"),
                SelectionMode = SelectionMode.Single
            };
            collectionView.SelectionChanged += HandleCollectionViewSelectionChanged;
            collectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(ListViewModel.DataList));

            _collectionRefreshView = new RefreshView
            {
                RefreshColor = Device.RuntimePlatform is Device.iOS ? Color.White : Color.Black,
                Content = collectionView
            };
            _collectionRefreshView.SetBinding(RefreshView.IsRefreshingProperty, nameof(ListViewModel.IsRefreshing));
            _collectionRefreshView.SetBinding(RefreshView.CommandProperty, nameof(ListViewModel.PullToRefreshCommand));

            Content = _collectionRefreshView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AppCenterHelpers.TrackEvent(AppCenterConstants.ListViewPageAppeared);

            _collectionRefreshView.IsRefreshing = true;
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


