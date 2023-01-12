using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentsController : Controller
    {
        public string List()
        {
            return "List of departments";
        }
        public string Details()
        {
            return "Return Details of department";
        }
    }
}
