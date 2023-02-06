using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
                              IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }


        public IActionResult Details(int? id)
        {
            //throw new Exception("Error in details View");
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Department = employee.Department,
                Name = employee.Name,
                Email = employee.Email,
                //It is good to name  Property name here as Photopath
                ExistingPhotoPath = employee.AddPhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFilename = ProcessUploadedFiles(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    AddPhotoPath = uniqueFilename
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Here Id is binded uses Hidden Id field in Edit View
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photo != null)
                {
                    //Check the existing photo path using hidden property from view
                    if (model.ExistingPhotoPath != null)
                    {
                        //ExistingPhotoPath only provides the fileName to get a complete physical address use
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                             "images", model.ExistingPhotoPath);
                        //To delete a class use file class present in System.IO namespace
                        System.IO.File.Delete(filePath);
                    }
                    //ProcessUploadedFiles method save the uploaded file to images folder in wwwroot and return us a unique filename utilization Guid Method and Ihosting Interface
                    employee.AddPhotoPath = ProcessUploadedFiles(model);

                }


                // We want to update Employee Photo if user has provided new Photo
                // if the new photo is not provided then we want to keep the existing photo
                // How do we determine if user has provided a new Photo
                // We could use the photo property of model
                // Because the input field is bind to phot0 property of Employee Edit View Model
                // If user has provided a new photo then this field would not be null simply and user ha uploaded a new Photo

                _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        private string ProcessUploadedFiles(EmployeeCreateViewModel model)
        {
            string uniqueFilename = null;

            //If Model state is valid then Redirect to Details after edit else return to edit View 
            //and show validation errors
            if (model.Photo != null)
            {
                //To get physical path of wwwroot we use IhostingEnvirement
                string uploadsFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //We want filename in Images folder to be unique otherwise one file override the other
                //To ensure that use Guid (Global unique identifier)
                uniqueFilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filepath = Path.Combine(uploadsFolderPath, uniqueFilename);

                //To dispose filestream use using statement

                // since we wrapped the filestream with using block as soon as this block complete 
                // execution the filestream is properly disposed off
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    // model.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFilename;
        }
    }
}
