using ConsumptionWebApp.Helper;
using ConsumptionWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumptionWebApp.Controllers
{
    public class HomeController : Controller
    {
        UserAPI _api = new UserAPI();

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            HttpClient client = _api.Initial();

            var postTask = client.PostAsJsonAsync("api/users", user);
            
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

        public async Task<IActionResult> Edit(long id)
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

        [HttpPost]
        public IActionResult Edit(User user)
        {
            HttpClient client = _api.Initial();

            var postTask = client.PutAsJsonAsync($"api/users/{user.Id}", user);

            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
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

        public async Task<IActionResult> Delete(long id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/users/{id}");

            if (res.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
    }
}
