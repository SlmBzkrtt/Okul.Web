using Okul.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Okul.Web.Controllers
{
    public class BolumController : Controller
    {
        OkulContext db;

        // GET: Birim
        public ActionResult Index()
        {
            return View(GetData());
        }

        public ActionResult Listeleme()
        {
            return View(GetData());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            db = new OkulContext();
            List<Birim> birimler = db.Birim.ToList();

            ViewBag.birimListesi = new SelectList(birimler, "birimID", "ad");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Bolum b)
        {
            db = new OkulContext();
            db.Bolum.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            TempData["bolumID"] = id;
            db = new OkulContext();
            Bolum bolum = db.Bolum.Where(x => x.bolumID == id).FirstOrDefault();

            db = new OkulContext();
            List<Birim> birimler = db.Birim.ToList();

            ViewBag.birimListesi = new SelectList(birimler, "birimID", "ad");

            return View(bolum);
        }

        [HttpPost]
        public ActionResult Duzenle(Bolum b)
        {
            int bolumID = TempData["bolumID"] == null ? 0 : (int)TempData["bolumID"];
            db = new OkulContext();
            Bolum bolum = db.Bolum.Where(x => x.bolumID == bolumID).FirstOrDefault();
            if (bolum != null)
            {
                bolum.ad = b.ad;
                bolum.birimID = b.birimID;
                db.SaveChanges();
            }

            return RedirectToAction("Listeleme");
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            db = new OkulContext();
            Bolum b = db.Bolum.Where(x => x.bolumID == id).FirstOrDefault();
            List<Kullanici> k = db.Kullanici.Where(x => x.bolumID == id).ToList();

            if (b != null && k == null)
            {
                db.Bolum.Remove(b);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Listeleme");
            }
            return RedirectToAction("Listeleme");
        }

        private List<Bolum> GetData()
        {
            db = new OkulContext();
            var data = db.Bolum.OrderBy(x => x.ad).ToList();
            return data;
        }
    }
}