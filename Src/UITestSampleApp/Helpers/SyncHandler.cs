using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

using Xamarin.Forms;

namespace UITestSampleApp
{
    public class SyncHandler : IMobileServiceSyncHandler
    {
        #region Methods
        public async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            MobileServicePreconditionFailedException ex;
            JObject result = null;

            do
            {
                ex = null;
                try
                {
                    result = await operation?.ExecuteAsync();
                }
                catch (MobileServicePreconditionFailedException e)
                {
                    ex = e;
                }

                // There was a conflict in the server
                if (ex != null)
                {
                    // Grabs the server item from the exception. If not available, fetch it.
                    var serverItem = ex.Value;
                    if (serverItem == null)
                    {
                        var operationItemId = operation?.Item["id"].ToString();
                        serverItem = await operation?.Table?.LookupAsync(operationItemId) as JObject;
                    }

                    var didUserSelectServe = await GetUserResponseToKeepServerDataOrLocalData().ConfigureAwait(false);

                    if (didUserSelectServe)
                        return serverItem;

                    OverwriteServerDataUsingLocalData(operation, serverItem);
                }
            } while (ex != null);

            return result;
        }

        public Task OnPushCompleteAsync(MobileServicePushCompletionResult result) => Task.CompletedTask;

        void OverwriteServerDataUsingLocalData(IMobileServiceTableOperation operation, JObject serverItem)
        {
            if (operation == null || serverItem == null)
                return;

            operation.Item[MobileServiceSystemColumns.Version] = serverItem[MobileServiceSystemColumns.Version];
        }

        Task<bool> GetUserResponseToKeepServerDataOrLocalData() => Application.Current?.MainPage?.DisplayAlert("Conflict Occurred", "Select which version to keep.", "Server", "Client");
        #endregion
    }
}
