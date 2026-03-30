namespace APBD_Cw1_s18308.Exceptions;

public class UserNotFoundException(int userId)
    : Exception($"User with ID: {userId} was not found.");