using SimchosFund.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimchosFund.Web.Models
{
    public class ContributionsViewModel
    {
        public Simcha Simcha { get; set; }
        public List<Contributor> Contributors { get; set; }
        
        public List<Contribution> SimchaContributions { get; set; }
    }
}
