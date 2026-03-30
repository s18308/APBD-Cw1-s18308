using APBD_Cw1_s18308.Enums;
using APBD_Cw1_s18308.Model;

namespace APBD_Cw1_s18308.Services;

public class ReportService : IReportService
{
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly IReservationService _reservationService;

    public ReportService(IEquipmentService equipmentService, IUserService userService, IReservationService reservationService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _reservationService = reservationService;
    }

    public string GenerateReport()
    {
        string result = "";

        result += "=== EQUIPMENT RENTAL REPORT ===\n\n";

        List<Equipment> allEquipment = _equipmentService.GetAll();
        int totalEquipment = allEquipment.Count;
        int availableCount = 0;
        int rentedCount = 0;
        int unavailableCount = 0;

        foreach (Equipment eq in allEquipment)
        {
            if (eq.Status == EquipmentStatus.Available)
            {
                availableCount++;
            }
            else if (eq.Status == EquipmentStatus.Rented)
            {
                rentedCount++;
            }
            else if (eq.Status == EquipmentStatus.Unavailable)
            {
                unavailableCount++;
            }
        }

        result += "EQUIPMENT:\n";
        result += $"Total: {totalEquipment}\n";
        result += $"Available: {availableCount}\n";
        result += $"Rented: {rentedCount}\n";
        result += $"Unavailable: {unavailableCount}\n\n";

        List<User> allUsers = _userService.GetAll();
        int totalUsers = allUsers.Count;
        int studentCount = 0;
        int employeeCount = 0;

        foreach (User user in allUsers)
        {
            if (user.Type == UserType.Student)
            {
                studentCount++;
            }
            else if (user.Type == UserType.Employee)
            {
                employeeCount++;
            }
        }

        result += "USERS:\n";
        result += $"Total: {totalUsers}\n";
        result += $"Students: {studentCount}\n";
        result += $"Employees: {employeeCount}\n\n";

        List<Reservation> allReservations = _reservationService.GetAll();
        int totalReservations = allReservations.Count;
        int activeCount = 0;
        int returnedCount = 0;
        int overdueCount = 0;
        decimal totalPenalties = 0;

        foreach (Reservation res in allReservations)
        {
            if (res.IsActive)
            {
                activeCount++;
            }
            else
            {
                returnedCount++;
            }

            if (res.IsOverdue)
            {
                overdueCount++;
            }

            totalPenalties += res.Penalty;
        }

        result += "RESERVATIONS:\n";
        result += $"Total: {totalReservations}\n";
        result += $"Active: {activeCount}\n";
        result += $"Returned: {returnedCount}\n";
        result += $"Overdue: {overdueCount}\n\n";

        result += $"TOTAL PENALTIES COLLECTED: {totalPenalties:C}\n\n";

        result += "==============================";

        return result;
    }
}
