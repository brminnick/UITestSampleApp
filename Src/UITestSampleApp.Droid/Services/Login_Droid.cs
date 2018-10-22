using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Akavache;

using Xamarin.Forms;

using UITestSampleApp.Droid;

[assembly: Dependency(typeof(Login_Droid))]
namespace UITestSampleApp.Droid
{
    public class Login_Droid : ILogin
    {
        const string _userNameKey = "username";
        const string _passwordKey = "password";

        public async Task<bool> SetPasswordForUsername(string username, string password)
        {
            try
            {
                await BlobCache.Secure.InsertObject(_userNameKey, username);
                await BlobCache.Secure.InsertObject(_passwordKey, password);

                return true;
            }
            catch (Exception e)
            {
                AppCenterHelpers.LogException(e);
                return false;
            }
        }

        public async Task<bool> CheckLogin(string username, string password)
        {
            string usernameFromDevice, passwordFromDevice;

            try
            {
                usernameFromDevice = await BlobCache.Secure.GetObject<string>(_userNameKey);
            }
            catch (Exception e)
            {
                AppCenterHelpers.LogException(e);
                return false;
            }

            try
            {
                passwordFromDevice = await BlobCache.Secure.GetObject<string>(_passwordKey);
            }
            catch (Exception e)
            {
                AppCenterHelpers.LogException(e);
                return false;
            }

            return !string.IsNullOrWhiteSpace(usernameFromDevice)
                          && !string.IsNullOrWhiteSpace(passwordFromDevice)
                          && password == passwordFromDevice
                          && username == usernameFromDevice;
        }
    }
}