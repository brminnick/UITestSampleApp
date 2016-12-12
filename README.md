[![Build Status](https://www.bitrise.io/app/7d55e4014e3f2164.svg?token=obxBTbr5cKzohmwZbTJJTQ)](https://www.bitrise.io/app/7d55e4014e3f2164)
# Sample App Demonstrating UITests
This app shows how to implement UITest and Unit Tests into a Xamarin.Forms project.

The UITests follow the recommended practice of Page Object testing. In the views, we've added an `AutomationId` to each control to show how UITest can interact with controls most efficiently, using their AutomationId. 

It demonstrates how to utilize [Backdoors in UITest](https://developer.xamarin.com/guides/testcloud/uitest/working-with/backdoors/) to bypass a login screen, improving the speed of the tests. 

It also demonstrates how to utilize [App Links] (https://blog.xamarin.com/deep-link-content-with-xamarin-forms-url-navigation/) in UITesting to navigate quickly to the page under test. App Links is [initialized](https://github.com/brminnick/UITestSampleApp/blob/master/UITestSampleApp/UITestSampleApp/App.cs#L46) and [executed](https://github.com/brminnick/UITestSampleApp/blob/master/UITestSampleApp/UITestSampleApp/App.cs#L63) in the source code for the app in [App.cs](https://github.com/brminnick/UITestSampleApp/blob/master/UITestSampleApp/UITestSampleApp/App.cs). The UITest `SelectItemOnListView` uses a backdoor method to [execute the App Links](https://github.com/brminnick/UITestSampleApp/blob/master/UITestSampleApp/UITestSampleApp.UITests/Tests/TestsAfterLoginScreen.cs#L49).

The login page leverages the [Reusable Login Page](https://github.com/michael-watson/Forms-Expenses/tree/master/MyLoginUI) created by [Michael Watson](https://github.com/michael-watson).

Author
===
Brandon Minnick

Xamarin Customer Success Engineer
