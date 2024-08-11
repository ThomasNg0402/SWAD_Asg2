using SWAD_classes;

internal class Program
{
    private static void Main(string[] args)
    {
        bool exit = false;
        // Dummy Data
        string[] validStations = { "iCar Station A @ Ang Mo Kio Ave 8 560711", "iCar Station B @ 13 Fernvale Ln 797496", "iCar Station C @ 128 Serangoon Ave 3 556111", "iCar Station D @ 7 Holland Vilage Way 275748" };
        Renter defaultRenter = new Renter(2,"Renter PPMan", "renter@gmail.com","+65 9123 4567",new DateTime(21/01/2001),"12980B", "House A of B", "Member");
        Renter renter = defaultRenter;
        PrimeRenter primeRenter = new PrimeRenter(3, "Mr Prime Man", "prime@rch.com", "+69 6969 6969", new DateTime(21 / 01 / 1991), "78122X", "12 Ngee Ann Polytechnic", "Prime");
        CarOwner owner = new CarOwner(1,"Mr Generic Car Owner John", "richguy@email.com","+65 9876 5432");
        Car car1 = new Car(1,"Volkswagen","Tiguan", 521.5, 2020, 125.50, "5-Seater", owner);
        Car car2 = new Car(2,"Honda","Civic", 712.9, 2019, 100, "5-Seater", owner);
        List<Car> carList = new List<Car>()
        {
            car1, car2
        };


        // Runs the program
        bool primeRenterMode = false;
        Console.WriteLine("Entering reserve car function.");
        while (!exit)
        {
            Console.Write($"Enter option to run\n - [1] Create Booking\n - [2] View Bookings\n - [3] Switch Prime mode (Currently: {primeRenterMode})\n - [0] Exit Program\nEnter option: ");
            string? userInput = Console.ReadLine();
            switch(userInput)
            {
                case "1":
                    Console.Write("Creating booking.\nEnter start date of booking: ");
                    string? startDate = Console.ReadLine();
                    Console.Write("Enter return date of booking: ");
                    string? returnDate = Console.ReadLine();
                    try
                    {
                        DateTime bookingStartDate = DateTime.Parse(startDate);
                        DateTime bookingReturnDate = DateTime.Parse(returnDate);
                        int bookingId = renter.createBooking(bookingStartDate,bookingReturnDate);
                        if (bookingId == -1)
                        {
                            throw new Exception(message: "Date invalid.");
                        }
                        List<Car> availableCars = new List<Car>();
                        List<int> availableCarId = new List<int>();
                        Console.WriteLine("Available cars:");
                        foreach (Car car in carList)
                        {
                            bool available = car.checkDates(bookingStartDate,bookingReturnDate);
                            if (available)
                            {
                                Console.WriteLine(car.ToString());
                                Console.WriteLine("==================================");
                                availableCars.Add(car);
                                availableCarId.Add(car.CarId);
                            }
                        }

                        // Select Car Id
                        Console.Write("Enter car id to book (\"E\" to cancel): ");
                        string? selectCar = Console.ReadLine();
                        if (selectCar == null || selectCar == "e" || selectCar == "E")
                        {
                            Booking? deleteBooking = renter.getBookingById(bookingId);
                            if (deleteBooking != null)
                            {
                                renter.BookingList.Remove(deleteBooking);
                            }

                        }
                        else
                        {
                            // Checking if inside available car list
                            try
                            {
                                int selectedId = Convert.ToInt32(selectCar);
                                bool checkCar = availableCarId.Contains(selectedId);
                                Console.WriteLine(checkCar);
                                if (!checkCar)
                                {
                                    throw new Exception(message: "Car Id not available.");
                                }
                                // SelectCar(carId)
                                Car selectedCar;
                                // Get booking
                                Booking? currentBooking = renter.getBookingById(bookingId);
                                if (currentBooking == null)
                                {
                                    throw new Exception(message: "Booking error.");
                                } 
                                foreach (Car car in availableCars)
                                {
                                    if (car.CarId == selectedId)
                                    {
                                        Console.WriteLine("Car selected.\n==============================");
                                        Console.WriteLine(car.ToString());
                                        Console.WriteLine("==============================");
                                        selectedCar = car;
                                        
                                        currentBooking.Car = selectedCar;
                                        selectedCar.addBooking(currentBooking);
                                        break;
                                    }
                                }
                                locationChange(currentBooking,validStations);
                                makePayment(currentBooking,validStations);
                                // Show current booking
                                if (renter.getBookingById(bookingId) != null)
                                {
                                    Console.WriteLine(currentBooking.ToString());
                                }
                                
                            }
                            catch (Exception e) 
                            {
                                Booking? deleteBooking = renter.getBookingById(bookingId);
                                if (deleteBooking != null)
                                {
                                    renter.BookingList.Remove(deleteBooking);
                                }
                                Console.WriteLine(e.Message); 
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message.ToString());
                    }
                    break;
                case "2":
                    if (renter.BookingList == null || renter.BookingList.Count == 0)
                    {
                        Console.WriteLine("No bookings.");
                    }
                    else
                    {
                        Console.WriteLine($"You have {renter.BookingList.Count} bookings.");
                        foreach (Booking booking in renter.BookingList)
                        {
                            Console.WriteLine($"Booking {booking.BookingId}");
                            Console.WriteLine(booking.ToString());
                        }
                    }
                    
                    break;
                case "3":
                    if (!primeRenterMode)
                    {
                        renter = primeRenter;
                        primeRenterMode = true;
                    }
                    else
                    {
                        renter = defaultRenter;
                        primeRenterMode = false;
                    }
                    break;
                case "0":
                    Console.WriteLine("Exiting program.");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }




        // Functions


        // Location change
        void locationChange(Booking booking, string[] validStations)
        {
            // For changing if added before (pickup)
            if (booking.PickUpLocation != null)
            {
                Console.Write($"Doorstep delivery: {booking.doorstepDelivery}");
                if (booking.doorstepDelivery)
                {
                    Console.WriteLine("Doorstep delivery fee: $10");
                }
                Console.WriteLine($"Old pickup location: {booking.PickUpLocation}");

            }

            // For changing if added before (dropoff)

            if (booking.ReturnLocation != null)
            {
                Console.Write($"Return from anywhere: {booking.doorstepReturn}");
                if (booking.doorstepReturn)
                {
                    Console.WriteLine("Returning fee: $10");
                }
                Console.WriteLine($"Old dropoff location: {booking.ReturnLocation}");

            }
            
            

            // Main function
            changePickup(booking, validStations);
            Console.WriteLine("==============================");
            changeReturn(booking, validStations);
            
        }

        // Change pickup location
        void changePickup(Booking booking, string[] validStations)
        {
            Console.Write("Do you want doorstep delivery?\n" +
                                        " - [1] Yes ($10 fee)\n" +
                                        " - [2] No (Default)\n" +
                                        "Enter option: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Doorstep delivery enabled.");
                    booking.doorstepDelivery = true;
                    break;
                case "2":
                    Console.WriteLine("No doorstep delivery. Ensure that your pickup location is at a valid station.");
                    booking.doorstepDelivery = false;
                    break;
                default:
                    Console.WriteLine("Defaulting.");
                    break;
            }
            Console.WriteLine("==============================");
            noDoorstepPickup(booking, validStations);
            Console.WriteLine("==============================");
            doorstepPickup(booking, validStations);
            Console.WriteLine("==============================");
            Console.WriteLine($"Pickup location: {booking.PickUpLocation}");
            Console.WriteLine($"Doorstep Delivery: {booking.doorstepDelivery}");
        }

        // Change return location
        void changeReturn(Booking booking, string[] validStations)
        {
            Console.Write("Do you want doorstep return?\n" +
                                        " - [1] Yes ($10 fee)\n" +
                                        " - [2] No (Default): ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Doorstep return enabled.");
                    booking.doorstepReturn = true;
                    break;
                case "2":
                    Console.WriteLine("No doorstep return. Ensure that your return location is at a valid station.");
                    booking.doorstepReturn = false;
                    break;
                default:
                    Console.WriteLine("Defaulting.");
                    break;
            }
            noDoorstepReturn(booking, validStations);
            doorstepReturn(booking, validStations);
            Console.WriteLine($"Return location: {booking.ReturnLocation}");
            Console.WriteLine($"Doorstep return: {booking.doorstepReturn}");
        }


        // doorstep delivery
        void doorstepPickup(Booking booking, string[] validStations)
        {
            if (booking.doorstepDelivery)
            {
                if (booking.Renter.Address != null)
                {
                    if (booking.PickUpLocation == null)
                    {
                        Console.WriteLine("Setting pickup location to your address.");
                        booking.PickUpLocation = booking.Renter.Address;
                    }
                    
                    Console.WriteLine($"Your pickup location is: {booking.Renter.Address}.");
                    Console.Write("Set a custom pickup address?\n" +
                        " - [1] Yes\n" +
                        " - [2] No (No change)\n" +
                        " - [0] Disable doorstep delivery\n" +
                        "Enter option: ");
                    string? option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            Console.Write("Enter custom pickup address: ");
                            string? newPickup = Console.ReadLine();
                            if (newPickup == null)
                            {
                                Console.WriteLine("Defaulting to previous address:\n" +
                                    $"{booking.PickUpLocation}");
                            }
                            else
                            {
                                Console.WriteLine($"New pickup location: {newPickup}");
                                booking.PickUpLocation = newPickup;
                            }
                            break;
                        case "2":
                            Console.WriteLine("No changes to pickup location.");
                            break;
                        case "0":
                            Console.WriteLine("Disabling doorstep delivery.");
                            booking.doorstepDelivery = false;
                            noDoorstepPickup(booking,validStations);
                            break;
                    }
                }
                else
                {
                    string? option = null;
                    while (option == null)
                    {
                        Console.WriteLine("Set custom pickup address: ");
                        option = Console.ReadLine();
                        if (option != null)
                        {
                            booking.PickUpLocation = option;
                            Console.WriteLine($"Setting pickup location to: {booking.PickUpLocation}");
                            break;
                        }
                    }
                    
                }
                
            }
        }
        // no doorstep pickup
        void noDoorstepPickup(Booking booking, string[] validStations)
        {
            if (!booking.doorstepDelivery)
            {
                bool loopPickup = true;
                // Pickup location loop
                while (loopPickup)
                {
                    Console.WriteLine("Enter pickup locations from out list of stations.");
                    int choice = 0;
                    foreach (string station in validStations)
                    {
                        choice++;
                        Console.WriteLine($" - [{choice}] {station}");
                    }
                    Console.WriteLine(" - [0] Enable doorstep delivery ($10)");
                    Console.Write("Enter option: ");
                    string? selectChoice = Console.ReadLine();
                    if (selectChoice != null)
                    {
                        try
                        {
                            int option = Convert.ToInt32(selectChoice);
                            if (option > 0 && option <= validStations.Length)
                            {
                                Console.WriteLine("Selected pickup location:");
                                Console.WriteLine(validStations[option - 1]);
                                booking.PickUpLocation = validStations[option - 1];
                                loopPickup = false;
                            }
                            else if (option == 0)
                            {
                                Console.WriteLine("Doorstep delivery enabled.");
                                booking.doorstepDelivery = true;
                                loopPickup = false;
                                doorstepPickup(booking, validStations);
                                break;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Invalid option.");
                        }
                    }
                }
            }
        }


        // return from anywhere
        void doorstepReturn(Booking booking, string[] validStations)
        {
            if (booking.doorstepReturn)
            {
                if (booking.Renter.Address != null)
                {
                    if (booking.ReturnLocation == null)
                    {
                        Console.WriteLine("Setting return location to your address.");
                        booking.ReturnLocation = booking.Renter.Address;
                    }

                    Console.WriteLine($"Your return location is: {booking.Renter.Address}.");
                    Console.Write("Set a custom return address?\n" +
                        " - [1] Yes\n" +
                        " - [2] No (No change)\n" +
                        " - [0] Disable doorstep return\n" +
                        "Enter option: ");
                    string? option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            Console.Write("Enter custom return address: ");
                            string? newReturn = Console.ReadLine();
                            if (newReturn == null)
                            {
                                Console.WriteLine("Defaulting to previous address:\n" +
                                    $"{booking.ReturnLocation}");
                            }
                            else
                            {
                                Console.WriteLine($"New return location: {newReturn}");
                                booking.ReturnLocation = newReturn;
                            }
                            break;
                        case "2":
                            Console.WriteLine("No changes to return location.");
                            break;
                        case "0":
                            Console.WriteLine("Disabling doorstep return.");
                            booking.doorstepReturn = false;
                            noDoorstepReturn(booking, validStations);
                            break;
                    }
                }
                else
                {
                    string? option = null;
                    while (option == null)
                    {
                        Console.WriteLine("Set custom return address: ");
                        option = Console.ReadLine();
                        if (option != null)
                        {
                            booking.ReturnLocation = option;
                            Console.WriteLine($"Setting return location to: {booking.ReturnLocation}");
                            break;
                        }
                    }

                }

            }
        }
        // no doorstep pickup
        void noDoorstepReturn(Booking booking, string[] validStations)
        {
            if (!booking.doorstepReturn)
            {
                bool loopReturn = true;
                // Pickup location loop
                while (loopReturn)
                {
                    Console.WriteLine("Enter return locations from out list of stations.");
                    int choice = 0;
                    foreach (string station in validStations)
                    {
                        choice++;
                        Console.WriteLine($" - [{choice}] {station}");
                    }
                    Console.WriteLine(" - [0] Enable doorstep return ($10)");
                    Console.Write("Enter option: ");
                    string? selectChoice = Console.ReadLine();
                    if (selectChoice != null)
                    {
                        try
                        {
                            int option = Convert.ToInt32(selectChoice);
                            if (option > 0 && option <= validStations.Length)
                            {
                                Console.WriteLine("Selected return location:");
                                Console.WriteLine(validStations[option - 1]);
                                booking.ReturnLocation = validStations[option - 1];
                                loopReturn = false;
                            }
                            else if (option == 0)
                            {
                                Console.WriteLine("Doorstep return enabled.");
                                booking.doorstepReturn = true;
                                loopReturn = false;
                                doorstepReturn(booking, validStations);
                                break;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Invalid option.");
                        }
                    }
                }
            }
        }


        // Payment (Positive return
        void makePayment(Booking booking, string[] validStations)
        {
            if (booking.Status == "Unconfirmed")
            {
                bool confirmLoop = true;
                while (confirmLoop)
                {
                    Console.WriteLine("Confirm booking?");
                    double total = booking.CalculateCost()[4];
                    Console.Write($" - [1] Confirm booking (Make payment for ${total})\n" +
                                  $" - [2] Modify booking\n" +
                                  $" - [3] View booking details\n" +
                                  $" - [0] Delete booking\n" +
                                  $"Enter option: ");
                    string? userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            Console.WriteLine("Making payment.");
                            Console.Write("Which payment method would you like to use?\n" +
                                          " - [1] Credit Card\n" +
                                          " - [2] Debit Card\n" +
                                          " - [3] Digital Wallet\n" +
                                          " - [0] Cancel payment\n" +
                                          "Enter option: ");
                            string? paymentInput = Console.ReadLine();
                            string? cardNo;
                            string? expDate;
                            string? cardType;
                            switch (paymentInput)
                            {
                                // Quick success?
                                case "1":
                                    Console.WriteLine("Payment by credit card.");

                                    do
                                    {
                                        Console.Write("Enter credit card number: ");
                                        userInput = Console.ReadLine();
                                        cardNo = userInput;
                                    }
                                    while (userInput == null);

                                    do
                                    {
                                        Console.Write("Enter card expiry date: ");
                                        userInput = Console.ReadLine();
                                        expDate = userInput;
                                    }
                                    while (userInput == null);

                                    cardType = "Credit";
                                    Console.WriteLine("Payment successful!");
                                    booking.Status = "Confirmed";
                                    Console.WriteLine("Booking confirmed!");
                                    confirmLoop = false;
                                    break;
                                case "2":
                                    Console.WriteLine("Payment by debit card.");

                                    do
                                    {
                                        Console.Write("Enter debit card number: ");
                                        userInput = Console.ReadLine();
                                        cardNo = userInput;
                                    }
                                    while (userInput == null);

                                    do
                                    {
                                        Console.Write("Enter card expiry date: ");
                                        userInput = Console.ReadLine();
                                        expDate = userInput;
                                    }
                                    while (userInput == null);

                                    cardType = "Debit";
                                    Console.WriteLine("Payment successful!");
                                    booking.Status = "Confirmed";
                                    Console.WriteLine("Booking confirmed!");
                                    confirmLoop = false;
                                    break;
                                case "3":
                                    Console.WriteLine("Payment by digital wallet.");
                                    do
                                    {
                                        Console.Write("Enter digital wallet expiry date: ");
                                        userInput = Console.ReadLine();
                                        expDate = userInput;
                                    }
                                    while (userInput == null);
                                    cardType = "Digital Wallet";
                                    Console.WriteLine("Payment successful!");
                                    booking.Status = "Confirmed";
                                    Console.WriteLine("Booking confirmed!");
                                    confirmLoop = false;
                                    break;
                                case "0":
                                    confirmLoop = false;
                                    Console.WriteLine("Cancelling payment.");
                                    break;
                            }
                            break;
                        case "2":
                            Console.WriteLine("Current booking\n" +
                                booking.ToString()
                                );
                            Console.Write("Modify booking\n" +
                                              $" - [1] Change pickup location\n" +
                                              $" - [2] Change return location\n" +
                                              $" - [0] Stop modifying booking\n" +
                                              $"Enter option: ");
                            string? modifyOption = Console.ReadLine();
                            switch (modifyOption)
                            {
                                case "1":
                                    changePickup(booking, validStations);
                                    break;
                                case "2":
                                    changeReturn(booking, validStations);
                                    break;
                                case "0":
                                    confirmLoop = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid option.");
                                    break;
                            }
                            break;
                        case "3":
                            Console.WriteLine(booking.ToString());
                            break;
                        case "0":
                            Booking? deleteBooking = booking;
                            if (deleteBooking != null)
                            {
                                booking.Renter.BookingList.Remove(deleteBooking);
                                booking.Car.Bookings.Remove(deleteBooking);
                            }
                            Console.WriteLine("Deleted booking.");
                            confirmLoop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }

                }
            }
        }
    }

}