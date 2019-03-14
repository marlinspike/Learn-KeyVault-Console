using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Learn_KeyVault_Console.Helpers {
    class KeyVaultHelper {
        public KeyVaultHelper() { }
        
        //Get a Secret from KeyVault
        public async Task<string> GetSecret(string secretName) {
            string token = KeyVaultHelper.getToken();
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secret = await keyVaultClient.GetSecretAsync("https://reubenkv.vault.azure.net/secrets/" + secretName);

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


        public static string getToken() {
            string accessToken = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://169.254.169.254/metadata/identity/oauth2/token?api-version=2018-02-01&resource=https://management.azure.com/");
            request.Headers["Metadata"] = "true";
            request.Method = "GET";

            try {
                // Call /token endpoint
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Pipe response Stream to a StreamReader, and extract access token
                StreamReader streamResponse = new StreamReader(response.GetResponseStream());
                string stringResponse = streamResponse.ReadToEnd();
                Dictionary<string, string> list = (Dictionary<string, string>)JsonConvert.DeserializeObject((stringResponse));
                accessToken = list["access_token"];
            }
            catch (Exception e) {
                string errorText = String.Format("{0} \n\n{1}", e.Message, e.InnerException != null ? e.InnerException.Message : "Acquire token failed");
            }

            return accessToken;
        }
    }
}
