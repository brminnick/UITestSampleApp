using Xamarin.UITest;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestSampleApp.UITests
{
	public static class UITestHelpers
	{
		public static void EnterText(Query textEntryQuery, string text, IApp app)
		{
			app.ClearText(textEntryQuery);
			app.EnterText(textEntryQuery, text);
			app.DismissKeyboard();
		}
	}
}
