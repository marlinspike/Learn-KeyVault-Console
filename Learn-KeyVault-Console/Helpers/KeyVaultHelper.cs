using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_KeyVault_Console.Helpers {
    class KeyVaultHelper {
        public KeyVaultHelper() { }

        public async Task<string> GetSecret(string secretName) {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secret = keyVaultClient.GetSecretAsync("https://reubenkv.vault.azure.net/secrets/" + secretName).GetAwaiter().GetResult();

            return secret.Value;
        }

        public async void WriteSecret(string secretName, string secretValue) {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            await keyVaultClient.SetSecretAsync("https://reubenkv.vault.azure.net/", secretName, secretValue,null,"text");

        }
    }
}
