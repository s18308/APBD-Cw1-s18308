using APBD_Cw1_s18308.Enums;
using APBD_Cw1_s18308.Model;

namespace APBD_Cw1_s18308.Services;

public class EquipmentService : IEquipmentService
{
    private readonly List<Equipment> _equipmentList = new List<Equipment>();

    public void Add(Equipment equipment)
    {
        _equipmentList.Add(equipment);
    }

    public List<Equipment> GetAll()
    {
        return new List<Equipment>(_equipmentList);
    }

    public List<Equipment> GetAvailable()
    {
        List<Equipment> available = new List<Equipment>();
        foreach (Equipment eq in _equipmentList)
        {
            if (eq.Status == EquipmentStatus.Available)
            {
                available.Add(eq);
            }
        }
        return available;
    }

    public Equipment? GetById(int id)
    {
        foreach (Equipment eq in _equipmentList)
        {
            if (eq.Id == id)
            {
                return eq;
            }
        }
        return null;
    }

    public bool MarkAsUnavailable(int equipmentId)
    {
        Equipment? equipment = GetById(equipmentId);
        if (equipment == null)
        {
            return false;
        }
        if (equipment.Status == EquipmentStatus.Rented)
        {
            return false;
        }
        equipment.ChangeStatus(EquipmentStatus.Unavailable);
        return true;
    }
}
