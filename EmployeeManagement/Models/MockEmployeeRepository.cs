using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id = 1, Name ="Ahmed",Department=Dept.HR,Email="m.ahmedqureshi1234@gmail.com"},
                new Employee(){Id = 2, Name ="Ali",Department=Dept.IT,Email ="Ali1233@Yahoo.com"},
                new Employee(){Id = 3, Name ="Hassan",Department =Dept.IT,Email ="Hassan3421@gmail.com"}
            };
        }

        public Employee Add(Employee employee)
        {
           employee.Id =  _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            Employee e = _employeeList.Find(x => x.Id == id);
            return e;
        }

        public Employee Update(Employee employeechanges)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == employeechanges.Id);
            if (employee != null)
            {
                employee.Name = employeechanges.Name;
                employee.Department = employeechanges.Department;
                employee.Email = employeechanges.Email;
            }
            return employee;
        }
    }
}
