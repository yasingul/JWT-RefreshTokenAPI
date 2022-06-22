using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SampleDataAccess
    {
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Yasin", LastName = "Gül" });
            output.Add(new() { FirstName = "Ali", LastName = "Geç" });
            output.Add(new() { FirstName = "Fatma", LastName = "Kaşıkçı" });
            output.Add(new() { FirstName = "Ece", LastName = "Gül" });

            Thread.Sleep(millisecondsTimeout: 3000);

            return output;
        }
    }
}