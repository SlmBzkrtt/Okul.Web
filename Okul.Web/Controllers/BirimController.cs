using Okul.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Okul.Web.Controllers
{
    public class BirimController : Controller
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
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Birim b)
        {
            db = new OkulContext();
            db.Birim.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            db = new OkulContext();
            Birim b = db.Birim.Where(x => x.birimID == id).FirstOrDefault();
            List<Kullanici> k = db.Kullanici.Where(x => x.birimID == id).ToList();
            if (b != null && k == null)
            {
                db.Birim.Remove(b);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Listeleme");
            }
            return RedirectToAction("Listeleme");
        }

        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            TempData["birimID"] = id;
            db = new OkulContext();
            Birim birim = db.Birim.Where(x => x.birimID == id).FirstOrDefault();
            return View(birim);
        }

        [HttpPost]
        public ActionResult Duzenle(Birim b)
        {
            int birimID = TempData["birimID"] == null ? 0 : (int)TempData["birimID"];
            db = new OkulContext();
            Birim birim = db.Birim.Where(x => x.birimID == birimID).FirstOrDefault();
            if (birim != null)
            {
                birim.ad = b.ad;
                db.SaveChanges();
            }

            return RedirectToAction("Listeleme");
        }

        private List<Birim> GetData()
        {
            db = new OkulContext();
            var data = db.Birim.OrderBy(x => x.ad).ToList();
            return data;
        }
    }
}