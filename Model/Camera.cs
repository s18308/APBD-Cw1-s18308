namespace APBD_Cw1_s18308.Model;

public class Camera: Equipment
{
    public double Megapixels { get; set; }
    public bool HasStabilization { get; set; }
    public string LensType { get; set; }

    public Camera(string name, double megapixels, bool hasStabilization, string lensType) : base(name)
    {
        Megapixels = megapixels;
        HasStabilization = hasStabilization;
        LensType = lensType;
    }

    public override string GetDetails()
    {
        return $"Megapixels: {Megapixels}, Has stabilization: {(HasStabilization ? "Y" : "N")}, Lens: {LensType}";
    }
}