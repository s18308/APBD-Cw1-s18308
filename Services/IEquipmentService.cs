using APBD_Cw1_s18308.Model;

namespace APBD_Cw1_s18308.Services;

public interface IEquipmentService
{
    void Add(Equipment equipment);
    List<Equipment> GetAll();
    List<Equipment> GetAvailable();
    Equipment? GetById(int id);
    bool MarkAsUnavailable(int equipmentId);
}