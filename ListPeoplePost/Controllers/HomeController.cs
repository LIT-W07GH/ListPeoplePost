using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ListPeoplePost.Data;
using Microsoft.AspNetCore.Mvc;
using ListPeoplePost.Models;
using Microsoft.AspNetCore.Http;

namespace ListPeoplePost.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
            "Data Source=.\\sqlexpress;Initial Catalog=MyFirstDb;Integrated Security=true;";


        public IActionResult Index()
        {
            var mgr = new PersonManager(_connectionString);
            var vm = new HomePageViewModel
            {
                People = mgr.GetPeople(),
            };
            if (TempData["success-message"] != null)
            {
                vm.Message = (string) TempData["success-message"];
            }
            return View(vm);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IEnumerable<Person> people)
        {
            var mgr = new PersonManager(_connectionString);
            mgr.AddPeople(people.Where(p => !String.IsNullOrEmpty(p.FirstName) && !String.IsNullOrEmpty(p.LastName)));

            TempData["success-message"] = "People added successfully!";

            return Redirect("/home/index");
        }
    }
}
