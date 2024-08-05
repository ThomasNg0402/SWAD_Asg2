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
        private CarOwner carowner;
        private Car car;

    }
}
