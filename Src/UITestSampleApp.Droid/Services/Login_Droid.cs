using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Akavache;

using Xamarin.Forms;

using UITestSampleApp.Droid;

[assembly: Dependency(typeof(Login_Droid))]
namespace UITestSampleApp.Droid
{
	public class Login_Droid : ILogin
	{
		public async Task<bool> SetPasswordForUsername(string username, string password)
		{
			await BlobCache.UserAccount.InsertObject("username", username);
			await BlobCache.UserAccount.InsertObject("password", password);

			return true;
		}

		public async Task<bool> CheckLogin(string username, string password)
		{
			string _username = null;
			string _password = null;

			try
			{
				_username = await BlobCache.UserAccount.GetObject<string>("username");
			}
			catch (Exception e)
			{
                AppCenterHelpers.Log(e);
				return false;
			}

			try
			{
				_password = await BlobCache.UserAccount.GetObject<string>("password");
			}
			catch (Exception e)
			{
                AppCenterHelpers.Log(e);
				return false;
			}

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