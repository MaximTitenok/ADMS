using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ADMS
{
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=8000;Database=ADMS;Username=API_ADMS;Password=1zx69uali");
        }
        internal DbSet<Department> Departments { get; set; }
        internal DbSet<Employee> Employees { get; set; }
        internal DbSet<Faculty> Faculties { get; set; }
        internal DbSet<Group> Groups { get; set; }
        internal DbSet<Position> Positions { get; set; }
        internal DbSet<Speciality> Specialities { get; set; }
        internal DbSet<Student> Students { get; set; }
        internal DbSet<Subject> Subjects { get; set; }

    }
}
