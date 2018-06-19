using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCStudentDemo.Models;

namespace MVCStudentDemo.Controllers
{
    public class EnrolledsController : Controller
    {
        private StudentDemoAppContext db = new StudentDemoAppContext();

        // GET: Enrolleds
        public ActionResult Index()
        {
            var enrolleds = db.Enrolleds.Include(e => e.Class).Include(e => e.Student);
            return View(enrolleds.ToList());
        }

        // GET: Enrolleds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolled enrolled = db.Enrolleds.Find(id);
            if (enrolled == null)
            {
                return HttpNotFound();
            }
            return View(enrolled);
        }

        // GET: Enrolleds/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Description");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Firstname");
            return View();
        }

        // POST: Enrolleds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Grade,StudentId,ClassId")] Enrolled enrolled)
        {
            if (ModelState.IsValid)
            {
                db.Enrolleds.Add(enrolled);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Description", enrolled.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Firstname", enrolled.StudentId);
            return View(enrolled);
        }

        // GET: Enrolleds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolled enrolled = db.Enrolleds.Find(id);
            if (enrolled == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Description", enrolled.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Firstname", enrolled.StudentId);
            return View(enrolled);
        }

        // POST: Enrolleds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Grade,StudentId,ClassId")] Enrolled enrolled)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrolled).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Description", enrolled.ClassId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Firstname", enrolled.StudentId);
            return View(enrolled);
        }

        // GET: Enrolleds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolled enrolled = db.Enrolleds.Find(id);
            if (enrolled == null)
            {
                return HttpNotFound();
            }
            return View(enrolled);
        }

        // POST: Enrolleds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrolled enrolled = db.Enrolleds.Find(id);
            db.Enrolleds.Remove(enrolled);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
