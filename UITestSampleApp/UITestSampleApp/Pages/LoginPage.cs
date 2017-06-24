using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using UITestSampleApp.Common;

namespace UITestSampleApp
{
	public class LoginPage : ReusableLoginPage
	{
		#region Fields
		bool isInitialized = false;
		#endregion

		#region Constructors
		public LoginPage()
		{
			AutomationId = "loginPage";

#if DEBUG
			var crashButton = new Button
			{
				Text = "x",
				TextColor = Color.White,
				BackgroundColor = Color.Transparent,
				AutomationId = AutomationIdConstants.CrashButton
			};
			crashButton.Clicked += (s, e) =>
			{
				throw new Exception("Crash Button Tapped");
			};

			MainLayout.Children.Add(crashButton,
				Constraint.RelativeToParent((parent) => parent.X),
				Constraint.RelativeToParent((parent) => parent.Y)
			);
#endif
		}
		#endregion

		#region Methods
		public override async void Login(string userName, string passWord)
		{
			base.Login(userName, passWord);

			var success = await DependencyService.Get<ILogin>().CheckLogin(userName, passWord);
			if (success)
			{
				App.IsLoggedIn = true;

				await RemoveLoginPage();
			}
			else
			{
				var signUp = await DisplayAlert("Invalid Login", "Sorry, we didn't recoginize the username or password. Feel free to sign up for free if you haven't!", "Sign up", "Try again");

				if (signUp)
				{
					await Navigation.PushModalAsync(new NewUserSignUpPage());

					MobileCenterHelpers.TrackEvent("NewUserSignUp", new Dictionary<string, string> {
						{ "ActionPoint", "System Prompt" },
					});
				}
			}
		}

		public override void NewUserSignUp()
		{
			base.NewUserSignUp();
			Navigation.PushModalAsync(new NewUserSignUpPage());
		}

		public override void RunAfterAnimation()
		{
			base.RunAfterAnimation();

			if (App.UserName != null)
				SetUsernameEntry(App.UserName);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			//Need bug fixed on Material Design for PopToRootAsync() 
			//https://bugzilla.xamarin.com/show_bug.cgi?id=36907
			if (!isInitialized)
			{
				switch (Device.RuntimePlatform)
				{
					case Device.iOS:
						Navigation.InsertPageBefore(new FirstPage(), this);
						break;
				}

				isInitialized = true;
			}
		}

		async Task RemoveLoginPage()
		{
			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					await Navigation.PopAsync();
					break;
				default:
					await Navigation.PushAsync(new FirstPage(), false);
					Navigation.RemovePage(this);
					break;
			}
		}
		#endregion
	}
}