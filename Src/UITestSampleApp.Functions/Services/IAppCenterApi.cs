using System.Net.Http;
using System.Threading.Tasks;

using Refit;

namespace UITestSampleApp.Functions
{
    public interface IAppCenterAPI
    {
        [Post(@"/v0.1/apps/{owner_name}/{app_name}/branches/{branch}/builds")]
        Task<HttpResponseMessage> QueueBuild([AliasAs("owner_name")] string ownerName, [AliasAs("app_name")] string appName, string branch, [Body] BuildParameters buildParameters);
    }
}
