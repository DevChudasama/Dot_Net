namespace lab_5_p_8
{
    class MyClass
    {
        public static void Main(String[] args)
        {
            Cal c = new Cal();
            mydelegate<int> d = new mydelegate<int>(c.add);
            mydelegate<double> d1 = new mydelegate<double>(c.add);

            Console.WriteLine(d(10, 20));
            Console.WriteLine(d1(10.25, 10.25));
        }
    }

    public delegate T mydelegate<T>(T x, T y);
    public class Cal
    {
        public int add(int x, int y)
        {
            return (x + y);
        }

        public double add(double x, double y)
        {
            return x + y;
        }
    }
}