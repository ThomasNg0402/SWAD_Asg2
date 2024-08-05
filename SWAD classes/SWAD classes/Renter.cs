using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class Renter : User
    {
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string DriverLicense { get; set; }
        public string Address { get; set; }
        public string MemberType { get; set; }
        public int MonthlyRental { get; set; }
    }
}
