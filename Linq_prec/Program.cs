using Linq_prec;

var context = new ApplicationDBContext();
//For example, Display FirstName of all employees.
var q1 = context.Employee.Select(x => x.FirstName);
foreach (var employee in q1)
{
    Console.WriteLine("\n {0}", employee);
}