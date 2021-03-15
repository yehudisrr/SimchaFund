using SimchosFund.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchosFund.Web.Models
{
    public class SimchasViewModel
    {
        public List<Simcha> Simchas { get; set; }
        public int ContributorCount { get; set; }
        public List<Contribution> Contributions { get; set; }
    }
}
