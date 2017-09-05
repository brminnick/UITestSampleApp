using Xamarin.Forms;

using EntryCustomReturn.Forms.Plugin.Abstractions;

namespace MyLoginUI.Views
{
	public class StyledEntry : Entry
	{
		public StyledEntry(double opacity = 0)
		{
			BackgroundColor = Color.Transparent;
			HeightRequest = 40;
			TextColor = Color.White;
			Opacity = opacity;
			PlaceholderColor = Color.White;
		}
	}
}
