using StudentList.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentList.Controllers
{
    public class StudentController : Controller
    {
        private StudentListContext db = new StudentListContext();

        public ActionResult List()
        {
            return View(db.Students.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                byte[] imageData = null;

                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                // установка массива байтов
                student.Photo = imageData;

                db.Students.Add(student);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();

            Student student = db.Students.Find(id);

            if (student == null)
                return HttpNotFound();

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                byte[] imageData = null;

                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                // установка массива байтов
                student.Photo = imageData;

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
  
            return RedirectToAction("List");
        }

        [Authorize]
        [HttpGet]      
        public ActionResult Delete(int? id)
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
            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
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

            db.Students.Remove(std);
            db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
