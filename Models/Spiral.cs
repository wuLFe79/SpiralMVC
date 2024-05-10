namespace SpiralMVC.Models
{
    public class CartesionPoint
    {
        public CartesionPoint(int _x, int _y, string _value)
        {
            x = _x;
            y = _y;
            value = _value;
        }
        public int x;
        public int y;
        public string value;
    }
}
