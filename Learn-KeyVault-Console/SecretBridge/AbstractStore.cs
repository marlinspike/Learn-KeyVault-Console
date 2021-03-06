﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_KeyVault_Console.SecretBridge {
    abstract class AbstractStore {
        public abstract Task<string> GetSecret(string secretName);
    }
}
