using System;
using System.Collections.Generic;
using System.Text;

namespace SimchosFund.Data
{
    public class Contribution
    {
        public int ContributorId { get; set; }
        public int SimchaId { get; set; }
        public decimal Amount { get; set; }
    }
}
