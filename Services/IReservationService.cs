using APBD_Cw1_s18308.Model;

namespace APBD_Cw1_s18308.Services;

public interface IReservationService
{
    Reservation RentEquipment(int userId, int equipmentId, int rentalDays);
    Reservation RentEquipmentWithDates(int userId, int equipmentId, DateTime rentalDate, DateTime dueDate);
    Reservation ReturnEquipment(int reservationId, DateTime returnDate);
    List<Reservation> GetActiveReservationsByUser(int userId);
    List<Reservation> GetOverdueReservations();
    List<Reservation> GetAll();
}
