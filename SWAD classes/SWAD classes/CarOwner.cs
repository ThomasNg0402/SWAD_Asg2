using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class CarOwner : User
    {
        private List<Registration> regList;
        private Insurance myInsurance;
        public Insurance MyInsurance
        {
            set
            {
                if (myInsurance != value)
                  {
                    myInsurance = value;
                    value.MyCarOwner = this;
                }
            }
        }
        public void add insurance(string aInsurance, int aCar)
        {
            this.aInsurance = aInsurance;
            this.aCar += aCar;
        }

    }
}
