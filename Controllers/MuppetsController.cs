using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MuppetShow.Models;

namespace MuppetShow.Controllers
{
    public class MuppetsController : Controller
    {
        
        private Database101Entities1 db = new Database101Entities1();

        // GET: Muppets
        public ActionResult Index()
        {
            return View(db.MuppetsTable.ToList());
        }

        // GET: Muppets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuppetsTable muppetsTable = db.MuppetsTable.Find(id);
            if (muppetsTable == null)
            {
                return HttpNotFound();
            }
            return View(muppetsTable);
        }

        // GET: Muppets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Muppets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,firstAppearance,image,video,likes")] MuppetsTable muppetsTable, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    var filename = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    photo.SaveAs(path);
                    muppetsTable.image = photo.FileName;
                    ViewBag.fileName = photo.FileName;
                }
                else { muppetsTable.image = "muppets.jpg"; }
                if (muppetsTable.video == null) { muppetsTable.video = "WgwoijMnp9k"; }
                db.MuppetsTable.Add(muppetsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(muppetsTable);
        }

        // GET: Muppets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuppetsTable muppetsTable = db.MuppetsTable.Find(id);
            if (muppetsTable == null)
            {
                return HttpNotFound();
            }
            return View(muppetsTable);
        }

        // POST: Muppets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,firstAppearance,image,video,likes")] MuppetsTable muppetsTable)
        {
            if (ModelState.IsValid)
            {
                if (muppetsTable.image == null) { muppetsTable.image = "muppets.jpg"; }
                if (muppetsTable.video == null) { muppetsTable.video = "WgwoijMnp9k"; }
                db.Entry(muppetsTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(muppetsTable);
        }

        // GET: Muppets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuppetsTable muppetsTable = db.MuppetsTable.Find(id);
            if (muppetsTable == null)
            {
                return HttpNotFound();
            }
            return View(muppetsTable);
        }

        // POST: Muppets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MuppetsTable muppetsTable = db.MuppetsTable.Find(id);
            db.MuppetsTable.Remove(muppetsTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Like(int Id)
        {
            MuppetsTable update = db.MuppetsTable.ToList().Find(u => u.Id == Id);
            update.likes += 1;
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
