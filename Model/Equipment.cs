using APBD_Cw1_s18308.Enums;

namespace APBD_Cw1_s18308.Model;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; private set; }

    protected Equipment(string name)
    {
        Id = _nextId++;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public void ChangeStatus(EquipmentStatus newStatus)
    {
        Status = newStatus;
    }

    public abstract string GetDetails();

    public override string ToString()
    {
        return $"[{Id}] {Name} (Status: {Status}) - {GetDetails()}";
    }
}
