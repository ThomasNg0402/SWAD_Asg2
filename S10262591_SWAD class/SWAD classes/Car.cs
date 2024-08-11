using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public string Photo { get; set; }
        public string CarType { get; set; }
        public int RentalRate { get; set; }
        private List<Registration> regList = new List<Registration>();

        public void AddRegistration(Registration reg)
        {
            regList.Add(reg);
        }

        public List<Registration> GetRegistrations()
        {
            return regList;
        }
        public void UpdateCarRegistration(int carId, Registration updatedReg)
        {
            for (int i = 0; i < regList.Count; i++)
            {
                if (regList[i].RegistrationId == carId)
                {
                    regList[i] = updatedReg;
                    break;
                }
            }
        }
    }
}
