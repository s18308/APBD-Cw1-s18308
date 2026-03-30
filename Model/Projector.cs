namespace APBD_Cw1_s18308.Model;

public class Projector: Equipment
{
    public string Resolution { get; set; }
    public int Lumens { get; set; }

    public Projector(string name, string resolution, int lumens) : base(name)
    {
        Resolution = resolution;
        Lumens = lumens;
    }

    public override string GetDetails()
    {
        return $"Resolution: {Resolution}, Lumens: {Lumens}";
    }
}
