using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Learn_KeyVault_Console.Helpers {
    class KVBlobHelper {
        KeyVaultKeyResolver cloudResolver;

        public void uploadBlob() {
            // This is standard code to interact with Blob storage.
            StorageCredentials creds = new StorageCredentials(
                ConfigurationManager.AppSettings["accountName"],
                   ConfigurationManager.AppSettings["accountKey"]);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer contain = client.GetContainerReference(ConfigurationManager.AppSettings["container"]);
            contain.CreateIfNotExistsAsync();

            // The Resolver object is used to interact with Key Vault for Azure Storage.
            // This is where the GetToken method from above is used.
            cloudResolver = new KeyVaultKeyResolver(GetToken);


        }

        private async static Task<string> GetToken(string authority, string resource, string scope) {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(
                ConfigurationManager.AppSettings["app-id"],
                ConfigurationManager.AppSettings["app-secret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
    }
}
