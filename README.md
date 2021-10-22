<a href="https://www.buymeacoffee.com/bminnick" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" style="height: 51px !important;width: 217px !important;" ></a>

|CI Tool                    |Build Status|
|---------------------------|---|
| App Center, iOS           |  [![Build status](https://build.appcenter.ms/v0.1/apps/0184bf48-f102-400c-aed0-629fdeb38696/branches/main/badge)](https://appcenter.ms)|
| App Center, Android       | [![Build status](https://build.appcenter.ms/v0.1/apps/864df958-bcca-401d-8f93-ae159cd5a9d3/branches/main/badge)](https://appcenter.ms) |
| GitHub Actions | [![Xamarin](https://github.com/brminnick/UITestSampleApp/actions/workflows/mobile.yml/badge.svg)](https://github.com/brminnick/UITestSampleApp/actions/workflows/mobile.yml) [![Azure Functions](https://github.com/brminnick/UITestSampleApp/actions/workflows/functions.yml/badge.svg)](https://github.com/brminnick/UITestSampleApp/actions/workflows/functions.yml) |

# Sample App Demonstrating UITests
This app shows how to implement UITests into a Xamarin.Forms project.

The UITests follow the recommended practice of Page Object Architecture. In the Xamarin.Forms views, we've added an `AutomationId` to each control to show how UITest interact with controls cross-platform, using their AutomationId. 

It demonstrates how to utilize [Backdoors in UITest](https://docs.microsoft.com/appcenter/test-cloud/frameworks/uitest/features/backdoors?WT.mc_id=mobile-0000-bramin) to bypass a login screen, improving the speed of the tests. 

It also demonstrates how to utilize [App Links](https://devblogs.microsoft.com/xamarin/deep-link-content-with-xamarin-forms-url-navigation?WT.mc_id=mobile-0000-bramin) in UITesting to navigate quickly to the page under test. App Links is [initialized](https://github.com/brminnick/UITestSampleApp/blob/master/Src/UITestSampleApp/App.cs#L53) and [executed](https://github.com/brminnick/UITestSampleApp/blob/master/Src/UITestSampleApp/App.cs#L45) in the source code for the app in [App.cs](https://github.com/brminnick/UITestSampleApp/blob/master/Src/UITestSampleApp/App.cs). The UITest `SelectItemOnListView` uses a backdoor method to [execute the App Links](https://github.com/brminnick/UITestSampleApp/blob/master/Src/UITestSampleApp.UITests/Tests/TestsAfterLoginScreen.cs#L58) and navigate directly to the ListPage.

The login page leverages the [Reusable Login Page](https://github.com/michael-watson/Forms-Expenses/tree/master/MyLoginUI) created by [Michael Watson](https://github.com/michael-watson).
