using System;
using System.Collections.Generic;
using System.Text;

namespace SimchosFund.Data
{
    public class Contributor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CellNumber { get; set; }
        public bool AlwaysInclude { get; set; }
        public decimal Balance { get; set; }


     }
}
