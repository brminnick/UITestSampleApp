using System.Collections.Generic;
using NUnit.Framework;
using UITestSampleApp.Shared;
using Xamarin.UITest;

namespace UITestSampleApp.UITests
{
    class TestsAfterLoginScreen : BaseTest
    {
        public TestsAfterLoginScreen(Platform platform) : base(platform)
        {
        }

        public override void BeforeEachTest()
        {
            base.BeforeEachTest();

            LoginPage.WaitForPageToLoad();

            App.InvokeBackdoorMethod(BackdoorMethodConstants.BypassLoginScreen);

            FirstPage.WaitForPageToLoad();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void EnterText(bool shouldUseKeyboardReturnButton)
        {
            //Arrange
            var textInput = "Hello World";

            //Act
            switch (shouldUseKeyboardReturnButton)
            {
                case true:
                    FirstPage.EnterTextAndPressEnter(textInput);
                    break;
                case false:
                    FirstPage.EnterText(textInput);
                    FirstPage.ClickGo();
                    break;
            }

            FirstPage.WaitForNoActivityIndicator();

            //Assert
            Assert.AreEqual(FirstPage.GetEntryFieldText(), textInput);
        }
    }
}

