using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class Registration
    {
        public int RegistrationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private CarOwner carOwner;
        private Car car;
        

        public Registration(Insurance insurance, CarOwner owner, Car car)
        {
            this.carOwner = owner;
            this.car = car;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now.AddYears(1); // Example duration
        }

        
        public CarOwner GetCarOwner()
        {
            return carOwner;
        }
    }

    

}
