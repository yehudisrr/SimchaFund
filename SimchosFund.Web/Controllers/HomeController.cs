using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimchosFund.Data;
using SimchosFund.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SimchosFund.Web.Controllers
{ 
public class HomeController : Controller
{
        private string _connectionString =
       @"Data Source=.\sqlexpress;Initial Catalog=SimchaFund;Integrated Security=true;";
        public IActionResult Index()
        {
            SFundDb db = new SFundDb(_connectionString);
            SimchasViewModel vm = new SimchasViewModel();
            vm.Simchas = db.GetSimchas();
            return View(vm);
        }
        public IActionResult New()
        {
            return View();
        }

    }
}