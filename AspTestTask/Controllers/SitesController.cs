using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using AspTestTask.Models;

namespace AspTestTask.Controllers
{
    public class SitesController : Controller
    {
        private SitesDBEntities db = new SitesDBEntities();

        //
        // GET: /Sites/

        public ActionResult Index()
        {
            var sites = from p in db.Sites select p;
            return View(sites.ToList());
        }

        //
        // GET: /Sites/Create

        public ActionResult Create()
        {
            ViewBag.Hosting = new SelectList(db.HostingPlans, "Id", "Hosting");
            return View("CreateEdit", new Site());
        }

        //
        // POST: /Sites/Create

        [HttpPost]
        public ActionResult Create(Site site)
        {
            var image = WebImage.GetImageFromRequest("Logo");

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (image.Width > 256)
                    {
                        image.Resize(256, ((256 * image.Height) / image.Width));
                    }

                    var newPath = "~/Images/Logos/site_" + site.Id + "_logo." + image.ImageFormat;

                    image.Save(newPath);
                    site.Logo = Url.Content(newPath);
                }
                else
                {
                    site.Logo = null;
                }

                site.Last_edited = DateTime.Now;

                if ((from p in db.Sites where p.SiteName == site.SiteName select p).Count() == 0)
                {
                    db.Sites.Add(site);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    //  так и не разобрался как правильно передать текст ошибки, чтобы он отображался в Html.ValidationMessageFor
                    ModelState.AddModelError("Site", "Такое имя уже существует");
                }
            }

            ViewBag.Hosting = new SelectList(db.HostingPlans, "Id", "Hosting", site.Hosting);
            return View("CreateEdit", site);
        }

        //
        // GET: /Sites/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hosting = new SelectList(db.HostingPlans, "Id", "Hosting", site.Hosting);
            return View("CreateEdit", site);
        }

        //
        // POST: /Sites/Edit/5

        [HttpPost]
        public ActionResult Edit(Site site)
        {
            var image = WebImage.GetImageFromRequest("Logo");

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (image.Width > 256)
                    {
                        image.Resize(256, ((256 * image.Height) / image.Width));
                    }

                    var newPath = "~/Images/Logos/site_" + site.Id + "_logo." + image.ImageFormat;

                    image.Save(newPath);
                    site.Logo = Url.Content(newPath);
                }
                else
                {
                    site.Logo = null;
                }

                site.Last_edited = DateTime.Now;

                db.Entry(site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Hosting = new SelectList(db.HostingPlans, "Id", "Hosting", site.Hosting);
            return View("CreateEdit", site);
        }

        //
        // GET: /Sites/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        //
        // POST: /Sites/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Site site = db.Sites.Find(id);
            db.Sites.Remove(site);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}