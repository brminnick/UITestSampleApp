using System;

using Xamarin.Forms;

using MyLoginUI;
using MyLoginUI.Views;

using EntryCustomReturn.Forms.Plugin.Abstractions;

namespace UITestSampleApp
{
    public class FirstPage : BaseContentPage<FirstViewModel>
    {
        #region Constant Fields
        const int _relativeLayoutPadding = 5;
        readonly Button _goButton, _listPageButton;
        #endregion

        #region Constructors
        public FirstPage()
        {
            const string entryTextPaceHolder = "Enter text and click 'Go'";

            _goButton = new StyledButton(Borders.Thin, 1)
            {
                Text = "Go",
                AutomationId = AutomationIdConstants.GoButton, // This provides an ID that can be referenced in UITests
            };
            _goButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoButtonCommand));

            var textEntry = new StyledEntry(1)
            {
                Placeholder = entryTextPaceHolder,
                AutomationId = AutomationIdConstants.TextEntry, // This provides an ID that can be referenced in UITests
                PlaceholderColor = Color.FromHex("749FA8"),
                HorizontalTextAlignment = TextAlignment.Center
            };
            CustomReturnEffect.SetReturnType(textEntry, ReturnType.Go);
            textEntry.SetBinding(CustomReturnEffect.ReturnCommandProperty, nameof(ViewModel.GoButtonCommand));
            textEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.EntryText));

            var textLabel = new StyledLabel
            {
                AutomationId = AutomationIdConstants.TextLabel, // This provides an ID that can be referenced in UITests
                HorizontalOptions = LayoutOptions.Center
            };
            textLabel.SetBinding(Label.TextProperty, nameof(ViewModel.LabelText));

            _listPageButton = new StyledButton(Borders.Thin, 1)
            {
                Text = "Go to List Page",
                AutomationId = AutomationIdConstants.ListViewButton // This provides an ID that can be referenced in UITests
            };

            var activityIndicator = new ActivityIndicator
            {
                AutomationId = AutomationIdConstants.BusyActivityIndicator, // This provides an ID that can be referenced in UITests
                Color = Color.White
            };
            activityIndicator.SetBinding(IsVisibleProperty, nameof(ViewModel.IsActiityIndicatorRunning));
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(ViewModel.IsActiityIndicatorRunning));

            Func<RelativeLayout, double> getTextEntryWidth = (p) => textEntry.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getGoButtonWidth = (p) => _goButton.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getActivityIndicatorWidth = (p) => activityIndicator.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getTextLabelWidth = (p) => textLabel.Measure(p.Width, p.Height).Request.Width;

			var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(textEntry,
                                        Constraint.RelativeToParent((parent) => parent.X),
                                        Constraint.RelativeToParent((parent) => parent.Y),
                                        Constraint.RelativeToParent((parent) => parent.Width - 20));
            relativeLayout.Children.Add(_goButton,
                                        Constraint.RelativeToParent((parent) => parent.X),
                                        Constraint.RelativeToView(textEntry, (parent, view) => view.Y + view.Height + _relativeLayoutPadding),
                                        Constraint.RelativeToParent((parent) => parent.Width - 20));
            relativeLayout.Children.Add(activityIndicator,
                                        Constraint.RelativeToParent((parent) => parent.Width / 2 - getActivityIndicatorWidth(parent) / 2),
                                        Constraint.RelativeToView(_goButton, (parent, view) => view.Y + view.Height + _relativeLayoutPadding));
            relativeLayout.Children.Add(textLabel,
                                        Constraint.RelativeToParent((parent) => parent.Width / 2 - getTextLabelWidth(parent) / 2),
                                        Constraint.RelativeToView(_goButton, (parent, view) => view.Y + view.Height + _relativeLayoutPadding));
            relativeLayout.Children.Add(_listPageButton,
                                        Constraint.RelativeToParent((parent) => parent.X),
                                        Constraint.RelativeToView(_goButton, (parent, view) => view.Y + view.Height + _relativeLayoutPadding * 15),
                                        Constraint.RelativeToParent((parent) => parent.Width - 20));

            Padding = GetPagePadding();
            Title = "First Page";
            Content = relativeLayout;
        }
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MobileCenterHelpers.TrackEvent(MobileCenterConstants.FirstPageOnAppeared);
        }

        protected override void SubscribeEventHandlers()
        {
            _goButton.Clicked += HandleButtonClicked;
            _listPageButton.Clicked += HandleListPageButtonClicked;
        }
        protected override void UnsubscribeEventHandlers()
        {
            _goButton.Clicked -= HandleButtonClicked;
            _listPageButton.Clicked -= HandleListPageButtonClicked;
        }

        void HandleButtonClicked(object sender, EventArgs e)
        {
            var goButton = sender as Button;
            Device.BeginInvokeOnMainThread(goButton.Unfocus);
        }

        void HandleListPageButtonClicked(object sender, EventArgs e) =>
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new ListPage()));

        Thickness GetPagePadding()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return new Thickness(10, 20, 10, 5);
                case Device.Android:
                    return new Thickness(10, 0, 10, 5);
                default:
                    throw new Exception("Platform Not Supported");
            }
        }
        #endregion
    }
}


