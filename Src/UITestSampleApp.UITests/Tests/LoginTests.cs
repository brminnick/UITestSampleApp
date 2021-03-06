﻿using System;
using System.Linq;
using NUnit.Framework;
using UITestSampleApp.Shared;
using Xamarin.UITest;
using Xamarin.UITest.iOS;

namespace UITestSampleApp.UITests
{
    class LoginTests : BaseTest
    {
        const string _username = "Brandon";

        public LoginTests(Platform platform) : base(platform)
        {
        }

        public override void BeforeEachTest()
        {
            base.BeforeEachTest();

            if (App is iOSApp)
                App.InvokeBackdoorMethod(BackdoorMethodConstants.ClearKeychain, _username);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CreateNewUserAndLogin(bool shouldUseKeyboardReturnButton)
        {
            //Arrange
            var username = _username;
            var password = "test";

            //Act
            LoginPage.PressSignUpButton();
            NewUserSignUpPage.CreateNewUserWithPassword(username, password, shouldUseKeyboardReturnButton);

            LoginPage.WaitForPageToLoad();
            LoginPage.LoginWithUsernamePassword(username, password, shouldUseKeyboardReturnButton);

            //Assert
            FirstPage.WaitForPageToLoad();
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
            NewUserSignUpPage.CreateNewUserWithPassword(username, password, false);
            LoginPage.LoginWithUsernamePassword(username, incorrectPassword, false);
            LoginPage.TapTryAgainDialog();

            //Assert
            Assert.IsTrue(App.Query("Login").Any());
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
            Assert.IsTrue(App.Query("Login").Any());
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
            Assert.IsTrue(App.Query("Login").Any());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CrashButtonTest(bool shouldAcceptCrashConfirmationDialog)
        {
            //Arrange

            //Act
            LoginPage.TapCrashButton();

            if (shouldAcceptCrashConfirmationDialog)
            {
                LoginPage.TapYes_CrashButtonDialog();

                //Assert
                Assert.Throws<Exception>(LoginPage.WaitForPageToLoad);
            }
            else
            {
                LoginPage.TapNo_CrashButtonDialog();

                //Assert
                LoginPage.WaitForPageToLoad();
            }
        }
    }
}