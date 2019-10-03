using System;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

using MyLoginUI.Views;

using UITestSampleApp.Shared;

namespace UITestSampleApp
{
    public class NewUserSignUpPage : ContentPage
    {
        readonly StyledButton _saveUsernameButton, _cancelButton;
        readonly StyledEntry _usernameEntry, _passwordEntry;
        readonly StackLayout _layout;

        public NewUserSignUpPage()
        {
            On<iOS>().SetUseSafeArea(true);

            BackgroundColor = Color.FromHex("#2980b9");

            _layout = new StackLayout
            {
                Padding = new Thickness(20, 50, 20, 20),
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            _usernameEntry = new UnderlinedEntry("Username", AutomationIdConstants.NewUserSignUpPage_NewUserNameEntry)
            {
                ReturnType = ReturnType.Next
            };

            _passwordEntry = new UnderlinedEntry("Password", AutomationIdConstants.NewUserSignUpPage_NewPasswordEntry)
            {
                IsPassword = true,
                ReturnType = ReturnType.Go,
                ReturnCommand = new Command(() => HandleSaveUsernameButtonClicked(_saveUsernameButton, EventArgs.Empty))
            };

            _saveUsernameButton = new BorderedButton("Save Username", AutomationIdConstants.NewUserSignUpPage_SaveUsernameButton)
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            _saveUsernameButton.Clicked += HandleSaveUsernameButtonClicked;

            _cancelButton = new BorderedButton("Cancel", AutomationIdConstants.NewUserSignUpPage_CancelButton)
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            _cancelButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PopModalAsync();
            };

            AddChildrenToParentLayout();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _usernameEntry.Focus();
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            _cancelButton.WidthRequest = width - 40;
            _saveUsernameButton.WidthRequest = width - 40;

            base.LayoutChildren(x, y, width, height);
        }

        void AddChildrenToParentLayout()
        {
            _layout.Children.Add(new WhiteTextLabel("Please enter username"));
            _layout.Children.Add(_usernameEntry);
            _layout.Children.Add(new WhiteTextLabel("Please enter password"));
            _layout.Children.Add(_passwordEntry);
            _layout.Children.Add(_saveUsernameButton);
            _layout.Children.Add(_cancelButton);

            Content = new Xamarin.Forms.ScrollView { Content = _layout };
        }

        async void HandleSaveUsernameButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await SecureStorageService.SaveLogin(_usernameEntry.Text, _passwordEntry.Text);
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Okay");
            }
        }

        class WhiteTextLabel : Label
        {
            public WhiteTextLabel(string text)
            {
                Text = text;
                HorizontalOptions = LayoutOptions.Start;
                TextColor = Color.White;
            }
        }

        class UnderlinedEntry : StyledEntry
        {
            public UnderlinedEntry(string placeHolder, string automationId) : base(1)
            {
                AutomationId = automationId;
                Placeholder = placeHolder;

                BackgroundColor = Color.Transparent;
                HeightRequest = 40;
                TextColor = Color.White;
                PlaceholderColor = Color.White;
                HorizontalOptions = LayoutOptions.Fill;
                HorizontalTextAlignment = TextAlignment.End;
                VerticalOptions = LayoutOptions.Fill;
            }
        }

        class BorderedButton : StyledButton
        {
            public BorderedButton(string text, string automationId) : base(Borders.Thin, 1)
            {
                Text = text;
                AutomationId = automationId;
            }
        }
    }
}
