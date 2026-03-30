namespace APBD_Cw1_s18308.Model;

public class Reservation
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public User User { get; private set; }
    public Equipment Equipment { get; private set; }
    public DateTime ReservationDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsActive
    {
        get { return ReturnDate == null; }
    }

    public bool IsOverdue
    {
        get { return IsActive && DateTime.Now > DueDate; }
    }

    public Reservation(User user, Equipment equipment, DateTime reservationDate, DateTime dueDate)
    {
        Id = _nextId++;
        User = user;
        Equipment = equipment;
        ReservationDate = reservationDate;
        DueDate = dueDate;
        ReturnDate = null;
        Penalty = 0;
    }

    public void CompleteReturn(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }

    public override string ToString()
    {
        string status = IsActive ? "Active" : "Returned";
        string returnInfo = ReturnDate.HasValue ? ReturnDate.Value.ToShortDateString() : "---";
        return $"[{Id}] {User.FullName} | {Equipment.Name} | Rented: {ReservationDate.ToShortDateString()} | Due: {DueDate.ToShortDateString()} | Info: {returnInfo} | Penalty: {Penalty:C} | {status}";
    }
}
