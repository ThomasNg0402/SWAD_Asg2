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
        public decimal Amount { get; set; }
        public DateTime TimePaid { get; set; }
        public DateTime DatePaid { get; set; }
        public string Status { get; set; }
        public bool AttemptPayment(decimal amt)
        {
            // Fake contact system here, then return true on success
            this.Amount = amt;
            this.TimePaid = DateTime.Now;
            this.DatePaid = DateTime.Now;
            this.Status = "Paid";
            return true;
        }
    }
}
