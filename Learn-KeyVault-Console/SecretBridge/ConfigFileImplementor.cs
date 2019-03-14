using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Learn_KeyVault_Console.SecretBridge {
    class ConfigFileImplementor : AbstractStore {
        public override async Task<string> GetSecret(string secretName) {
            string secretValue = ConfigurationManager.AppSettings[secretName].ToString();
            return secretValue;
        }


    }
}
