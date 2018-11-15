using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Christ3D.Application.Interfaces;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Christ3D.UI.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(_studentAppService.GetAll());
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        // 页面
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            try
            {
                // 视图模型验证
                if (!ModelState.IsValid)
                    return View(studentViewModel);
                // 执行添加方法
                _studentAppService.Register(studentViewModel);

                ViewBag.Sucesso = "Student Registered!";

                return View(studentViewModel);
            }
            catch(Exception e)
            {
                return View(e.Message);
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}