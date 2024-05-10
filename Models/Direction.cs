namespace SpiralMVC.Models
{
    public class Direction
    {
        public Direction(string _val, string _name)
        {
            DirectionValue = _val;
            DirectionName = _name;
        }

        public string DirectionValue { get; set; }
        public string DirectionName { get; set; }

        public static List<Direction> PopulateDirections()
        {
            return new List<Direction>
            {
                new Direction("", "- Please Select -"),
                new Direction("U", "Up"),
                new Direction("R", "Right"),
                new Direction("D", "Down"),
                new Direction("L", "Left"),
            };
        }
    }
}
