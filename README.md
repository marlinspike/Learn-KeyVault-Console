# Learn-KeyVault-Console
Demo of using the Key Vault from a C# console app, as well as a Bridge Pattern for Key Vault and App Config secret reader. It's all you'll need to invoke the Key Vault.

Note that this implementation requires you to use Managed Identity, whic is the preferred way of accessing Key Vault. It's very simple to implement, and here's how you'd do it: https://docs.microsoft.com/en-us/azure/key-vault/tutorial-net-windows-virtual-machine

The Bridge Pattern (https://springframework.guru/gang-of-four-design-patterns/bridge-pattern/), is an elegant way to separate an abstraction from an implementation, so that either can change without impacting the other. For projects migrating to Azure, or which need to use a combination of Key Vault and App Config files, it's one of the patterns you can use to export secrets to Key Vault, while keeping more mundate app-config stuff in the config file. In the implemented Bridge Pattern, the caller is abstracted from how the secret is actually retrieved, aside from just knowing that a particular implementaiton must be used.

