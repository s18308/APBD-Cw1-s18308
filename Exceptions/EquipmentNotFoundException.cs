namespace APBD_Cw1_s18308.Exceptions;

public class EquipmentNotFoundException(int equipmentId)
    : Exception($"Equipment with ID: {equipmentId} was not found.");