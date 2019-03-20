using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Refined Abstraction that uses the concrete impelmentations of AbstractStore to get secrets from various resources
namespace Learn_KeyVault_Web.SecretBridge {
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

        //ToDo: Persist Secret to Session or Cache of your choice: ASP.NET Session State, Redis...
        private bool persistSecretToCache(string secretName, string secretValue) {
            return true;
        }

        //ToDo: Check to see if secret is in Cache. If it is, return it from here, and then just skip checking the abstractStore
        //Returns: NULL if not found, or String value
        private string getSecretFromCache(string secretName) {
            return null;
        }

        //First Checks cache to see if secret is found there; if not, go to ConcreteStore to find it, and then save it to cache and return value
        public async Task<string> getSecert(string secretName) {

            var secret = getSecretFromCache(secretName);
            if (secret == null) {
                secret = await abstractStore.GetSecret(secretName);
                persistSecretToCache(secretName, secret);
            }

            return secret;
        }


    }
}
