using System.Threading.Tasks;

namespace UITestSampleApp
{
	public interface ILogin
	{
		Task<bool> SetPasswordForUsername(string username, string password);
		Task<bool> CheckLogin(string username, string password);
	}
}

