namespace APBD_Cw1_s18308.Model;

public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string Processor { get; set; }
    public int StorageGb { get; set; }
    public double ScreenSize { get; set; }

    public Laptop(string name, int ramGb, string processor, int storageGb, double screenSize) : base(name)
    {
        RamGb = ramGb;
        Processor = processor;
        StorageGb = storageGb;
        ScreenSize = screenSize;
    }

    public override string GetDetails()
    {
        return $"RAM: {RamGb}GB, Procesor: {Processor}, Storage: {StorageGb}GB, Display: {ScreenSize}in\"";
    }
}