using System;

using Xamarin.Forms;

using MyLoginUI;
using MyLoginUI.Views;

using UITestSampleApp.Shared;
using Xamarin.Forms.Markup;
using Xamarin.Essentials;

namespace UITestSampleApp
{
    public class FirstPage : BaseContentPage<FirstViewModel>
    {
        public FirstPage() : base(PageTitleConstants.FirstPage)
        {
            const string entryTextPaceHolder = "Enter text and click 'Go'";

            Padding = GetPagePadding();

            Content = new StackLayout
            {
                Spacing = 12,

                Children =
                {
                    new StyledEntry(1)
                    {
                        Placeholder = entryTextPaceHolder,
                        AutomationId = AutomationIdConstants.FirstPage_TextEntry, // This provides an ID that can be referenced in UITests
                        HorizontalTextAlignment = TextAlignment.Center,
                        ReturnType = ReturnType.Go
                    }.FillExpandHorizontal()
                     .Bind(Entry.TextProperty, nameof(FirstViewModel.EntryText))
                     .Bind(Entry.ReturnCommandProperty, nameof(FirstViewModel.GoButtonCommand))
                     .Bind(Entry.ReturnCommandParameterProperty, nameof(FirstViewModel.EntryText)),

                    new StyledButton(Borders.Thin, 1)
                    {
                        Text = "Go",
                        AutomationId = AutomationIdConstants.FirstPage_GoButton // This provides an ID that can be referenced in UITests
                    }.FillExpandHorizontal()
                     .Assign(out Button goButton)
                     .Bind(Button.CommandProperty, nameof(FirstViewModel.GoButtonCommand))
                     .Bind(Button.CommandParameterProperty, nameof(FirstViewModel.EntryText)),

                    new StyledLabel
                    {
                        AutomationId = AutomationIdConstants.FirstPage_TextLabel, // This provides an ID that can be referenced in UITests
                        HorizontalOptions = LayoutOptions.Center
                    }.FillExpandHorizontal().TextCenter()
                     .Bind(Label.TextProperty, nameof(FirstViewModel.LabelText)),

                    new StyledButton(Borders.Thin, 1)
                    {
                        Text = "Go to List Page",
                        AutomationId = AutomationIdConstants.FirstPage_ListViewButton,// This provides an ID that can be referenced in UITests
                    }.FillExpandHorizontal()
                     .Assign(out Button listPageButton),

                    new ActivityIndicator
                    {
                        AutomationId = AutomationIdConstants.FirstPage_BusyActivityIndicator, // This provides an ID that can be referenced in UITests
                        Color = Color.White
                    }.Center()
                     .Bind(IsVisibleProperty, nameof(FirstViewModel.IsActiityIndicatorRunning))
                     .Bind(ActivityIndicator.IsRunningProperty, nameof(FirstViewModel.IsActiityIndicatorRunning))
                }
            }.Top();

            listPageButton.Clicked += HandleListPageButtonClicked;
            goButton.Clicked += HandleButtonClicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AppCenterHelpers.TrackEvent(AppCenterConstants.FirstPageOnAppeared);
        }

        void HandleButtonClicked(object sender, EventArgs e)
        {
            var goButton = (Button)sender;
            MainThread.BeginInvokeOnMainThread(goButton.Unfocus);
        }

        void HandleListPageButtonClicked(object sender, EventArgs e) =>
            MainThread.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(new ListPage()));

        Thickness GetPagePadding() => Device.RuntimePlatform switch
        {
            Device.iOS => new Thickness(10, 20, 10, 5),
            Device.Android => new Thickness(10, 0, 10, 5),
            _ => throw new NotSupportedException("Runtime Platform Not Supported"),
        };
    }
}


