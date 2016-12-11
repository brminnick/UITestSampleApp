using Xamarin.Forms;

namespace UITestSampleApp
{
	public class WhiteTextImageCell : ImageCell
	{
		public WhiteTextImageCell() : base()
		{
			TextColor = Color.White;
			DetailColor = Color.White;

			this.SetValue(ImageCell.TextProperty, "Number");
			this.SetBinding(ImageCell.DetailProperty, "Number");
			this.SetValue(ImageCell.ImageSourceProperty, "Hash");
		}
	}
}

