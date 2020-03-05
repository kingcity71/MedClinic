using System;
using MedClinic.Data;
namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new MedClinicContext())
            {
                var q = context.Patients;
            }
        }
    }
}
