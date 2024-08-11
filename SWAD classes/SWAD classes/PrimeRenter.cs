using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class PrimeRenter : Renter
    {
        public PrimeRenter(int userid, string name, string email, string contact,DateTime dob, string driverLicense, string address, string memberType) : base(userid, name, email, contact,dob, driverLicense, address, memberType)
        {
            this.MemberType = "Prime";
        }
    }
}
