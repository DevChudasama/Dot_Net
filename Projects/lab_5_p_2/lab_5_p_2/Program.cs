//Write a program using method overloading by changing number of arguments to calculate area of square and rectangle.

using System;
class Program
{
    public static void Main(String[] args)
    {
        Console.WriteLine("Enter length of Square : ");
        double l = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter Length of Rectangle : ");
        double l2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter Breadth of Recangle : ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Area of Square = " + Area(l));
        Console.WriteLine("Area of Rectangle = " + Area(l2, b));
       
    }
    public static double Area(double l)
    {
        return l * l;
    }
    public static double Area(double l, double b)
    {
        return l * b;
    }
}