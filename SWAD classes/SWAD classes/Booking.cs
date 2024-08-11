using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class Booking
    {
        public int BookingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string? PickUpLocation { get; set; }
        public string? ReturnLocation { get; set; }
        public Renter Renter { get; set; }
        public Car? Car { get; set; }
        public bool doorstepDelivery { get; set; }
        public bool doorstepReturn { get; set; }

        public Booking(int bookingId, DateTime startDate, DateTime endDate, Renter Renter)
        {
            this.BookingId = bookingId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Status = "Unconfirmed";
            this.Renter = Renter;
            if (Renter.BookingList != null && Renter.BookingList.Count != 0)
            {
                Renter.BookingList.Add(this);
            }
            else
            {
                Renter.BookingList = new List<Booking>();
                Renter.BookingList.Add(this);
            }
           
            this.doorstepDelivery = false;
            this.doorstepReturn = false;
        }

        // Create Booking
        public void updateBooking(Car car)
        {
            this.Car = car;
        }

        public int getBookingLength()
        {
            return (this.EndDate - this.StartDate).Days;
        }

        // Calculate Cost
        public double[] CalculateCost()
        {
            try
            {
                // Cost before discount
                double cost = 0;
                // Cost after discount
                double totalCost = 0;
                // Discount
                double discount = 0;
                // Cost of renting without fees
                double mainCost = this.getBookingLength() * this.Car.RentalRate;
                // Fees
                double fees = 0;
                if (doorstepDelivery)
                {
                    // Add $10 for fee idk
                    fees += 10;
                }
                if (doorstepReturn)
                {
                    // For return delivery
                    fees += 10;
                }
                cost += mainCost + fees;
                if (this.Renter.MemberType == "Prime")
                {
                    // 10% discount
                    discount += cost * 0.1;
                    totalCost = cost - discount;
                }
                else
                {
                    totalCost = cost;
                }
                double[] batchCost = 
                    {
                        mainCost, fees, cost, discount, totalCost
                    };
                return batchCost;
            }
            catch
            {
                return new double[]
                    {
                        0,0,0,0
                    }; ;
            }
        }

        // ToString
        public override string ToString()
        {
            try
            {
                double[] calculateCost = this.CalculateCost();
                // Return string
                string returnString = $"==============================\n" +
                   $"Booking {this.BookingId}\n\n" +
                   $"Renter: {this.Renter.Name}\n" +
                   $"Status: {this.Status}" +
                   $"Booking period: {this.getBookingLength()} Days\n" +
                   $"Car owner: {this.Car.Owner.Name}\n" +
                   $"Owner Contact No.: {this.Car.Owner.ContactNo}\n" +
                   $"{this.Car.ToString()}\n\n" +
                   $"Pickup location: {this.PickUpLocation}\n" +
                   $"Return location: {this.ReturnLocation}\n" +
                   $"Booking Cost: \n" +
                   $"==============================\n\n" +
                   $"Renting for {this.getBookingLength()} days for ${this.Car.RentalRate}\n" +
                   $"${calculateCost[0]}\n\n";
                if (this.doorstepDelivery)
                {
                    returnString += $"Doorstep Delivery\n$10\n\n";
                }
                if (this.doorstepReturn)
                {
                    returnString += $"Return from location\n$10\n\n";
                }
                returnString += $"Total fees\n${calculateCost[1]}\n\n";
                returnString += $"Subtotal\n${calculateCost[2]}\n\n";
                if (this.Renter.MemberType == "Prime")
                {
                    returnString += $"BONUS! Prime members get 10% off!\n";
                }
                returnString += $"Discounts\n${calculateCost[3]}\n\n";
                returnString += $"Total\n${calculateCost[4]}\n\n";
                returnString += $"==============================";
                return returnString;
            }
            catch
            {
                return $"Booking {this.BookingId}";
            }
        }
    }
}
