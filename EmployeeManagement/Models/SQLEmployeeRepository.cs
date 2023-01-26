using System.Collections.Generic;

namespace EmployeeManagement.Models
{

    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;
        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
            }
            context.SaveChanges();
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            return employee;
        }

        public Employee Update(Employee employeechanges)
        {
            var employee = context.Employees.Attach(employeechanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeechanges;

        }
    }
}
