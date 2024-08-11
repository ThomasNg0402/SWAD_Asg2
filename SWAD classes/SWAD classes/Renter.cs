using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    class Renter : User
    {
        public Renter(int userid, string name, string email, string contact , DateTime dob, string driverLicense, string address, string memberType) : base(userid,name,email,contact) 
        {
            this.Dob = dob;
            this.DriverLicense = driverLicense;
            this.Address = address;
            this.MemberType = memberType;
            this.MonthlyRental = 0;
        }
        public DateTime Dob { get; set; }
        public string DriverLicense { get; set; }
        public string Address { get; set; }
        public string MemberType { get; set; }
        public int MonthlyRental { get; set; }
        public List<Booking>? BookingList { get; set; }

        public Booking? getBookingById(int id)
        {
            // Returns the booking
            foreach (Booking booking in this.BookingList)
            {
                Console.WriteLine(booking.BookingId);
                if (booking.BookingId == id)
                {
                    return booking;
                }
            }
            return null;
        }

        public int createBooking(DateTime startDate, DateTime returnDate)
        {
            if (startDate.Date>returnDate.Date || startDate < DateTime.Now)
            {
                Console.WriteLine("Your date range is invalid.");
                return -1;
            }
            int newBookingId = 1;
            if (BookingList != null)
            {
                newBookingId = BookingList.Count + 1;
            }
            Booking newBooking = new Booking(newBookingId,startDate,returnDate,this);
            return newBookingId;
        }

    }
}
