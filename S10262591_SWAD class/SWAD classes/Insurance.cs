using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class Insurance
    {
        private int insuranceId;
        private string provider;
        private string coverageDetails;
        public int InsuranceId
        {
            get { return insuranceId; }
            set { insuranceId = value; }
        }
        public string Provider
        {
            get { return provider; }
            set { provider = value; }
        }
        public string CoverageDetails
        {
            get { return coverageDetails; }
            set { coverageDetails = value; }
        }

        private List<CarOwner> carOwners = new List<CarOwner>();

        
        public CarOwner MyCarOwner { get; set; }

       
        
        public void AddInsurance(CarOwner aCarOwner, Car aCar)
        {
            MyCarOwner = aCarOwner;
            //carOwners.Add(aCarOwner);
            // Add logic to handle insurance registration
            Registration aReg = new Registration(this, aCarOwner, aCar);
            
            aCar.AddRegistration(aReg);
        }
        

    }
}
