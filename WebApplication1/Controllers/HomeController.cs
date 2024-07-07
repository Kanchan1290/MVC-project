using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Security;
using System;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly PracticeProjectContext _context;
        private readonly IDataProtector _protector;

        public HomeController(PracticeProjectContext context, DataSecurityProvider provider, IDataProtectionProvider protectionProvider)
        {
            _context = context;
            _protector = protectionProvider.CreateProtector(provider.datakey);
        }

        public IActionResult Index()
        {
            var students = _context.StudentLists.ToList();
            var u = students.Select(e =>
            {
                e.EncrptedId = _protector.Protect(Convert.ToString(e.StudentId));
                return e;
            }).ToList();

            return View(u);
        }

        public IActionResult Details(string id)
        {
            int StudentId = Convert.ToInt32(_protector.Unprotect(id));
            var u = _context.StudentLists.FirstOrDefault(x => x.StudentId == StudentId);
            return View(u);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentListEdit u)
        {
            if (ModelState.IsValid)
            {
                StudentList studentList = new StudentList
                {
                    Email = u.Email,
                    StudentName = u.StudentName,
                    StudentId = u.StudentId,
                    StudentPassword = u.StudentPassword,
                    Faculty = u.Faculty
                };
                _context.Add(studentList);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            int StudentId = Convert.ToInt32(_protector.Unprotect(id));
            var k = _context.StudentLists.FirstOrDefault(x => x.StudentId == StudentId);
            if (k == null)
            {
                return NotFound();
            }

            StudentListEdit userEdit = new StudentListEdit
            {
                Email = k.Email,
                StudentName = k.StudentName,
                StudentId = k.StudentId,
                StudentPassword = k.StudentPassword,
                Faculty = k.Faculty
            };

            return View(userEdit);
        }

        [HttpPost]
        public IActionResult Update(StudentListEdit k)
        {
            if (ModelState.IsValid)
            {
                var user = _context.StudentLists.Find(k.StudentId);
                if (user == null)
                {
                    return NotFound();
                }
                user.Email = k.Email;
                user.StudentName = k.StudentName;
                user.StudentId = k.StudentId;
                user.StudentPassword = k.StudentPassword;
                user.Faculty = k.Faculty;

                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(k);
        }

        public IActionResult Delete(string id)
        {
            int StudentId = Convert.ToInt32(_protector.Unprotect(id));
            var u = _context.StudentLists.FirstOrDefault(x => x.StudentId == StudentId);
            if (u == null)
            {
                return NotFound();
            }

            _context.StudentLists.Remove(u);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GetUsers()
        {
            List<StudentList> students = _context.StudentLists.ToList();
            var u = students.Select(e =>
            {
                e.EncrptedId = _protector.Protect(Convert.ToString(e.StudentId));
                return e;
            });
            return PartialView("_GetUsers", u);
        }
    }
}
