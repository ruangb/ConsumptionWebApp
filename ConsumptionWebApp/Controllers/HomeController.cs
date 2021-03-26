using ConsumptionWebApp.Helper;
using ConsumptionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumptionWebApp.Controllers
{
    public class HomeController : Controller
    {
        UserAPI _api = new UserAPI();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/users");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;

                users = JsonConvert.DeserializeObject<List<User>>(results);
            }

            return View(users);
        }

        public async Task<IActionResult> Details(long id)
        {
            User user = new User();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/users/{id}");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;

                user = JsonConvert.DeserializeObject<User>(results);
            }

            return View(user);
        }
    }
}
