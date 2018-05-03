using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPTask5_9.Models;
using ASPTask5_9.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ASPTask5_9.Controllers
{
    public class StudentController : Controller
    {
        public readonly IService _service;

        public StudentController(IService service)
        {
            _service = service;
        }
        
        //Inject в представление
        //Student
        public IActionResult Index()
        {
            return View();
        }

        //ПАРАМЕТР КОНСТРУКТОРА
        //Student/Details/1      
        public ActionResult Details(int id)
        {
            return View(_service.GetStudent(id));
        }

        //ПАРАМЕТР МЕТОДА
        //Student/Edit/1
        public ActionResult Edit(int id, [FromServices] IService service)
        {
            return View(service.GetStudent(id));
        }

        //HttpContext
        //Student/Create
        [HttpPost]
        public ActionResult Create([FromForm] StudentModel student)
        {
            var httpService = HttpContext.RequestServices.GetService<Service>();
            TryValidateModel(student);
            if (ModelState.IsValid)
            {
                httpService.AddStudent(student);
                return RedirectToAction(nameof(Index));
            }
            else return View(student);
        }

        //ActivatorUtilities
        //Student/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var instance = ActivatorUtilities.CreateInstance(HttpContext.RequestServices, typeof(DeleteClass), id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Student/Create
        public ActionResult Create()
        {
            return View();
        }

        //Student/Edit/1
        [HttpPost]
        public ActionResult Edit (int id, [FromForm] StudentModel student)
        {
            TryValidateModel(student);
            if (ModelState.IsValid)
            {
                _service.SetStudent(id, student);
                return RedirectToAction(nameof(Index));
            }
            else return View(student);
        }

        //Student/Delete/1
        public ActionResult Delete (int id)
        {
            return View(_service.GetStudent(id));
        }
    }

    public class DeleteClass
    {
        public DeleteClass(IService service, int id)
        {
            service.DeleteStudent(id);
        }
    }
}