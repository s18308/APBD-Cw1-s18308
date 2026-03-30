namespace APBD_Cw1_s18308.Exceptions;

public class ReservationLimitExceededException(string userFullName, int maxReservations)
    : Exception($"User {userFullName} has reached the reservation limit ({maxReservations}).");