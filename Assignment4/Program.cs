using System;
using System.Collections.Generic;

public class Circle
{
    public double Radius { get; private set; }
    private const double Pi = Math.PI;

    //Constructor which creates new circle and throws an expection if the radius is negative
    public Circle(double radius)
    {
        if (radius < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius cannot be negative.");
        } 
        Radius = radius;
 
    }

    //Calculates the area 
    public double getArea()
    {
        double Area = Pi * Radius * Radius;
        return Area;
    }

    //Calculates the perimeter
    public double getPermeter()
    {
        double perimeter = 2 * Pi * Radius;
        return perimeter;
    }

    //Determines if the points entered is within the circle or not
    public bool ContainsPoint(double x, double y)
    {
        return x * x + y * y <= Radius * Radius;
    }
}

class Program
{
    static void Main(string[] args)
    {
        //creates a list of the circle

        List<Circle> circles = CreateCircles();
        PrintCirclesInfo(circles);
        (double, double) point = GetUserPoint();
        CheckPointInCircles(circles, point);
    }

    //Prompts user to enter the list of the circle and the radius for the circle
    static List<Circle> CreateCircles()
    {
        int count = -1;
        while (count < 0)
        {
            Console.Write("Enter the number of circles to create: ");
            if (!int.TryParse(Console.ReadLine(), out count) || count < 0)
            {
                Console.WriteLine("Please enter a positive integer.");
                count = -1; // Reset count to ensure the loop continues
            }
        }

        var circles = new List<Circle>();
        for (int i = 0; i < count; i++)
        {
            double radius = -1;
            while (radius < 0)
            {
                Console.Write($"Enter the radius for circle {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out radius) || radius < 0)
                {
                    Console.WriteLine("Please enter a positive number for the radius.");
                    radius = -1; // Reset radius to ensure the loop continues
                }
            }
            try
            {
                circles.Add(new Circle(radius));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                i--; 
            }
        }

        return circles;
    }

    //Prints the area and permiter of each circles in the list
    static void PrintCirclesInfo(List<Circle> circles)
    {
        for (int i = 0; i < circles.Count; i++)
        {
            Console.WriteLine($"Circle {i + 1}:\n Area of the circle = {circles[i].getArea():F2} \n Perimeter of the Circle = {circles[i].getPermeter():F2}");
        }
    }

    //prompts the user for the two cordinates
    static (double, double) GetUserPoint()
    {
        Console.Write("Enter the X-coordinate of the point: ");
        double x = double.Parse(Console.ReadLine());
        Console.Write("Enter the Y-coordinate of the point: ");
        double y = double.Parse(Console.ReadLine());
        return (x, y);
    }

    //Checks whether the points that is provided by the user lies within the circle
    static void CheckPointInCircles(List<Circle> circles, (double X, double Y) point)
    {
        for (int i = 0; i < circles.Count; i++)
        {
            if (circles[i].ContainsPoint(point.X, point.Y))
            {
                Console.WriteLine($"Point ({point.X}, {point.Y}) is inside circle {i + 1}.");
            }
            else
            {
                Console.WriteLine($"Point ({point.X}, {point.Y}) is not inside circle {i + 1}.");
            }
        }
    }
}
