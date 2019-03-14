using Learn_KeyVault_Console.Helpers;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_KeyVault_Console.SecretBridge {
    class KeyVaultImplementor : AbstractStore {
        public override async Task<string> GetSecret(string secretName) {
            string token = KeyVaultHelper.getToken();
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secret = await keyVaultClient.GetSecretAsync("https://reubenkv.vault.azure.net/secrets/" + secretName);

            return secret.Value;
        }
    }
}
