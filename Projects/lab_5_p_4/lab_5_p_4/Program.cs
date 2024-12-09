//Create a class named RBI with calculateInterest() method.Create another classes HDFC, SBI, ICICI which overrides calculateInterest() method.
using System;

class Program
{
    public static void Main(string[] args)
    {
        RBI r = new RBI();
        HDFC h = new HDFC();
        SBI s = new SBI();
        ICICI i = new ICICI();

        r.calculateInterest();
        h.calculateInterest();
        s.calculateInterest();
        i.calculateInterest();
    }
}
class RBI
{
    public virtual void calculateInterest()
    {
        Console.WriteLine("Interest of SBI");
    }
}
class HDFC : RBI
{
    public override void calculateInterest()
    {
        Console.WriteLine("Interest of RBI");
    }
}
class SBI : RBI
{
    public override void calculateInterest()
    {
        Console.WriteLine("Interest of SBI");
    }
}
class ICICI : RBI
{
    public override void calculateInterest()
    {
        Console.WriteLine("Interest of ICICI");
    }
}