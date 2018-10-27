using Refit;

using System.Net.Http;
using System.Threading.Tasks;

namespace UITestSampleApp.Functions
{
    public interface IAppServiceAPI
    {
        [Post(@"/v0.1/apps/{owner_name}/{app_name}/branches/{branch}/builds")]
        Task<HttpResponseMessage> QueueBuild(string owner_name, string app_name, string branch, [Body]BuildParameters buildParameters);
    }
}
