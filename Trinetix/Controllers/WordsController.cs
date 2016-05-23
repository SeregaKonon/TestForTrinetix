using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Trinetix;
using PagedList.Mvc;
using PagedList;

namespace Trinetix.Controllers
{
    public class WordsController : Controller
    {
        private DbFileContext db = new DbFileContext();

        // GET: Words
        public ActionResult Index(string searchBy, string search, int? page)
        {
            var words = db.Words.Include(w => w.Files);
            if (searchBy == "WordName")
            {
                return View(words.Where(x => x.WordName.StartsWith(search) || search == null)
                      .ToList().ToPagedList(page ?? 1, 50));
            }
            else
            {
                return View(words.Where(x => x.Files.FileName.StartsWith(search) || search == null)
                    .ToList().ToPagedList(page ?? 1, 50));
            }
        }

        // GET: Words/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Words words = db.Words.Find(id);
            if (words == null)
            {
                return HttpNotFound();
            }
            return View(words);
        }

        // GET: Words/Create
        public ActionResult Create()
        {
            ViewBag.FileId = new SelectList(db.Files, "FileId", "FileName");
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WordID,WordName,WordPositionCol,WordPositionRow,FileId")] Words words)
        {
            if (ModelState.IsValid)
            {
                words.WordID = Guid.NewGuid();
                db.Words.Add(words);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FileId = new SelectList(db.Files, "FileId", "FileName", words.FileId);
            return View(words);
        }

        // GET: Words/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Words words = db.Words.Find(id);
            if (words == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileId = new SelectList(db.Files, "FileId", "FileName", words.FileId);
            return View(words);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WordID,WordName,WordPositionCol,WordPositionRow,FileId")] Words words)
        {
            if (ModelState.IsValid)
            {
                db.Entry(words).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FileId = new SelectList(db.Files, "FileId", "FileName", words.FileId);
            return View(words);
        }

        // GET: Words/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Words words = db.Words.Find(id);
            if (words == null)
            {
                return HttpNotFound();
            }
            return View(words);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Words words = db.Words.Find(id);
            db.Words.Remove(words);
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
