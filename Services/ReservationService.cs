using APBD_Cw1_s18308.Enums;
using APBD_Cw1_s18308.Exceptions;
using APBD_Cw1_s18308.Model;

namespace APBD_Cw1_s18308.Services;

public class ReservationService : IReservationService
{
    private const int StudentMaxReservations = 2;
    private const int EmployeeMaxReservations = 5;
    private const decimal PenaltyPerDay = 10.00m;

    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly List<Reservation> _reservations = new List<Reservation>();

    public ReservationService(IEquipmentService equipmentService, IUserService userService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
    }

    private int GetMaxReservations(UserType type)
    {
        if (type == UserType.Student)
        {
            return StudentMaxReservations;
        }
        else
        {
            return EmployeeMaxReservations;
        }
    }

    public Reservation RentEquipment(int userId, int equipmentId, int rentalDays)
    {
        User user = GetUserOrThrow(userId);
        Equipment equipment = GetEquipmentOrThrow(equipmentId);
        ValidateAvailability(equipment);
        ValidateReservationLimit(user, userId);

        DateTime rentalDate = DateTime.Now;
        DateTime dueDate = rentalDate.AddDays(rentalDays);
        Reservation reservation = new Reservation(user, equipment, rentalDate, dueDate);

        equipment.ChangeStatus(EquipmentStatus.Rented);
        _reservations.Add(reservation);

        return reservation;
    }

    public Reservation RentEquipmentWithDates(int userId, int equipmentId, DateTime rentalDate, DateTime dueDate)
    {
        User user = GetUserOrThrow(userId);
        Equipment equipment = GetEquipmentOrThrow(equipmentId);
        ValidateAvailability(equipment);
        ValidateReservationLimit(user, userId);

        Reservation reservation = new Reservation(user, equipment, rentalDate, dueDate);

        equipment.ChangeStatus(EquipmentStatus.Rented);
        _reservations.Add(reservation);

        return reservation;
    }

    public Reservation ReturnEquipment(int reservationId, DateTime returnDate)
    {
        Reservation? reservation = null;
        foreach (Reservation r in _reservations)
        {
            if (r.Id == reservationId && r.IsActive)
            {
                reservation = r;
                break;
            }
        }

        if (reservation == null)
        {
            throw new ReservationNotFoundException(reservationId);
        }

        decimal penalty = 0;
        if (returnDate > reservation.DueDate)
        {
            int daysLate = (returnDate - reservation.DueDate).Days;
            penalty = daysLate * PenaltyPerDay;
        }

        reservation.CompleteReturn(returnDate, penalty);
        reservation.Equipment.ChangeStatus(EquipmentStatus.Available);

        return reservation;
    }

    public List<Reservation> GetActiveReservationsByUser(int userId)
    {
        List<Reservation> active = new List<Reservation>();
        foreach (Reservation r in _reservations)
        {
            if (r.User.Id == userId && r.IsActive)
            {
                active.Add(r);
            }
        }
        return active;
    }

    public List<Reservation> GetOverdueReservations()
    {
        List<Reservation> overdue = new List<Reservation>();
        foreach (Reservation r in _reservations)
        {
            if (r.IsOverdue)
            {
                overdue.Add(r);
            }
        }
        return overdue;
    }

    public List<Reservation> GetAll()
    {
        return new List<Reservation>(_reservations);
    }

    private User GetUserOrThrow(int userId)
    {
        User? user = _userService.GetById(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }
        return user;
    }

    private Equipment GetEquipmentOrThrow(int equipmentId)
    {
        Equipment? equipment = _equipmentService.GetById(equipmentId);
        if (equipment == null)
        {
            throw new EquipmentNotFoundException(equipmentId);
        }
        return equipment;
    }

    private void ValidateAvailability(Equipment equipment)
    {
        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new EquipmentUnavailableException(equipment.Id, equipment.Name, equipment.Status.ToString());
        }
    }

    private void ValidateReservationLimit(User user, int userId)
    {
        int activeCount = GetActiveReservationsByUser(userId).Count;
        int maxAllowed = GetMaxReservations(user.Type);
        if (activeCount >= maxAllowed)
        {
            throw new ReservationLimitExceededException(user.FullName, maxAllowed);
        }
    }
}
