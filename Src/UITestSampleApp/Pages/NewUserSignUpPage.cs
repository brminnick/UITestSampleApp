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
        #region Fields
        StyledButton _saveUsernameButton, _cancelButton;
        StyledEntry _usernameEntry, _passwordEntry;
        StackLayout _layout;
        #endregion

        #region Constructos
        public NewUserSignUpPage()
        {
            On<iOS>().SetUseSafeArea(true);

            BackgroundColor = Color.FromHex("#2980b9");
            ConstructUI();
            AddChildrenToParentLayout();
        }
        #endregion

        #region Methods
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

        void ConstructUI()
        {
            _layout = new StackLayout
            {
                Padding = new Thickness(20, 50, 20, 20),
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            _usernameEntry = new StyledEntry(1)
            {
                Style = StyleConstants.UnderlinedEntry,
                AutomationId = AutomationIdConstants.NewUserSignUpPage_NewUserNameEntry,
                Placeholder = "Username",
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.End,
                PlaceholderColor = Color.FromHex("749FA8"),
                ReturnType = ReturnType.Next,
                ReturnCommand = new Command(() => _passwordEntry.Focus())
            };

            _passwordEntry = new StyledEntry(1)
            {
                Style = StyleConstants.UnderlinedEntry,
                AutomationId = AutomationIdConstants.NewUserSignUpPage_NewPasswordEntry,
                Placeholder = "Password",
                IsPassword = true,
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalOptions = LayoutOptions.Fill,
                PlaceholderColor = Color.FromHex("749FA8"),
                ReturnType = ReturnType.Go,
                ReturnCommand = new Command(() => HandleSaveUsernameButtonClicked(_saveUsernameButton, EventArgs.Empty))
            };

            _saveUsernameButton = new StyledButton(Borders.Thin, 1)
            {
                Style = StyleConstants.BorderedButton,
                AutomationId = AutomationIdConstants.NewUserSignUpPage_SaveUsernameButton,
                Text = "Save Username",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            _saveUsernameButton.Clicked += HandleSaveUsernameButtonClicked;

            _cancelButton = new StyledButton(Borders.Thin, 1)
            {
                Style = StyleConstants.BorderedButton,
                AutomationId = AutomationIdConstants.NewUserSignUpPage_CancelButton,
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            _cancelButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PopModalAsync();
            };
        }

        void AddChildrenToParentLayout()
        {
            _layout.Children.Add(
                new Label
                {
                    Style = StyleConstants.WhiteTextLabel,
                    Text = "Please enter username",
                    HorizontalOptions = LayoutOptions.Start
                }
            );
            _layout.Children.Add(_usernameEntry);
            _layout.Children.Add(
                new Label
                {
                    Style = StyleConstants.WhiteTextLabel,
                    Text = "Please enter password",
                    HorizontalOptions = LayoutOptions.Start
                }
            );
            _layout.Children.Add(_passwordEntry);
            _layout.Children.Add(_saveUsernameButton);
            _layout.Children.Add(_cancelButton);

            Content = new Xamarin.Forms.ScrollView { Content = _layout };
        }
        #endregion

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
    }
}