using APBD_Cw1_s18308.Enums;
using APBD_Cw1_s18308.Model;
using APBD_Cw1_s18308.Services;

EquipmentService equipmentService = new EquipmentService();
UserService userService = new UserService();
ReservationService reservationService = new ReservationService(equipmentService, userService);
ReportService reportService = new ReportService(equipmentService, userService, reservationService);

Console.WriteLine("--------- ADDING EQUIPMENT ---------");

equipmentService.Add(new Laptop("Dell", 16, "Intel i7", 512, 15.6));
equipmentService.Add(new Laptop("MacBook", 32, "Apple M4", 1024, 14.2));
equipmentService.Add(new Projector("Epson", "1024x768", 3600));
equipmentService.Add(new Projector("BenQ", "1920x1080", 3500));
equipmentService.Add(new Camera("Canon", 24.1, true, "18-55mm"));
equipmentService.Add(new Camera("Sony", 24.2, true, "16-50mm"));

foreach (Equipment eq in equipmentService.GetAll())
{
    Console.WriteLine(eq);
}


Console.WriteLine("\n--------- ADDING USERS ---------");

userService.Add(new User("Jan", "Kowalski", UserType.Student));
userService.Add(new User("Maciej", "Nowak", UserType.Student));
userService.Add(new User("Jon", "Doe", UserType.Employee));

foreach (User user in userService.GetAll())
{
    Console.WriteLine(user);
}



Console.WriteLine("\n--------- VALID RESERVATIONS ---------");

try
{
    Reservation r1 = reservationService.RentEquipment(1, 1, 7);
    Console.WriteLine($"OK: {r1}");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}

try
{
    Reservation r2 = reservationService.RentEquipmentWithDates(1, 2, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-6));
    Console.WriteLine($"OK: {r2}");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}

try
{
    Reservation r3 = reservationService.RentEquipment(3, 3, 10);
    Console.WriteLine($"OK: {r3}");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}



Console.WriteLine("\n--------- INVALID OPERATIONS ---------");

Console.WriteLine("Attempt: rent 3rd item for student (limit exceeded):");
try
{
    reservationService.RentEquipment(1, 4, 5);
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}

Console.WriteLine("Attempt: rent already rented equipment:");
try
{
    reservationService.RentEquipment(2, 1, 5);
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}

Console.WriteLine("\n--------- MARK AS UNAVAILABLE ---------");

bool marked = equipmentService.MarkAsUnavailable(6);
Console.WriteLine($"Camera (Id=6) marked as unavailable: {marked}");


Console.WriteLine("Attempt: rent unavailable camera:");
try
{
    reservationService.RentEquipment(2, 6, 5);
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}

Console.WriteLine("Available equipment:");
foreach (Equipment eq in equipmentService.GetAvailable())
{
    Console.WriteLine(eq);
}


Console.WriteLine("\n--------- ON-TIME RETURN ---------");

try
{
    Reservation returned1 = reservationService.ReturnEquipment(1, DateTime.Now);
    Console.WriteLine($"Returned: {returned1}");
    Console.WriteLine($"Penalty: {returned1.Penalty:C}");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}

Console.WriteLine("\n--------- LATE RETURN (5 days overdue) ---------");

try
{
    Reservation returned2 = reservationService.ReturnEquipment(2, DateTime.Now.AddDays(-1));
    Console.WriteLine($"Returned: {returned2}");
    Console.WriteLine($"Penalty: {returned2.Penalty:C}");
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");
}

Console.WriteLine("\n--------- ACTIVE RESERVATIONS FOR EMPLOYEE (Jon) ---------");

foreach (Reservation r in reservationService.GetActiveReservationsByUser(3))
{
    Console.WriteLine(r);
}


Console.WriteLine("\n--------- OVERDUE RESERVATIONS ---------");

try
{
    reservationService.RentEquipmentWithDates(3, 4, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-3));
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR adding overdue reservation: {ex.Message}");
}

foreach (Reservation r in reservationService.GetOverdueReservations())
{
    Console.WriteLine(r);
}

Console.WriteLine("\n--------- FINAL REPORT ---------");
Console.WriteLine(reportService.GenerateReport());