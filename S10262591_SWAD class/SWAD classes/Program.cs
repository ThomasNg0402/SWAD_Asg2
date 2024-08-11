using SWAD_classes;


List<Car> carList = new List<Car>();
List<CarOwner> carOwnerList = new List<CarOwner>();

while (true)
{
    Console.WriteLine("Welcome to the Car Management System");
    Console.WriteLine("1. Register a new car");
    Console.WriteLine("2. Add insurance to a car");
    Console.WriteLine("3. Display registered cars and insurances");
    Console.WriteLine("4. Exit");

    Console.Write("Select an option: ");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            RegisterCar(carList);
            break;
        case 2:
            AddInsurance(carList, carOwnerList);
            break;

        case 3:
            DisplayRegisteredCarsAndInsurances(carList);
            break;
        case 4:
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

static void RegisterCar(List<Car> carList)
{
    Car newCar = new Car();

    while (true)
    {
        try
        {
            Console.Write("Enter Car ID: ");
            newCar.CarId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Car Make: ");
            newCar.Make = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newCar.Make)) throw new Exception("Car Make cannot be empty.");

            Console.Write("Enter Car Model: ");
            newCar.Model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newCar.Model)) throw new Exception("Car Model cannot be empty.");

            Console.Write("Enter Car Mileage: ");
            newCar.Mileage = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Car Year: ");
            newCar.Year = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Car Photo URL: ");
            newCar.Photo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newCar.Photo)) throw new Exception("Car Photo URL cannot be empty.");

            Console.Write("Enter Car Type: ");
            newCar.CarType = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newCar.CarType)) throw new Exception("Car Type cannot be empty.");

            Console.Write("Enter Rental Rate: ");
            newCar.RentalRate = Convert.ToInt32(Console.ReadLine());

            carList.Add(newCar);
            Console.WriteLine("The Car details has been successfully provided!\n");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Console.WriteLine("Please try again");
        }
    }
}

static void AddInsurance(List<Car> carList, List<CarOwner> carOwnerList)
{
    if (carList.Count == 0)
    {
        Console.WriteLine("No cars available. Please register a car first.\n");
        return;
    }

    CarOwner owner = new CarOwner();
    Insurance insurance = new Insurance();

    while (true)
    {
        try
        {
            Console.Write("Enter Car Owner Name: ");
            owner.Name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(owner.Name)) throw new Exception("Car Owner Name cannot be empty.");

            Console.Write("Enter Insurance ID: ");
            insurance.InsuranceId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Insurance Provider: ");
            insurance.Provider = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(insurance.Provider)) throw new Exception("Insurance Provider cannot be empty.");

            Console.Write("Enter Insurance Coverage Details: ");
            insurance.CoverageDetails = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(insurance.CoverageDetails)) throw new Exception("Coverage Details cannot be empty.");

            Console.Write("Enter Car ID to add insurance: ");
            int carId = Convert.ToInt32(Console.ReadLine());

            Car selectedCar = carList.Find(car => car.CarId == carId);
            if (selectedCar == null) throw new Exception("Car ID not found.");

            Console.Write("Enter Start Date (dd/MM/yyyy): ");
            DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Console.Write("Enter End Date (dd/MM/yyyy): ");
            DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

            Registration reg = new Registration(insurance, owner, selectedCar)
            {
                StartDate = startDate,
                EndDate = endDate
            };

            owner.AddInsurance(insurance, selectedCar);
            selectedCar.AddRegistration(reg);

            carOwnerList.Add(owner);

            Console.WriteLine("Insurance added, Car Owner added and car registered!\n");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Please try again.\n");
        }
    }
}


static void DisplayRegisteredCarsAndInsurances(List<Car> carList)
{
    if (carList.Count == 0)
    {
        Console.WriteLine("No cars registered.\n");
        return;
    }

    Console.WriteLine("\nRegistration List:");
    foreach (var car in carList)
    {
        Console.WriteLine($"Car ID: {car.CarId}, Make: {car.Make}, Model: {car.Model}, Mileage: {car.Mileage}, Year: {car.Year}, CarPhoto: {car.Photo}, Type: {car.CarType}, Rental Rate: {car.RentalRate}");

        var regList = car.GetRegistrations();
        if (regList.Count > 0)
        {
            foreach (var reg in regList)
            {
                var carOwner = reg.GetCarOwner();
                var insurance = carOwner.MyInsurance;

                Console.WriteLine($"\nRegistration ID: {reg.RegistrationId}, Start Date: {reg.StartDate.ToString("dd/MM/yyyy")}, End Date: {reg.EndDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"\nCar Owner: {carOwner.Name}");
                Console.WriteLine($"\nInsurance ID: {insurance.InsuranceId}, Provider: {insurance.Provider}, Coverage: {insurance.CoverageDetails}");
            }
        }
        else
        {
            Console.WriteLine("\nNo registrations found for this car.");
        }
    }

    Console.WriteLine();
}


