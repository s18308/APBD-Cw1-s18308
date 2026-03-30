namespace APBD_Cw1_s18308.Exceptions;

public class ReservationNotFoundException(int reservationId)
    : Exception($"Active reservation with ID: {reservationId} was not found.");

