using System.Threading.Tasks;

using Foundation;

using Xamarin.Forms;

using UITestSampleApp.iOS;

[assembly: Dependency(typeof(Login_iOS))]
namespace UITestSampleApp.iOS
{
	public class Login_iOS : ILogin
	{

		public async Task<bool> SetPasswordForUsername(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
				return false;

			KeychainHelpers.SetPasswordForUsername(username, password, "XamarinExpenses", Security.SecAccessible.Always, true);
			NSUserDefaults.StandardUserDefaults.SetString(username, "username");
			NSUserDefaults.StandardUserDefaults.SetBool(true, "hasLogin");
			NSUserDefaults.StandardUserDefaults.Synchronize();

			return true;
		}

		public async Task<bool> CheckLogin(string username, string password)
		{
			var _username = NSUserDefaults.StandardUserDefaults.ValueForKey(new NSString("username"));
			var _password = KeychainHelpers.GetPasswordForUsername(username, "XamarinExpenses", true);

			if (_username == null || _password == null)
				return false;

			if (password == _password &&
				username == _username.ToString())
			{
				return true;
			}

			return false;
		}
	}
}

