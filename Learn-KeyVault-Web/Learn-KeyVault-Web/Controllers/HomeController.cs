using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Learn_KeyVault_Web.Models;
using Learn_KeyVault_Web.SecretBridge;
using Microsoft.Extensions.Options;

namespace Learn_KeyVault_Web.Controllers {
    public class HomeController : Controller {
        private AppSettings AppSettings { get; set; }

        public HomeController(IOptions<AppSettings> settings) {
            AppSettings = settings.Value;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult Secrets() {
            SecretSource kvSource = new SecretSource(new KeyVaultImplementor());
            string keyVaultSecret = kvSource.getSecert("hw").GetAwaiter().GetResult();
            SecretSource fileSource = new SecretSource(new ConfigFileImplementor());
            string configFileSecert = AppSettings.appSecret; //fileSource.getSecert("appSecret").GetAwaiter().GetResult();

            ViewBag.ConfigFileSecret = configFileSecert;
            ViewBag.KeyVaultSecret = keyVaultSecret;


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
