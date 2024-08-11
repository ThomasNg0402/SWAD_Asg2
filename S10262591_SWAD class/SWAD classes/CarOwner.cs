using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class CarOwner : User
    {
        private List<Registration> regList = new List<Registration>();
        
        private Insurance myInsurance;
        public Insurance MyInsurance
        {
            get { return myInsurance; }
            set
            {
                if (myInsurance != value)
                  {
                    myInsurance = value;
                    value.MyCarOwner = this;
                }
            }
        }

        public void AddInsurance(Insurance aInsurance, Car aCar)
        {
            
            aInsurance.AddInsurance(this, aCar);
            regList.Add(new Registration(aInsurance, this, aCar));
            
        }

        

    }
}
