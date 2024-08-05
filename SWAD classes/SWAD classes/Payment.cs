using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class Payment
    {
        public int PaymentId { get; set; }
        public int Amount { get; set; }
        public DateTime TimePaid { get; set; }
        public DateTime DatePaid { get; set; }
        public string Status { get; set; }
    }
}
