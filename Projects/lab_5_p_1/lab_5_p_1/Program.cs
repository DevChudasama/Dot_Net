using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace lab_5_p_1
{

    class program
    {
        public static void Main(string[] args)
        {
            //  Write a program using method overloading by changing datatype of arguments to perform addition of two integer numbers and two float numbers
            Console.WriteLine("Enter number:");
            int a = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number:");
            int b = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number:");
            float x = float.Parse(Console.ReadLine()); 
            
            Console.WriteLine("Enter number:");
            float y = float.Parse(Console.ReadLine());


            Console.WriteLine("Ans"+ add(a,b));
            Console.WriteLine("Ans" + add(x, y));
        }
        public static int add(int a, int b) { 
            return a + b;
        }

        public static float add(float x, float y)
        {
            return x + y;
        }
    }
}
