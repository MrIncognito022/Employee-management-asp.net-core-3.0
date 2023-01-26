using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    
    public static class ModelBuilderExtensions
    {
        //This is an extension method
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Ali",
                   Department = Dept.IT,
                   Email = "Ali123@outlook.com"
               },
               new Employee
               {
                   Id = 2,
                   Name = "Kashif",
                   Department = Dept.Payroll,
                   Email = "Kashifdfs@outlook.com"
               });
        }
    }
}
