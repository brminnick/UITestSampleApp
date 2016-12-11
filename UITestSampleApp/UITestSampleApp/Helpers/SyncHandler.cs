using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITestSampleApp
{
    public class SyncHandler : IMobileServiceSyncHandler
    {
        MobileServiceClient client;

        public SyncHandler(MobileServiceClient client)
        {
            this.client = client;
        }

        public async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            MobileServicePreconditionFailedException ex;
            JObject result = null;

            do
            {
                ex = null;
                try
                {
                    result = await operation.ExecuteAsync();
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
                        serverItem = (JObject)(await operation.Table.LookupAsync((string)operation.Item["id"]));
                    }

                    // Prompt user action
                    var userAction = await App.Current.MainPage.DisplayAlert("Conflict Occurred", "Select which version to keep.", "Server", "Client");

                    if (userAction)
                    {
                        return serverItem;
                    }
                    else
                    {
                        // Overwrite the server version and try the operation again by continuing the loop.
                        operation.Item[MobileServiceSystemColumns.Version] = serverItem[MobileServiceSystemColumns.Version];
                    }
                }
            } while (ex != null);

            return result;
        }

        public Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
        {
            return Task.FromResult(0);
        }
    }
}
