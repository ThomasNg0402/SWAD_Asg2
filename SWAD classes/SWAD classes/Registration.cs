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
        public CarOwner CarOwner { get { return carOwner; } set { this.CarOwner = value; } }
        public Car Car { get { return car; } set { car = value; } }
    }
}
