using Xamarin.Forms;

namespace MyLoginUI
{
	public class StyledLabel : Label
	{
		public StyledLabel()
		{
			TextColor = Color.White;
			FontFamily = StyleHelpers.GetFontFamily();
			FontSize = 14;
		}
	}
}

