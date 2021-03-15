﻿using Microsoft.AspNetCore.Mvc;
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
            vm.ContributorCount = db.GetContributors().Count;
            return View(vm);
        }
    
        [HttpPost]
        public IActionResult New(Simcha simcha)
        {
            SFundDb db = new SFundDb(_connectionString);
            db.AddSimcha(simcha);
            return Redirect("/Home/Index");
        }

        public IActionResult Contributions(int Id)
        {
            SFundDb db = new SFundDb(_connectionString);
            ContributionsViewModel vm = new ContributionsViewModel();
            vm.Simcha = db.GetSimcha(Id);
            vm.Contributors = db.GetContributors();
            return View(vm);
        }

        [HttpPost]
        public IActionResult UpdateContributions(List<Contribution> contributions)
        {
            SFundDb db = new SFundDb(_connectionString);
            db.ClearContributions(contributions[0].SimchaId);
            foreach (var contribution in contributions)
            {
                {
                   db.AddContribution(contribution);
                   contribution.AddContribution = true;
                }
            }
            return Redirect("/Home/Index");
        }
    }
}