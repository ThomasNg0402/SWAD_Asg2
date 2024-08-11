using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace SWAD_classes
{
    public class ReserveCar
    {
        public bool reservedStatus {  get; set; } = false;
        public string CarId { get; set; }
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool DoorstepDelivery { get; set; }
        public decimal TotalCost { get; private set; }
        public string RenterId { get; set; }

        public ReserveCar(string renterId, string carId)
        {
            RenterId = renterId;
            CarId = carId;
        }

        public void SelectCar(string carId)
        {
            // Check availability and assign the car ID
            CarId = carId;
        }

        public void SetPickupLocation(string location)
        {
            PickupLocation = location;
        }

        public void SetReturnLocation(string location)
        {
            ReturnLocation = location;
        }

        public void SetRentalDuration(DateTime pickupDate, DateTime returnDate)
        {
            PickupDate = pickupDate;
            ReturnDate = returnDate;
        }

        public void ChooseDeliveryOption(bool doorstepDelivery)
        {
            DoorstepDelivery = doorstepDelivery;
        }

        public void CalculateTotalCost()
        {
            TotalCost = 100; // Placeholder value for cost calculation
        }

        public string GetReservationSummary()
        {
            return $"Car: {CarId}, Pickup: {PickupLocation}, Return: {ReturnLocation}, Duration: {PickupDate} - {ReturnDate}, Total Cost: {TotalCost:C}";
        }

        public void ConfirmBooking(int paymentType)
        {
            var paymentMethod = new CreditCard();
            var paymentMethod2 = new DebitCard();
            var paymentMethod3 = new DigitalWallet();
            try
            {
                switch (paymentType)
                {
                    // Case 0 is default cancel
                    case 0:
                        return;
                    // Payment type Credit card
                    case 1:
                        paymentMethod = new CreditCard();
                        // Number
                        Console.Write("Enter credit card number: ");
                        var input = Console.ReadLine();
                        if (input == null)
                        {
                            Console.Write("Credit card number cannot be empty. Another empty input to cancel: ");
                            input = Console.ReadLine();
                            if (input == null || input == "")
                            {
                                return;
                            }
                        }
                        paymentMethod.CardNumber = input;
                        // Expiry date
                        Console.Write("Enter credit card expiry date: ");
                        input = Console.ReadLine();
                        if (input == null)
                        {
                            Console.Write("Expiry date cannot be empty. Another empty input to cancel: ");
                            input = Console.ReadLine();
                            if (input == null || input == "")
                            {
                                return;
                            }
                        }
                        paymentMethod.ExpirationDate = Convert.ToDateTime(input);
                        // Setting Type
                        paymentMethod.Type = "Credit Card";
                        break;
                    // Payment type Debit card
                    case 2:
                        paymentMethod2 = new DebitCard();
                        // Number
                        Console.Write("Enter debit card number: ");
                        input = Console.ReadLine();
                        if (input == null)
                        {
                            Console.Write("Debit card number cannot be empty. Another empty input to cancel: ");
                            input = Console.ReadLine();
                            if (input == null || input == "")
                            {
                                return;
                            }
                        }
                        paymentMethod2.CardNumber = input;
                        // Expiry date
                        Console.Write("Enter dedit card expiry date: ");
                        input = Console.ReadLine();
                        if (input == null)
                        {
                            Console.Write("Expiry date cannot be empty. Another empty input to cancel: ");
                            input = Console.ReadLine();
                            if (input == null || input == "")
                            {
                                return;
                            }
                        }
                        paymentMethod2.ExpirationDate = Convert.ToDateTime(input);
                        // Setting Type
                        paymentMethod.Type = "Debit Card";
                        break;
                    // Payment type Digital Wallet
                    case 3:
                        // Expiry date
                        Console.Write("Enter digital wallet expiry date: ");
                        input = Console.ReadLine();
                        if (input == null)
                        {
                            Console.Write("Expiry date cannot be empty. Another empty input to cancel: ");
                            input = Console.ReadLine();
                            if (input == null || input == "")
                            {
                                return;
                            }
                        }
                        paymentMethod3.ExpirationDate = Convert.ToDateTime(input);
                        // Setting Type
                        paymentMethod.Type = "Debit Card";
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Invalid payment information. Enter \"R\" to retry, else cancelling.");
                if (Console.ReadLine() == "R" ||  Console.ReadLine() == "r") {
                    ConfirmBooking(paymentType);
                }
                return;
            }

            // Process payment and finalize booking
            try
            {
                Console.Write("Processing payment...");
                bool test = false;
                if (paymentType == 1)
                {
                    test = paymentMethod.AttemptPayment(this.TotalCost);
                }
                else if (paymentType == 2)
                {
                    test = paymentMethod2.AttemptPayment(this.TotalCost);
                }
                else if (paymentType == 3)
                {
                    test = paymentMethod3.AttemptPayment(this.TotalCost);
                }

                // Success
                if (test)
                {
                    Console.WriteLine("Success!");
                    // Send confirmation receipt
                    SendConfirmationReceipt();
                }
            }
            catch
            {
                Console.WriteLine("Payment failed. Cancelling booking...");
            }
            return;

        }

        public void SendConfirmationReceipt()
        {
            this.reservedStatus = true;
            // Logic to send a confirmation receipt to the renter
            Console.WriteLine("Reservation confirmed. Receipt sent.");
        }

        public void ModifyReservation()
        {
            // Logic to modify an existing reservation
        }

        public void CancelReservation()
        {
            // Logic to cancel an existing reservation
        }
    }

    public class PaymentService
    {
        public bool ProcessPayment(string renterId, decimal amount)
        {
            // Implement payment processing logic
            return true; // Placeholder for payment success
        }
    }
}
