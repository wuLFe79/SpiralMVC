namespace SpiralMVC.Models
{
    public class SpiralViewModel
    {
        public SpiralViewModel()
        {
            Items = new List<CartesionPoint>();
            OriginalDirection = "";
            Clockwise = true;
        }

        public string OriginalDirection { get; set; }
        public bool Clockwise { get; set; }

        public int? FinalNumber { get; set; }
        public List<CartesionPoint> Items { get; set; }
    }
}
