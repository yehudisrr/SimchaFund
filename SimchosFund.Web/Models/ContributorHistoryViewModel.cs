using SimchosFund.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchosFund.Web.Models
{
    public class ContributorHistoryViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public Contributor Contributor { get; set; }
    }
}
