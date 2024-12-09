namespace lab_5_p_6
{
    class MyClass
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Enter number:");
            int x = int.Parse(Console.ReadLine());

            Fac c = new Fac();
            mydelegate d = new mydelegate(c.fac);

            Console.WriteLine(d(x));
            Console.ReadLine();
        }
    }

    public delegate int mydelegate(int x);
    public class Fac
    {
       public int fac(int x)
        {
            int fact = 1;

            for (int i = 2;i<=x;i++)
            {
                fact =  fact * i;
         
            }
            return fact;
        }
    }
}