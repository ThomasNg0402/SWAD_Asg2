using System;

namespace SWAD_Asg2
{
    class Insurance
    {
        private int insuranceId;
        private string provider;
        private string coverageDetails;
        public int InsuranceId { get; set; }
        public string Provider { get; set; }
        public string CoverageDetails { get; set; }
        private CarOwner myCarOwner;
        public CarOwner MyCarOwner
        {
            set
            {
                if (myCarOwner != value)
                {
                    myCarOwner = value;
                    value.MyCarInsurance = this;
                }
            }
        }
        public void add(string aCarOwner, int aCar)
        {
            this.aCarOwner = aCarOwner;
            this.aCar += aCar;
        }

    }
}

}

