using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using EntryCustomReturn.Forms.Plugin.Abstractions;

using UITestSampleApp.Common;

namespace UITestSampleApp
{
	public class ReusableLoginPage : ContentPage
	{
		#region LoginPage Properties

		string logoFileImageSource;

		public string LogoFileImageSource
		{
			get { return logoFileImageSource; }
			set
			{
				if (logoFileImageSource == value)
					return;
				logoFileImageSource = value;
				logo.Source = ImageSource.FromFile(logoFileImageSource);
			}
		}

		#endregion

		#region Internal Global References

		Image logo;
		StyledButton loginButton, newUserSignUpButton, forgotPasswordButton;
		StyledEntry loginEntry, passwordEntry;
		Label logoSlogan;

		double _relativeLayoutPadding = 10;

		bool isInitialized = false;

		#endregion

		public ReusableLoginPage()
		{
			BackgroundColor = Color.FromHex("#3498db");
			Padding = GetPagePadding();
			MainLayout = new RelativeLayout();

			CreateGlobalChildren();
			AddConstraintsToChildren();

			Content = new ScrollView
			{
				Content = MainLayout
			};
		}

		#region Properties
		public RelativeLayout MainLayout { get; set; }
		#endregion

		#region UI Construction Methods

		void CreateGlobalChildren()
		{
			logo = new Image();
			logoSlogan = new StyledLabel
			{
				Opacity = 0,
				Text = "Delighting Developers."
			};
			loginEntry = new StyledEntry
			{
				AutomationId = AutomationIdConstants.UsernameEntry,
				Placeholder = "Username",
			};
			CustomReturnEffect.SetReturnType(loginEntry, ReturnType.Next);
			CustomReturnEffect.SetReturnCommand(loginEntry, new Command(() => passwordEntry.Focus()));

			passwordEntry = new StyledEntry
			{
				AutomationId = AutomationIdConstants.PasswordEntry,
				Placeholder = "Password",
				IsPassword = true,
			};
			CustomReturnEffect.SetReturnType(passwordEntry, ReturnType.Go);
			CustomReturnEffect.SetReturnCommand(passwordEntry, new Command(() => HandleLoginButtonClicked(passwordEntry, EventArgs.Empty)));

			loginButton = new StyledButton(Borders.Thin)
			{
				AutomationId = AutomationIdConstants.LoginButton,
				Text = "Login",
			};
			newUserSignUpButton = new StyledButton(Borders.None)
			{
				AutomationId = AutomationIdConstants.NewUserButton,
				Text = "Sign-up",
			};
			forgotPasswordButton = new StyledButton(Borders.None)
			{
				AutomationId = AutomationIdConstants.ForgotPasswordButton,
				Text = "Forgot Password?",
			};

			loginButton.Clicked += HandleLoginButtonClicked;

			newUserSignUpButton.Clicked += (object sender, EventArgs e) => NewUserSignUp();
			forgotPasswordButton.Clicked += (object sender, EventArgs e) => ForgotPassword();
		}

		void AddConstraintsToChildren()
		{
			Func<RelativeLayout, double> getNewUserButtonWidth = (p) => newUserSignUpButton.Measure(p.Width, p.Height).Request.Width;
			Func<RelativeLayout, double> getForgotButtonWidth = (p) => forgotPasswordButton.Measure(p.Width, p.Height).Request.Width;
			Func<RelativeLayout, double> getLogoSloganWidth = (p) => logoSlogan.Measure(p.Width, p.Height).Request.Width;

			MainLayout.Children.Add(
				logo,
				xConstraint: Constraint.Constant(100),
				yConstraint: Constraint.Constant(250),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - 200)
			);

			MainLayout.Children.Add(
				logoSlogan,
				xConstraint: Constraint.RelativeToParent(p => (p.Width / 2) - (getLogoSloganWidth(p) / 2)),
				yConstraint: Constraint.Constant(125)
			);

