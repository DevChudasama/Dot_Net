namespace lab_5_p_ex1
{
    class MyClass
    {
        public static void Main(String[] args)
        {
            Child obj = new Child();


            Console.Write(obj.add(10, 20));
        }
    }

    public class Parent
    {
        public int add(int x, int y)
        {
            return (x * y);
        }
    }

    public class Child : Parent
    {
        public int add(int x, int y)
        {
            return x + y;
        }
    }
}