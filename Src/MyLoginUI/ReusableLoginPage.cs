using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using MyLoginUI.Views;

using UITestSampleApp.Shared;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace MyLoginUI.Pages
{
    public abstract class ReusableLoginPage : ContentPage
    {
        #region Constant Fields
        const double _relativeLayoutPadding = 10;
        readonly Image _logo = new Image();
        #endregion

        #region Fields
        string _logoFileImageSource;
        StyledButton _loginButton, _newUserSignUpButton, _forgotPasswordButton;
        StyledEntry _loginEntry, _passwordEntry;
        Label _logoSlogan;
        bool _isInitialized;
        #endregion

        #region Properties
        protected RelativeLayout MainLayout { get; }

        string LogoFileImageSource
        {
            get => _logoFileImageSource;
            set
            {
                if (_logoFileImageSource != value)
                {
                    _logoFileImageSource = value;
                    _logo.Source = ImageSource.FromFile(_logoFileImageSource);
                }
            }
        }
        #endregion

        #region Constructors
        protected ReusableLoginPage(string logoFileImageSource)
        {
            On<iOS>().SetUseSafeArea(true);

            BackgroundColor = Color.FromHex("#3498db");
            Padding = GetPagePadding();
            MainLayout = new RelativeLayout();

            LogoFileImageSource = logoFileImageSource;

            CreateGlobalChildren();
            AddConstraintsToChildren();

            Content = new Xamarin.Forms.ScrollView { Content = MainLayout };
        }
        #endregion

        #region Methods
        protected virtual Task RunAfterAnimation() => Task.CompletedTask;

        protected virtual Task ForgotPassword() => Task.CompletedTask;

        protected abstract Task Login(string username, string password);

        protected abstract Task NewUserSignUp();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (string.IsNullOrEmpty(LogoFileImageSource))
                throw new ArgumentNullException(nameof(LogoFileImageSource), "You must set the LogoFileImageSource property to specify the logo");

            _logo.Source = LogoFileImageSource;

            if (!_isInitialized)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(500);
                    await _logo?.TranslateTo(0, -MainLayout.Height * 0.3 - 10, 250);
                    await _logo?.TranslateTo(0, -MainLayout.Height * 0.3 + 5, 100);
                    await _logo?.TranslateTo(0, -MainLayout.Height * 0.3, 50);

                    await _logo?.TranslateTo(0, -200 + 5, 100);
                    await _logo?.TranslateTo(0, -200, 50);

                    await Task.WhenAll(_logoSlogan?.FadeTo(1, 5),
                                        _newUserSignUpButton?.FadeTo(1, 250),
                                        _forgotPasswordButton?.FadeTo(1, 250),
                                        _loginEntry?.FadeTo(1, 250),
                                        _passwordEntry?.FadeTo(1, 250),
                                        _loginButton?.FadeTo(1, 249));

                    _isInitialized = true;

                    await RunAfterAnimation();
                });
            }
        }

        async void HandleNewUserSignUpButtonClicked(object sender, EventArgs e) => await NewUserSignUp();

        void CreateGlobalChildren()
        {
            _logoSlogan = new StyledLabel
            {
                Opacity = 0,
                Text = "Delighting Developers."
            };
            _loginEntry = new StyledEntry
            {
                AutomationId = AutomationIdConstants.LoginPage_UsernameEntry,
                Placeholder = "Username",
                ReturnType = ReturnType.Next,
                ReturnCommand = new Command(() => _passwordEntry.Focus())
            };

            _passwordEntry = new StyledEntry
            {
                AutomationId = AutomationIdConstants.LoginPage_PasswordEntry,
                Placeholder = "Password",
                IsPassword = true,
                ReturnType = ReturnType.Go,
                ReturnCommand = new Command(() => HandleLoginButtonClicked(_passwordEntry, EventArgs.Empty))
            };

            _loginButton = new StyledButton(Borders.Thin)
            {
                AutomationId = AutomationIdConstants.LoginPage_LoginButton,
                Text = "Login",
            };
            _loginButton.Clicked += HandleLoginButtonClicked;

            _newUserSignUpButton = new StyledButton(Borders.None)
            {
                AutomationId = AutomationIdConstants.LoginPage_NewUserSignUpButton,
                Text = "Sign-up",
            };
            _newUserSignUpButton.Clicked += HandleNewUserSignUpButtonClicked;

            _forgotPasswordButton = new StyledButton(Borders.None)
            {
                AutomationId = AutomationIdConstants.LoginPage_ForgotPasswordButton,
                Text = "Forgot Password?",
            };
            _forgotPasswordButton.Clicked += async (object sender, EventArgs e) => await ForgotPassword();
        }

        void AddConstraintsToChildren()
        {
            Func<RelativeLayout, double> getNewUserButtonWidth = (p) => _newUserSignUpButton.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getForgotButtonWidth = (p) => _forgotPasswordButton.Measure(p.Width, p.Height).Request.Width;
            Func<RelativeLayout, double> getLogoSloganWidth = (p) => _logoSlogan.Measure(p.Width, p.Height).Request.Width;

            MainLayout.Children.Add(
                _logo,
                xConstraint: Constraint.Constant(100),
                yConstraint: Constraint.Constant(250),
                widthConstraint: Constraint.RelativeToParent(p => p.Width - 200)
            );

            MainLayout.Children.Add(
                _logoSlogan,
                xConstraint: Constraint.RelativeToParent(p => (p.Width / 2) - (getLogoSloganWidth(p) / 2)),
                yConstraint: Constraint.Constant(125)
            );

            MainLayout.Children.Add(
                _loginEntry,
                xConstraint: Constraint.Constant(40),
                yConstraint: Constraint.RelativeToView(_logoSlogan, (p, v) => v.Y + v.Height + _relativeLayoutPadding),
                widthConstraint: Constraint.RelativeToParent(p => p.Width - 80)
            );
            MainLayout.Children.Add(
                _passwordEntry,
                xConstraint: Constraint.Constant(40),
                yConstraint: Constraint.RelativeToView(_loginEntry, (p, v) => v.Y + v.Height + _relativeLayoutPadding),
                widthConstraint: Constraint.RelativeToParent(p => p.Width - 80)
            );

            MainLayout.Children.Add(
                _loginButton,
                xConstraint: Constraint.Constant(40),
                yConstraint: Constraint.RelativeToView(_passwordEntry, (p, v) => v.Y + v.Height + _relativeLayoutPadding),
                widthConstraint: Constraint.RelativeToParent(p => p.Width - 80)
            );
            MainLayout.Children.Add(
                _newUserSignUpButton,
                xConstraint: Constraint.RelativeToParent(p => (p.Width / 2) - (getNewUserButtonWidth(p) / 2)),
                yConstraint: Constraint.RelativeToView(_loginButton, (p, v) => v.Y + _loginButton.Height + 15)
            );
            MainLayout.Children.Add(
                _forgotPasswordButton,
                xConstraint: Constraint.RelativeToParent(p => (p.Width / 2) - (getForgotButtonWidth(p) / 2)),
                yConstraint: Constraint.RelativeToView(_newUserSignUpButton, (p, v) => v.Y + _newUserSignUpButton.Height + _relativeLayoutPadding)
            );
        }

        async void HandleLoginButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_loginEntry.Text) || string.IsNullOrWhiteSpace(_passwordEntry.Text))
                await DisplayAlert("Error", "You must enter a username and password.", "Okay");
            else
                await Login(_loginEntry.Text, _passwordEntry.Text);
        }

        Thickness GetPagePadding()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return new Thickness(0, 20, 0, 0);
                case Device.iOS:
                    return new Thickness(0, 0, 0, 0);
                default:
                    throw new NotSupportedException("Runtime Platform Unsupported");
            }
        }
        #endregion
    }
}