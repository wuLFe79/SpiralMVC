using Microsoft.AspNetCore.Mvc;
using SpiralMVC.Models;
using System.Diagnostics;

namespace SpiralMVC.Controllers
{
    public class SpiralController : Controller
    {
        private readonly ILogger<SpiralController> _logger;
        private string curDirection = "";

        public SpiralController(ILogger<SpiralController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string originalDirection, bool clockwise, int finalNumber)
        {
            SpiralViewModel model = new SpiralViewModel();
            model.Clockwise = clockwise;
            model.OriginalDirection = originalDirection;
            model.FinalNumber = finalNumber;


            //populate model with spiral 
            LoadSpiral(model);

            //return model to view
            return View(model);
        }

        public void LoadSpiral(SpiralViewModel model)
        {
            model.Items.Clear();
            CartesionPoint lastPoint = new CartesionPoint(0, 0, "0");
            CartesionPoint nextPoint = new CartesionPoint(0, 0, "0");
            model.Items.Add(nextPoint);
            curDirection = model.OriginalDirection;

            //populate initial grid
            for (int it = 1; it <= model.FinalNumber; it++)
            {
                nextPoint = GetNextPoint(lastPoint, it.ToString());


                if (ChangeDirection(nextPoint, model))
                    curDirection = getNextDirection(model.Clockwise);

                model.Items.Add(new CartesionPoint(nextPoint.x, nextPoint.y, nextPoint.value));
                lastPoint = nextPoint;
            }

            //pad out grid with empty string for display purposes
            var minX = model.Items.Min(obj => obj.x);
            var minY = model.Items.Min(obj => obj.y);
            var maxX = model.Items.Max(obj => obj.x);
            var maxY = model.Items.Max(obj => obj.y);

            for (int x = minX; x <= maxX; x++)
            {
                if (!model.Items.Exists(obj => obj.x == x && obj.y == minY))
                    model.Items.Add(new CartesionPoint(x, minY, ""));

                if (!model.Items.Exists(obj => obj.x == x && obj.y == maxY))
                    model.Items.Add(new CartesionPoint(x, maxY, ""));
            }

            for (int y = minY; y <= maxY; y++)
            {
                if (!model.Items.Exists(obj => obj.x == minX && obj.y == y))
                    model.Items.Add(new CartesionPoint(minX, y, ""));

                if (!model.Items.Exists(obj => obj.x == maxX && obj.y == y))
                    model.Items.Add(new CartesionPoint(maxX, y, ""));
            }
        }

        private bool ChangeDirection(CartesionPoint curPoint, SpiralViewModel model)
        {
            if (curDirection == "U")
                return !model.Items.Exists(obj => obj.y >= curPoint.y);
            else if (curDirection == "R")
                return !model.Items.Exists(obj => obj.x >= curPoint.x);
            else if (curDirection == "D")
                return !model.Items.Exists(obj => obj.y <= curPoint.y);
            else
                return !model.Items.Exists(obj => obj.x <= curPoint.x);
        }

        private CartesionPoint GetNextPoint(CartesionPoint lastPoint, string value)
        {
            CartesionPoint nextPoint = lastPoint;
            nextPoint.value = value;

            if (curDirection == "U")
                nextPoint.y += 1;
            else if (curDirection == "R")
                nextPoint.x += 1;
            else if (curDirection == "D")
                nextPoint.y -= 1;
            else
                nextPoint.x -= 1;

            return nextPoint;
        }

        private string getNextDirection(bool isClockwise)
        {
            if (isClockwise)
            {
                if (curDirection == "U")
                    return "R";
                if (curDirection == "R")
                    return "D";
                if (curDirection == "D")
                    return "L";
                return "U";
            }
            else
            {
                if (curDirection == "U")
                    return "L";
                if (curDirection == "L")
                    return "D";
                if (curDirection == "D")
                    return "R";
                return "U";
            }
        }

    }
}
