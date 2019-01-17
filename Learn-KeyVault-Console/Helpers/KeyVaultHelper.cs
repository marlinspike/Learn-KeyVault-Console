using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_KeyVault_Console.Helpers {
    class KeyVaultHelper {
        public KeyVaultHelper() { }
        
        //Get a Secret from KeyVault
        public async Task<string> GetSecret(string secretName) {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secret = keyVaultClient.GetSecretAsync("https://reubenkv.vault.azure.net/secrets/" + secretName).GetAwaiter().GetResult();

            return secret.Value;
        }

        //Write a Secert to Key Vault
        public async void WriteSecret(string secretName, string secretValue) {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            await keyVaultClient.SetSecretAsync("https://reubenkv.vault.azure.net/", secretName, secretValue,null,"text");

        }


        //Get Stored Key
        public async Task<string> GetKey(string keyName) {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var key = keyVaultClient.GetKeyAsync("https://reubenkv.vault.azure.net/", keyName);
            return key.Result.Key.ToString();
        }
    }
}
