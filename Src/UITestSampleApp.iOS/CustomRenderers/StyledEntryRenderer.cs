using UIKit;
using Foundation;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using MyLoginUI.Views;

using UITestSampleApp.iOS;

[assembly: ExportRenderer(typeof(StyledEntry), typeof(StyledEntryRenderer))]

namespace UITestSampleApp.iOS
{
    public class StyledEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName is nameof(Element.IsEnabled))
            {
                if (!Control.Enabled)
                    Control.TextColor = UIColor.White;
                else
                    Control.TextColor = UIColor.Blue;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var formsEntry = (StyledEntry)e.NewElement;
                Control.Font = UIFont.FromName("AppleSDGothicNeo-Light", 18);
                Control.TextColor = UIColor.White;

                if (!string.IsNullOrEmpty(formsEntry.Placeholder))
                    Control.AttributedPlaceholder = new NSAttributedString(formsEntry.Placeholder, UIFont.FromName("AppleSDGothicNeo-Light", 18), UIColor.White);
            }
        }
    }
}