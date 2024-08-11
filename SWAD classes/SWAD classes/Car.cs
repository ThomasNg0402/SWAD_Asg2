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
        public double Mileage { get; set; }
        public int Year { get; set; }
        public string Photo { get; set; }
        public string CarType { get; set; }
        public double RentalRate { get; set; }
        public List<Booking>? Bookings;
        public CarOwner Owner { get; set; }

        public Car(int carId, string make, string model, double mileage, int year, double RentalRate, string carType, CarOwner owner)
        {
            this.CarId = carId;
            this.Make = make;
            this.Model = model;
            this.Mileage = mileage;
            this.Year = year;
            this.RentalRate = RentalRate;
            this.Bookings = new List<Booking>();
            this.Photo = "";
            this.CarType = carType;
            Owner = owner;
        }

        // Add booking to bookingList
        public void addBooking(Booking booking)
        {
            // Adds
            if (this.Bookings != null)
            {
                this.Bookings.Add(booking);
            }
            // Creates if bookingList is empty
            else
            {
                this.Bookings = new List<Booking>()
                {
                    booking
                };
            }
            booking.Car = this;
        }

        // Get all dates in bookingList to check if reserved
        public bool checkDates(DateTime startDate, DateTime returnDate)
        {
            try
            {
                // If booking list is empty will be available
                if (this.Bookings == null)
                {
                    return true;
                }
                // Each booking
                foreach (Booking booking in this.Bookings)
                {
                    // check overlap of date where a.start <= b.end && b.start <= a.end;
                    if ((booking.StartDate <= returnDate) && (booking.EndDate >= startDate) && (booking.Status == "Confirmed"))
                    {
                        // Immediately return not available
                        return false;
                    }
                }

                // Returns true if available
                return true;
            }
            catch
            {
                // return false on other errors
                return false;
            }
            
        }
        public override string ToString()
        {
            return $"Car Id: {this.CarId}\n" +
                $"{this.Make} {this.Model} {this.Year} {this.CarType}\n" +
                $"Mileage: {this.Mileage}km\n" +
                $"Rental Rate: ${this.RentalRate}/Day\n" +
                $"Photo link: {this.Photo}\n" +
                $"Car Owner: {this.Owner.Name}\n" +
                $"Owner Contact No: {this.Owner.ContactNo}\n" +
                $"Bookings: {this.Bookings.Count}";
        }



        //public void updateCarRegistered(int aCar)
        //{
        //    this.aCar += aCar;
        //}
    }
}