			MainLayout.Children.Add(
				loginEntry,
				xConstraint: Constraint.Constant(40),
				yConstraint: Constraint.RelativeToView(logoSlogan, (p, v) => v.Y + v.Height + _relativeLayoutPadding),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - 80)
			);
			MainLayout.Children.Add(
				passwordEntry,
				xConstraint: Constraint.Constant(40),
				yConstraint: Constraint.RelativeToView(loginEntry, (p, v) => v.Y + v.Height + _relativeLayoutPadding),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - 80)
			);

			MainLayout.Children.Add(
				loginButton,
				xConstraint: Constraint.Constant(40),
				yConstraint: Constraint.RelativeToView(passwordEntry, (p, v) => v.Y + v.Height + _relativeLayoutPadding),
				widthConstraint: Constraint.RelativeToParent(p => p.Width - 80)
			);
			MainLayout.Children.Add(
				newUserSignUpButton,
				xConstraint: Constraint.RelativeToParent(p => (p.Width / 2) - (getNewUserButtonWidth(p) / 2)),
				yConstraint: Constraint.RelativeToView(loginButton, (p, v) => v.Y + loginButton.Height + 15)
			);
			MainLayout.Children.Add(
				forgotPasswordButton,
				xConstraint: Constraint.RelativeToParent(p => (p.Width / 2) - (getForgotButtonWidth(p) / 2)),
				yConstraint: Constraint.RelativeToView(newUserSignUpButton, (p, v) => v.Y + newUserSignUpButton.Height + _relativeLayoutPadding)
			);
		}

		#endregion

		#region Virual Methods to Expose Override Methods

		public virtual void RunAfterAnimation()
		{
		}

		public virtual void Login(string userName, string passWord)
		{
		}

		public virtual void NewUserSignUp()
		{
		}

		public virtual void ForgotPassword()
		{
		}

		#endregion

		#region Page Overrides

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (string.IsNullOrEmpty(LogoFileImageSource))
				throw new Exception("You must set the LogoFileImageSource property to specify the logo");

			logo.Source = LogoFileImageSource;

			List<Task> animationTaskList;

			if (!isInitialized)
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					await Task.Delay(500);
					await logo?.TranslateTo(0, -MainLayout.Height * 0.3 - 10, 250);
					await logo?.TranslateTo(0, -MainLayout.Height * 0.3 + 5, 100);
					await logo?.TranslateTo(0, -MainLayout.Height * 0.3, 50);

					await logo?.TranslateTo(0, -200 + 5, 100);
					await logo?.TranslateTo(0, -200, 50);

					var logoSloginAnimationTask = logoSlogan?.FadeTo(1, 5);
					var newUserSignUpButtonAnimationTask = newUserSignUpButton?.FadeTo(1, 250);
					var forgotPasswordButtonAnimationTask = forgotPasswordButton?.FadeTo(1, 250);
					var loginEntryAnimationTask = loginEntry?.FadeTo(1, 250);
					var passwordEntryAnimationTask = passwordEntry?.FadeTo(1, 250);
					var loginButtonAnimationTask = loginButton?.FadeTo(1, 249);

					animationTaskList = new List<Task>
					{
						logoSloginAnimationTask,
						newUserSignUpButtonAnimationTask,
						forgotPasswordButtonAnimationTask,
						loginEntryAnimationTask,
						passwordEntryAnimationTask,
						loginButtonAnimationTask
					};

					await Task.WhenAll(animationTaskList);

					isInitialized = true;
					RunAfterAnimation();
				});
			}
		}
		#endregion

		void HandleLoginButtonClicked(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(loginEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text))
			{
				DisplayAlert("Error", "You must enter a username and password.", "Okay");
				return;
			}

			Login(loginEntry.Text, passwordEntry.Text);
		}

		#region Extension Methods

		public void SetUsernameEntry(string password)
		{
			if (!string.IsNullOrEmpty(password))
				loginEntry.Text = password;
		}

		#endregion

		Thickness GetPagePadding()
		{
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					return new Thickness(0, 20, 0, 0);
				case Device.iOS:
					return new Thickness(0, 0, 0, 0);
				default:
					throw new Exception("Platform Unsupported");
			}
		}
	}
}