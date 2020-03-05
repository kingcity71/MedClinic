using Microsoft.EntityFrameworkCore;
using MedClinic.Entity;
using System;

namespace MedClinic.Data
{
    public class MedClinicContext : DbContext
    {
        public MedClinicContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=MedClinic;" +
                "Trusted_Connection=True;");
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Conclusion> Conclusions { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PatientData> PatientDatas { get; set; }
    }
}
