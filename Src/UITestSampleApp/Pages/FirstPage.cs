using System;

using Xamarin.Forms;

using MyLoginUI;
using MyLoginUI.Views;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class FirstPage : BaseContentPage<FirstViewModel>
    {
        #region Constant Fields
        const int _relativeLayoutPadding = 5;
        #endregion

        #region Constructors
        public FirstPage() : base(PageTitleConstants.FirstPage)
        {
            const string entryTextPaceHolder = "Enter text and click 'Go'";

            var goButton = new StyledButton(Borders.Thin, 1)
            {
                Text = "Go",
                AutomationId = AutomationIdConstants.FirstPage_GoButton // This provides an ID that can be referenced in UITests
            };
            goButton.Clicked += HandleButtonClicked;
            goButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoButtonCommand));
            goButton.SetBinding(Button.CommandParameterProperty, nameof(ViewModel.EntryText));

            var textEntry = new StyledEntry(1)
            {
                Placeholder = entryTextPaceHolder,
                AutomationId = AutomationIdConstants.FirstPage_TextEntry, // This provides an ID that can be referenced in UITests
                PlaceholderColor = Color.FromHex("749FA8"),
                HorizontalTextAlignment = TextAlignment.Center,
                ReturnType = ReturnType.Go
            };
            textEntry.SetBinding(Entry.TextProperty, nameof(ViewModel.EntryText));
            textEntry.SetBinding(Entry.ReturnCommandProperty, nameof(ViewModel.GoButtonCommand));
            textEntry.SetBinding(Entry.ReturnCommandParameterProperty, nameof(ViewModel.EntryText));

            var textLabel = new StyledLabel
            {
                AutomationId = AutomationIdConstants.FirstPage_TextLabel, // This provides an ID that can be referenced in UITests
                HorizontalOptions = LayoutOptions.Center
            };
            textLabel.SetBinding(Label.TextProperty, nameof(ViewModel.LabelText));

            var listPageButton = new StyledButton(Borders.Thin, 1)
            {
                Text = "Go to List Page",
                AutomationId = AutomationIdConstants.FirstPage_ListViewButton,// This provides an ID that can be referenced in UITests
            };
            listPageButton.Clicked += HandleListPageButtonClicked;

            var activityIndicator = new ActivityIndicator
            {
                AutomationId = AutomationIdConstants.FirstPage_BusyActivityIndicator, // This provides an ID that can be referenced in UITests
                Color = Color.White
            };
            activityIndicator.SetBinding(IsVisibleProperty, nameof(ViewModel.IsActiityIndicatorRunning));
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(ViewModel.IsActiityIndicatorRunning));

            Func<RelativeLayout, double> getTextEntryWidth = (p) => textEntry.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getGoButtonWidth = (p) => goButton.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getActivityIndicatorWidth = (p) => activityIndicator.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getTextLabelWidth = (p) => textLabel.Measure(p.Width, p.Height).Request.Width;

            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(textEntry,
                                        Constraint.RelativeToParent(parent => parent.X),
                                                Constraint.RelativeToParent(parent => parent.Y),
                                                Constraint.RelativeToParent(parent => parent.Width - 20));
            relativeLayout.Children.Add(goButton,
                                        Constraint.RelativeToParent(parent => parent.X),
                                        Constraint.RelativeToView(textEntry, (parent, view) => view.Y + view.Height + _relativeLayoutPadding),
                                        Constraint.RelativeToParent(parent => parent.Width - 20));
            relativeLayout.Children.Add(activityIndicator,
                                        Constraint.RelativeToParent(parent => parent.Width / 2 - getActivityIndicatorWidth(parent) / 2),
                                        Constraint.RelativeToView(goButton, (parent, view) => view.Y + view.Height + _relativeLayoutPadding));
            relativeLayout.Children.Add(textLabel,
                                        Constraint.RelativeToParent(parent => parent.Width / 2 - getTextLabelWidth(parent) / 2),
                                        Constraint.RelativeToView(goButton, (parent, view) => view.Y + view.Height + _relativeLayoutPadding));
            relativeLayout.Children.Add(listPageButton,
                                        Constraint.RelativeToParent(parent => parent.X),
                                        Constraint.RelativeToView(goButton, (parent, view) => view.Y + view.Height + _relativeLayoutPadding * 15),
                                        Constraint.RelativeToParent(parent => parent.Width - 20));

            Padding = GetPagePadding();
            Content = relativeLayout;
        }
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            AppCenterHelpers.TrackEvent(AppCenterConstants.FirstPageOnAppeared);
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
                    throw new NotSupportedException("Runtime Platform Not Supported");
            }
        }
        #endregion
    }
}


