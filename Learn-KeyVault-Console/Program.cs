using Learn_KeyVault_Console.Helpers;
using Learn_KeyVault_Console.SecretBridge;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Configuration;

namespace Learn_KeyVault_Console {
    class Program {

        static void Main(string[] args) {
            string vaultName, secretName, secretValue, keyName = "";
            KeyVaultHelper kvHelper = new KeyVaultHelper();
            Program.printDefaultInstructions();
            string input = "";
            Program p = new Program();
            input = Console.ReadLine().ToUpper();

            while (true) {
                switch (input) {
                    case "X":
                        Environment.Exit(0);
                        break;
                    //Read from KV
                    case "R":
                        //Console.Write("Enter name of KeyVault: "); vaultName = Console.ReadLine();
                        Console.Write("Enter Secret name: "); secretName = Console.ReadLine();
                        try {
                            string secret = kvHelper.GetSecret(secretName).GetAwaiter().GetResult();
                            Console.WriteLine("Got Secret: " + secret);
                        }catch (Exception e) {
                            Console.WriteLine(e);
                        }
                        Console.Read();
                        break;
                    //Write to KV
                    case "W":
                        Console.Write("Enter name of Secret: "); secretName = Console.ReadLine();
                        Console.Write("Enter Secret Value: "); secretValue = Console.ReadLine();

                        kvHelper.WriteSecret(secretName, secretValue);
                        Console.WriteLine("Write complete!");
                        Console.Read();
                        break;
                    //Get Key
                    case "K":
                        Console.Write("Enter name of Key: "); keyName = Console.ReadLine();
                        string key = kvHelper.GetKey(keyName).GetAwaiter().GetResult();
                        Console.WriteLine("Got Key: " + key);
                        Console.Read();
                        break;
                    case "EB":
                        //Encrypt and upload blob
                        string app_id = ConfigurationManager.AppSettings.Get("app-id");
                        Console.WriteLine($"App-id: {app_id}");
                        Console.Read();
                        break;

                    case "AK":
                        SecretSource kvSource = new SecretSource(new KeyVaultImplementor());
                        Console.WriteLine(kvSource.getSecert("hw").GetAwaiter().GetResult());
                        Console.ReadLine();
                        break;
                    case "AC":
                        SecretSource fileSource = new SecretSource(new ConfigFileImplementor());
                        Console.WriteLine(fileSource.getSecert("testSecret").GetAwaiter().GetResult());
                        Console.ReadLine();
                        break;
                }
                Program.printDefaultInstructions();
                input = Console.ReadLine().ToUpper();
            }
        }

        static void printDefaultInstructions() {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("---------");
            Console.WriteLine("R   = Read Secret from KeyVault");
            Console.WriteLine("W   = Write Secret to KeyVault");
            Console.WriteLine("K   = Read Key from KeyVault");
            Console.WriteLine("AK  = Read Key from Abstracted KeyVault");
            Console.WriteLine("AC  = Read Key Abstracted Config file");
            Console.WriteLine("EB = Upload an encyrpted blob to Azure Storage");

            Console.WriteLine("X   = Exit");
            Console.WriteLine("---------");
            Console.WriteLine("Please enter selection");
        }

    }
}
