using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Refined Abstraction that uses the concrete impelmentations of AbstractStore to get secrets from various resources
namespace Learn_KeyVault_Console.SecretBridge {
    class SecretSource {
        AbstractStore abstractStore = null;

        public SecretSource() {

        }

        public SecretSource(AbstractStore secretStore) {
            this.SecretStoreImplementation = secretStore;
        }

        public AbstractStore SecretStoreImplementation {
            get { return abstractStore; }
            set { this.abstractStore = value; }
        }

        public async Task<string> getSecert(string secretName) {
            var secret = await abstractStore.GetSecret(secretName);
            return secret;
        }


    }
}
