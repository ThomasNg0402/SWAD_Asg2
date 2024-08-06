using System;
using System.Collections.Generic;
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
        private int carId;
        public int InsuranceId { get; set; }
        public string Provider { get; set; } = " ";
        public string CoverageDetails { get; set; } = " ";
        public int CarId { get; set;}
        
        public void add(int aCar)
        {
            this.CarId += aCar;
        }

        public void addInsurance(CarOwner aCarOwner, string provider, string coverageDetails, string startDate, string endDate)
        {
            Registration aReg = new Registration();
            aReg.StartDate = Convert.ToDateTime(startDate);
            aReg.EndDate = Convert.ToDateTime(endDate);
            aReg.CarOwner = aCarOwner;
            this.provider = provider;
            this.coverageDetails = coverageDetails;
        }

    }
}
