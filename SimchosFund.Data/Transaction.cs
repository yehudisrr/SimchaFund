using System;
using System.Collections.Generic;
using System.Text;

namespace SimchosFund.Data
{
    public class Transaction
    {
        public string Name { get; set; }
        public int ContributorId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string SimchaName { get; set; }
        public int SimchaId { get; set; }

    }
}
