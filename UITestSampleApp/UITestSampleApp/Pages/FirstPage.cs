using System;

using Xamarin.Forms;

using MyLoginUI;
using MyLoginUI.Views;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
	public class FirstPage : BasePage
	{
		#region Constant Fields
		readonly Button _goButton, _listPageButton;
		#endregion

		#region Constructors
		public FirstPage()
		{
			const string entryTextPaceHolder = "Enter text and click 'Go'";

			var viewModel = new FirstPageViewModel();
			BindingContext = viewModel;

			_goButton = new StyledButton(Borders.Thin, 1)
			{
				Text = "Go",
				AutomationId = AutomationIdConstants.GoButton, // This provides an ID that can be referenced in UITests
			};
			_goButton.SetBinding<FirstPageViewModel>(Button.CommandProperty, vm => vm.GoButtonTapped);

			var textEntry = new StyledEntry(1)
			{

				Placeholder = entryTextPaceHolder,
				AutomationId = AutomationIdConstants.TextEntry, // This provides an ID that can be referenced in UITests
				PlaceholderColor = Color.FromHex("749FA8"),
			};
			textEntry.SetBinding<FirstPageViewModel>(Entry.TextProperty, vm => vm.EntryText);

			var textLabel = new StyledLabel
			{
				AutomationId = AutomationIdConstants.TextLabel, // This provides an ID that can be referenced in UITests
				HorizontalOptions = LayoutOptions.Center
			};
			textLabel.SetBinding<FirstPageViewModel>(Label.TextProperty, vm => vm.LabelText);

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
			activityIndicator.SetBinding<FirstPageViewModel>(ActivityIndicator.IsVisibleProperty, vm => vm.IsActiityIndicatorRunning);
			activityIndicator.SetBinding<FirstPageViewModel>(ActivityIndicator.IsRunningProperty, vm => vm.IsActiityIndicatorRunning);

			var stackLayout = new StackLayout
			{
				Children = {
					textEntry,
					_goButton,
					activityIndicator,
					textLabel,
				},
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand

			};

			var relativeLayout = new RelativeLayout();
			relativeLayout.Children.Add(stackLayout,
				Constraint.RelativeToParent((parent) =>
				{
					return parent.X;
				}),
				Constraint.RelativeToParent((parent) =>
				{
					return parent.Y;
				}),
				Constraint.RelativeToParent((parent) =>
				{
					return parent.Width - 20;
				}),
				Constraint.RelativeToParent((parent) =>
				{
					return parent.Height / 2;
				})
			);

			relativeLayout.Children.Add(_listPageButton,
				Constraint.RelativeToParent((parent) =>
				{
					return parent.X;
				}),
				Constraint.Constant(250),
				Constraint.RelativeToParent((parent) =>
				{
					return parent.Width - 20;
				})
			);

			Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
			Title = "First Page";
			Content = relativeLayout;
		}
		#endregion

		#region Methods
		protected override void OnAppearing()
		{
			base.OnAppearing();
			AnalyticsHelpers.TrackEvent(AnalyticsConstants.FirstPageOnAppeared);

			_goButton.Clicked += HandleButtonClicked;
			_listPageButton.Clicked += HandleListPageButtonClicked;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			_goButton.Clicked -= HandleButtonClicked;
			_listPageButton.Clicked -= HandleListPageButtonClicked;
		}

		void HandleButtonClicked(object sender, EventArgs e)
		{
			var goButton = sender as Button;
			Device.BeginInvokeOnMainThread(goButton.Unfocus);
		}

		async void HandleListPageButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ListPage());
		}
		#endregion
	}
}


