namespace APBD_Cw1_s18308.Exceptions;

public class EquipmentUnavailableException(int equipmentId, string equipmentName, string status)
    : Exception($"Equipment \"{equipmentName}\" (ID: {equipmentId}) is not available (status: {status}).");