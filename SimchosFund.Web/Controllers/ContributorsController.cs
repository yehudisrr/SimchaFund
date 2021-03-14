using Microsoft.AspNetCore.Mvc;
using SimchosFund.Data;
using SimchosFund.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchosFund.Web.Controllers
{
    public class ContributorsController : Controller
    {
        private string _connectionString =
      @"Data Source=.\sqlexpress;Initial Catalog=SimchaFund;Integrated Security=true;";
        public IActionResult Index()
        {
            SFundDb db = new SFundDb(_connectionString);
            ContributionsViewModel vm = new ContributionsViewModel();
            vm.Contributors = db.GetContributors();
            return View(vm);
        }
      
        [HttpPost]
        public IActionResult New(Contributor contributor, int amount, DateTime createdDate)
        {
            SFundDb db = new SFundDb(_connectionString);
            db.AddContributor(contributor);
            Deposit deposit = new Deposit
            {
                ContributorId = contributor.Id,
                Date = createdDate,
                Amount = amount
            };
            db.AddDeposit(deposit);
            return Redirect("/Contributors/Index");
        }
        [HttpPost]
        public IActionResult Edit(Contributor contributor)
        {
            SFundDb db = new SFundDb(_connectionString);
            db.EditContributor(contributor);
            return Redirect("/Contributors/Index");
        }
        [HttpPost]
        public IActionResult Deposit(Deposit deposit)
        {
            SFundDb db = new SFundDb(_connectionString);
            db.AddDeposit(deposit);
            return Redirect("/Contributors/Index");
        }

        public IActionResult History(int id)
        {
            SFundDb db = new SFundDb(_connectionString);
            ContributorHistoryViewModel vm = new ContributorHistoryViewModel();
            List<Transaction> transactions = db.GetDeposits(id);
            if (db.GetContributions(id) != null)
            {
                transactions.AddRange(db.GetContributions(id));
            }
            vm.Transactions = transactions;
            vm.Contributor = db.GetContributor(id);
            return View(vm);
        }
    }
}
