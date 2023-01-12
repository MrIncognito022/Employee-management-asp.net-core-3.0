using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id = 1, Name ="Ahmed",Department="DICT",Email="m.ahmedqureshi1234@gmail.com"},
                new Employee(){Id = 2, Name ="Ali",Department="HR",Email ="Ali1233@Yahoo.com"},
                new Employee(){Id = 3, Name ="Hassan",Department ="Finance",Email ="Hassan3421@gmail.com"}
            };
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
    }
}
