using Microsoft.AspNetCore.Mvc;
using safriltest1.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using safriltest1.BusinessLogic;
using Newtonsoft.Json;

namespace safriltest1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var testInsert = new TestingName();
            //testInsert.Name = "Rizky";
            //TestingBusinessLogic.InsertTestingName(testInsert);

            //TestingBusinessLogic.DeleteTestingName(3);

            var TestingName = TestingBusinessLogic.GetDataTestingName();
            ViewBag.TestingName = TestingName;
            ViewBag.TestingNameJSON = JsonConvert.SerializeObject(TestingName);

            return View();
        }

        [HttpPost]
        public IActionResult AddData([FromBody] TestingName testingName)
        {
            if (ModelState.IsValid)
            {
                TestingBusinessLogic.InsertTestingName(testingName);
                return Ok(new { message = "Data updated successfully" });
            }
            return BadRequest(new { message = "Invalid data" });
        }

        [HttpPost]
        public IActionResult EditData([FromBody] TestingName testingName)
        {
            if (ModelState.IsValid)
            {
                TestingBusinessLogic.UpdateTestingName(testingName);
                return Ok(new { message = "Data updated successfully" }); 
            }
            return BadRequest(new { message = "Invalid data" }); 
        }

        [HttpPost]
        public IActionResult DeleteData(int ID)
        {
            if (ModelState.IsValid)
            {
                TestingBusinessLogic.DeleteTestingName(ID);
                return Ok(new { message = "Delete successfully" });
            }
            return BadRequest(new { message = "Invalid data" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string ReverseWordManual(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string reversed = string.Empty;

            foreach (char c in input)
            {
                reversed = c + reversed; // Prepend each character to the result
            }

            return reversed;
        }
    }
}
