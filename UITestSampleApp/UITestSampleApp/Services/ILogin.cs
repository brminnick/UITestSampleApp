using System.Threading.Tasks;

namespace UITestSampleApp
{
	public interface ILogin
	{
		void AuthenticateWithTouchId(LoginPage page);
		Task<bool> SetPasswordForUsername(string username, string password);
		Task<bool> CheckLogin(string username, string password);
		Task SaveUsername(string username);
		Task<string> GetSavedUsername();

	}
}

