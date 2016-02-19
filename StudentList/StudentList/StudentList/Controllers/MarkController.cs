using StudentList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentList.Controllers
{
    public class MarkController : Controller
    {
        //
        // GET: /Mark/

        StudentListContext db = new StudentListContext();

        [HttpGet]
        public ActionResult List(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            Student std = db.Students.Find(id);

            if (std == null)
            {
                return HttpNotFound();
            }

            std.Marks = db.Marks.Where(m => m.StudentId == std.Id);

            return View(std);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create(int? studentId)
        {

            if (studentId == null)
            {
                return HttpNotFound();
            }

            Student std = db.Students.Find(studentId);

            if (std == null)
            {
                return HttpNotFound();
            }

            ViewBag.routeId = studentId;

            return View();
        }


        [HttpPost]
        public ActionResult Create(Mark mark, int studentId)
        {
            mark.StudentId = studentId;

            db.Marks.Add(mark);
            db.SaveChanges();
            return RedirectToAction("List", new { id = studentId });
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int? id, int? studentId)
        {
            if (id == null || studentId == null)
                return HttpNotFound();

            Mark mark = db.Marks.Find(id);

            Student std = db.Students.Find(studentId);

            if (std == null || mark == null)
                return HttpNotFound();

            return View(mark);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id, int studentId)
        {
            Mark mark = db.Marks.Find(id);

            mark.StudentId = studentId;

            if (mark == null)
                return HttpNotFound();

            db.Marks.Remove(mark);
            db.SaveChanges();
            return RedirectToAction("List", new { id = studentId });
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id, int? studentId)
        {
            if (id == null || studentId == null)
                return HttpNotFound();

            Mark mark = db.Marks.Find(id);

            Student std = db.Students.Find(studentId);

            if (mark == null || std == null)
                return HttpNotFound();

            return View(mark);
        }

        [HttpPost]
        public ActionResult Edit(Mark mark, int studentId)
        {
            mark.StudentId = studentId;

            db.Entry(mark).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List", new { id = studentId });
        }
    }
}
