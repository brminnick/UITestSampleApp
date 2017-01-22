using Xamarin.UITest;

using NUnit.Framework;
using System.Configuration;

namespace UITestSampleApp.UITests
{
	[Category("LoginTests")]
	public class LoginTests : BaseTest
	{
		const string _username = "Brandon";

		public LoginTests(Platform platform)
			: base(platform)
		{
		}

		public override void BeforeEachTest()
		{
			base.BeforeEachTest();

			BackdoorHelpers.CleariOSKeyChain(app, _username);
			BackdoorHelpers.SetiOSXTCAgent(app);
		}

		[TestCase(true)]
		[TestCase(false)]
		[Test]
		public void CreateNewUserAndLogin(bool shouldUseKeyboardReturnButton)
		{
			//Arrange
			var username = _username;
			var password = "test";
			var expectedFirstPageTitle = "First Page";

			//Act
			LoginPage.PressSignUpButton();

			if (shouldUseKeyboardReturnButton)
				NewUserSignUpPage.CreateNewUserWithPasswordUsingEnterButton(username, password);
			else
				NewUserSignUpPage.CreateNewUserWithPassword(username, password);

			if (shouldUseKeyboardReturnButton)
				LoginPage.LoginWithUsernamePasswordUsingEnterButton(username, password);
			else
				LoginPage.LoginWithUsernamePassword(username, password);

			//Assert
			var actualFirstPageTitle = FirstPage.GetTitle();
			Assert.AreEqual(expectedFirstPageTitle, actualFirstPageTitle);
		}

		[Test]
		public void CreateNewUserAndUnsuccessfullyLogin()
		{
			//Arrange
			var username = _username;
			var password = "test";
			var incorrectPassword = "incorrect";

			//Act
			LoginPage.PressSignUpButton();
			NewUserSignUpPage.CreateNewUserWithPassword(username, password);
			LoginPage.LoginWithUsernamePassword(username, incorrectPassword);
			LoginPage.TapTryAgainDialog();

			//Assert
			Assert.IsTrue(app.Query("Login").Length > 0);
		}

		[Test]
		public void TryLoginWithNoPasswordEntered()
		{
			//Arrange
			var username = _username;

			//Act
			LoginPage.EnterUsername(username);
			LoginPage.PressLoginButton();
			LoginPage.TapOkayOnErrorDialog();

			//Assert
			Assert.IsTrue(app.Query("Login").Length > 0);
		}

		[Test]
		public void TryLoginWithNoUsernameEntered()
		{
			//Arrange
			var password = "xamarin";

			//Act	
			LoginPage.EnterPassword(password);
			LoginPage.PressLoginButton();
			LoginPage.TapOkayOnErrorDialog();

			//Assert
			Assert.IsTrue(app.Query("Login").Length > 0);
		}

		[Ignore]
		[Test]
		public void CrashButtonTest()
		{
			//Arrange

			//Act
			LoginPage.TapCrashButton();

			//Assert
		}
	}
}