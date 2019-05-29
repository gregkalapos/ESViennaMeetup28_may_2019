using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Elastic.Apm;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var res = await httpClient.GetAsync("http://dbservice:5001/api/values");

            var strRes = await res.Content.ReadAsStringAsync();
            var longVal = long.Parse(strRes);

            if (!CurrencyRate.HasValue)
            {
                //this could be done in parallel with the other call and await with Task.WhenAll
                //but that's not the case and you can see it in Kibana.
                
                //using a private access key here - no guarantee it works forever,
                //get your private key here: https://fixer.io
                var currencyConv = await httpClient.GetAsync(
                    "http://data.fixer.io/api/latest?access_key=63be7e90f2f447484cf75a7e1f39cd8e&base=EUR&symbols=USD");

                var strCurrencyRes = await currencyConv.Content.ReadAsStringAsync();

                dynamic jsObj = JsonConvert.DeserializeObject(strCurrencyRes);

                var rate = (double) jsObj.rates.USD;
                CurrencyRate = rate;

                ViewData["retVal"] = $"{longVal} EUR is {rate * longVal} USD";
                CurrencyRate = rate;
            }
            else
            {
                //a primitive caching to create custom spans with the agent API
                Agent.Tracer.CurrentTransaction.CaptureSpan("ReadCurrencyRate", "Cache", (s) =>
                {
                    s.Tags["CachedCurrencyRate"] = CurrencyRate.ToString();
                    ViewData["retVal"] = $"{longVal} EUR is {CurrencyRate * longVal} USD"; 
                });
               
            }

            return View();
        }

        private static double? CurrencyRate;

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}