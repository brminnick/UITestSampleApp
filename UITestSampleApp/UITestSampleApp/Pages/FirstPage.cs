using System;

using Xamarin.Forms;

using MyLoginUI;
using MyLoginUI.Views;

using EntryCustomReturn.Forms.Plugin.Abstractions;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
	public class FirstPage : BaseContentPage<FirstViewModel>
	{
		#region Constant Fields
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
			_goButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GoButtonTapped));

			var textEntry = new StyledEntry(1)
			{
				Placeholder = entryTextPaceHolder,
				AutomationId = AutomationIdConstants.TextEntry, // This provides an ID that can be referenced in UITests
				PlaceholderColor = Color.FromHex("749FA8"),
			};
			CustomReturnEffect.SetReturnType(textEntry, ReturnType.Go);
			textEntry.SetBinding(CustomReturnEffect.ReturnCommandProperty, nameof(ViewModel.GoButtonTapped));
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


