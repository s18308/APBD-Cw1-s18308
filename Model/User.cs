using APBD_Cw1_s18308.Enums;

namespace APBD_Cw1_s18308.Model;

public class User
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType Type { get; set; }

    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }

    public User(string firstName, string lastName, UserType type)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} ({Type})";
    }
}