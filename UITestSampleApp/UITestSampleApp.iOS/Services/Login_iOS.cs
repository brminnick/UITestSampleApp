using System.Threading.Tasks;

using Foundation;

using Xamarin.Forms;

using UITestSampleApp.iOS;

[assembly: Dependency(typeof(Login_iOS))]
namespace UITestSampleApp.iOS
{
	public class Login_iOS : ILogin
	{

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<bool> SetPasswordForUsername(string username, string password)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
				return false;

			KeychainHelpers.SetPasswordForUsername(username, password, "XamarinExpenses", Security.SecAccessible.Always, true);
			NSUserDefaults.StandardUserDefaults.SetString(username, "username");
			NSUserDefaults.StandardUserDefaults.SetBool(true, "hasLogin");
			NSUserDefaults.StandardUserDefaults.Synchronize();

			return true;
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<bool> CheckLogin(string username, string password)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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

