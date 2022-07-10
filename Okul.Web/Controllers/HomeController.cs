using FirmaApp.Web.Tools;
using Okul.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Okul.Web.Controllers
{
    public class HomeController : Controller
    {
        OkulContext db;

        public ActionResult BirimIndex()
        {
            db = new OkulContext();
            List<Birim> birim = db.Birim.OrderBy(x => x.ad).ToList();
            return View(birim);
        }

        public ActionResult BolumIndex()
        {
            db = new OkulContext();
            List<Bolum> bolum = db.Bolum.OrderBy(x => x.ad).ToList();
            return View(bolum);
        }

        public ActionResult OgrenciListele()
        {
            return View(GetData());
        }

        private List<Kullanici> GetData()
        {
            db = new OkulContext();
            int kullaniciID = ((Kullanici)Session["Kullanici"]).kullaniciID;
            var data = db.Kullanici.OrderBy(x => x.ad).Where(x => x.aktifMi == true && x.yetkiID == 2 && x.kullaniciID == kullaniciID).ToList();
            return data;
        }

        [HttpGet]
        public ActionResult OgrenciDuzenle(int id)
        {
            TempData["kullaniciID"] = id;
            db = new OkulContext();
            Kullanici kullanici = db.Kullanici.Where(x => x.kullaniciID == id).FirstOrDefault();
            ViewBag.cinsiyet = kullanici.cinsiyet;

            db = new OkulContext();
            List<Birim> birimler = db.Birim.ToList();
            List<Bolum> bolumler = db.Bolum.ToList();

            ViewBag.birimListesi = new SelectList(birimler, "birimID", "ad");
            ViewBag.bolumListesi = new SelectList(bolumler, "bolumID", "ad");

            return View(kullanici);
        }

        [HttpPost]
        public ActionResult OgrenciDuzenle(Kullanici k, HttpPostedFileBase foto)
        {
            int kullaniciID = TempData["kullaniciID"] == null ? 0 : (int)TempData["kullaniciID"];
            db = new OkulContext();
            Kullanici kullanici = db.Kullanici.Where(x => x.kullaniciID == kullaniciID).FirstOrDefault();
            if (kullanici != null)
            {
                if (k.foto == null)
                {
                    kullanici.ad = k.ad;
                    kullanici.soyad = k.soyad;
                    kullanici.eposta = k.eposta;
                    kullanici.sifre = k.sifre;
                    kullanici.cinsiyet = k.cinsiyet;
                    kullanici.birimID = k.birimID;
                    kullanici.bolumID = k.bolumID;
                    kullanici.ogrenciNo = k.ogrenciNo;
                    db.SaveChanges();
                }
                else if (k.foto != null)
                {
                    ImageManager resim = new ImageManager();
                    var sonuc = resim.Ekle(foto);
                    kullanici.foto = sonuc.ad;

                    kullanici.ad = k.ad;
                    kullanici.soyad = k.soyad;
                    kullanici.eposta = k.eposta;
                    kullanici.sifre = k.sifre;
                    kullanici.cinsiyet = k.cinsiyet;
                    kullanici.birimID = k.birimID;
                    kullanici.bolumID = k.bolumID;
                    kullanici.ogrenciNo = k.ogrenciNo;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("OgrenciListele");
        }
    }
}