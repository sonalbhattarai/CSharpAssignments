using System;

public class Point
{
    private double x;
    private double y;

    public double X
    {
        get { return x; }
        set { x = value; }
    }

    public double Y
    {
        get { return y; }
        set { y = value; }
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public class Circle
{
    private Point center;
    private double radius;

    public Point Center
    {
        get { return center; }
        set { center = value; }
    }

    public double Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    public Circle(Point center, double radius)
    {
        Center = center;
        Radius = radius;
    }

    public double Perimeter()
    {
        return 2 * Math.PI * radius;
    }

    public double Surface()
    {
        return Math.PI * radius * radius;
    }

    public bool IsPointInside(Point point)
    {
        double distance = Math.Sqrt(Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2));
        return distance <= radius;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Circle[] circles = new Circle[3];

        // prompt the user for center points and radius for each circle
        for (int i = 0; i < circles.Length; i++)
        {
            Console.Write($"Enter x-coordinate for circle {i + 1}:");
            double centerX = double.Parse(Console.ReadLine());

            Console.Write($"Enter y-coordinate for circle {i + 1}:");
            double centerY = double.Parse(Console.ReadLine());

            Console.Write($"Enter the radius for circle {i + 1}:");
            double radius = double.Parse(Console.ReadLine());

            Point center = new Point(centerX, centerY);
            circles[i] = new Circle(center, radius);
        }

        foreach (Circle circle in circles)
        {
            Console.WriteLine("Circle with center ({0}, {1}) and radius {2}", circle.Center.X, circle.Center.Y, circle.Radius);
            Console.WriteLine("Perimeter: {0}", circle.Perimeter());
            Console.WriteLine("Surface: {0}", circle.Surface());
            Console.WriteLine();
        }

        Point testPoint = new Point(1, 2);
        foreach (Circle circle in circles)
        {
            Console.WriteLine("Is point ({0}, {1}) inside the circle with center ({2}, {3}) and radius {4}? {5}",
                testPoint.X, testPoint.Y, circle.Center.X, circle.Center.Y, circle.Radius, circle.IsPointInside(testPoint));
        }
    }
}

