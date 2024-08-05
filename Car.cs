using System;

namespace SWAD_Asg2
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
        private List<Registration> regList;

        public void addRegistration(Registration reg)
        {
            this.reg = reg;
        }
        public void updateCarRegistered(int aCar)
        {
            this.aCar += aCar;
        }

    }
}
