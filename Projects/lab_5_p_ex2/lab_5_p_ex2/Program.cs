namespace lab_5_p_ex2
{
    class MyClass
    {
        public static void Main(String[] args)
        {
            //extra
            Cal c = new Cal();
            mydelegate c1 = new mydelegate(c.sub);//add or sub


            Console.Write(c1(10, 20));
        }
    }
    public delegate int mydelegate(int x, int y);

    public class Cal
    {
        public int add(int x, int y)
        {
            return x + y;
        }

        public int sub(int x, int y)
        {
            return x - y;
        }
    }
}