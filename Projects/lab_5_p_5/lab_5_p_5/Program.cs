//Create a class Hospital with HospitalDetails() method.Create another classes Apollo, Wockhardt, Gokul_Superspeciality which overrides HospitalDetails() method
using System;
using System.Security.Claims;


class Program
{
    public static void Main(String[] args)
    {
        Hospital h = new Hospital();
        Appolo a = new Appolo();
        Wockhardt w = new Wockhardt();
        Gokul_Superspeciality g = new Gokul_Superspeciality();

        h.HospitalDetails();
        a.HospitalDetails();
        w.HospitalDetails();
        g.HospitalDetails();
       
    }
}

class Hospital
{
    public virtual void HospitalDetails()
    {
        System.Console.WriteLine("General Hospital Details (Parent Class)");
    }
}
class Appolo : Hospital
{
    override public void HospitalDetails()
    {
        Console.WriteLine("Appolo Hospital Details (Child Class)");
    }
}
class Wockhardt : Hospital
{
    override public void HospitalDetails()
    {
        Console.WriteLine("Wockhardt Hospital Details (Child Class)");
    }
}
class Gokul_Superspeciality : Hospital
{
    override public void HospitalDetails()
    {
        Console.WriteLine("Gokul_Superspeciality Hospital Details (Child Class)");
    }
}