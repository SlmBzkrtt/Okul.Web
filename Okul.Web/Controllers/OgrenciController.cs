using FirmaApp.Web.Tools;
using Okul.Model;
using Okul.Web.DTOs;
using Okul.Web.Enums;
using Okul.Web.Request.OgrenciRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Okul.Web.Controllers
{
    public class OgrenciController : Controller
    {
        OkulContext db;
        // GET: Ogrenci
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
        public ActionResult Ekle(EkleOgrenciRequest ekleOgrenciRequest, HttpPostedFileBase foto)
        {
            db = new OkulContext();
            if (foto != null)
            {
                ImageManager resim = new ImageManager();
                var sonuc = resim.Ekle(foto);
                //var sonuc2 = resim.Ekle(foto, 20000000);
                //var sonuc3 = resim.Ekle(foto, "personel/");
                if (sonuc.basariliMi == false)
                {
                    ViewBag.sonuc = sonuc.mesaj;
                    return View();
                }

                Kullanici kullanici = KullaniciEkleRequestMapToKullanici(ekleOgrenciRequest, sonuc);

                db.Kullanici.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Ekle");
        }



        public ActionResult BolumleriGetir(int? birimID)
        {
            db = new OkulContext();

            List<Bolum> bolumListe = db.Bolum.Where(b => b.birimID == birimID).ToList();

            var liste = new SelectList(bolumListe, "bolumID", "ad");

            return Json(liste, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Sil(int id)
        {
            int kullaniciID = TempData["kullaniciID"] == null ? 0 : (int)TempData["kullaniciID"];
            db = new OkulContext();
            Kullanici kullanici = db.Kullanici.Where(x => x.kullaniciID == kullaniciID).FirstOrDefault();

            kullanici.aktifMi = false;
            db.SaveChanges();

            return RedirectToAction("Listeleme");
        }

        [HttpGet]
        public ActionResult Duzenle(int id)
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
        public ActionResult Duzenle(Kullanici k, HttpPostedFileBase foto)
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

            return RedirectToAction("Listeleme");
        }

        private List<KullaniciDTO> GetData()
        {
            db = new OkulContext();
            List<Birim> birimler = db.Birim.ToList();
            List<Bolum> bolumler = db.Bolum.ToList();

            var data = db.Kullanici.OrderBy(x => x.ad).Where(x => x.aktifMi == true && x.yetkiID == (int)Yetkiler.Ogrenci).ToList();

            List<KullaniciDTO> kullaniciDTOs = new List<KullaniciDTO>();

            foreach (var item in data)
            {
                GetOgrenciler(birimler, bolumler, kullaniciDTOs, item);
            }

            return kullaniciDTOs;
        }

        private void GetOgrenciler(List<Birim> birimler, List<Bolum> bolumler, List<KullaniciDTO> kullaniciDTOs, Kullanici item)
        {
            KullaniciDTO kullaniciDTO = new KullaniciDTO();
            kullaniciDTO.KullaniciID = item.kullaniciID;
            kullaniciDTO.Ad = item.ad;
            kullaniciDTO.Soyad = item.soyad;
            kullaniciDTO.Foto = item.foto;
            kullaniciDTO.OgrenciNo = item.ogrenciNo;
            kullaniciDTO.BirimAdi = birimler.First(x => x.birimID == item.birimID).ad;
            kullaniciDTO.BolumAdi = bolumler.First(x => x.bolumID == item.bolumID).ad;
            kullaniciDTOs.Add(kullaniciDTO);
        }

        private Kullanici KullaniciEkleRequestMapToKullanici(EkleOgrenciRequest ekleOgrenciRequest, ImageResult sonuc)
        {
            Kullanici kullanici = new Kullanici();
            kullanici.ad = ekleOgrenciRequest.ad;
            kullanici.soyad = ekleOgrenciRequest.soyad;
            kullanici.eposta = ekleOgrenciRequest.eposta;
            kullanici.ogrenciNo = ekleOgrenciRequest.ogrenciNo;
            kullanici.sifre = ekleOgrenciRequest.sifre;
            kullanici.foto = sonuc.ad;
            kullanici.birimID = ekleOgrenciRequest.birimID;
            kullanici.bolumID = ekleOgrenciRequest.bolumID;
            kullanici.cinsiyet = ekleOgrenciRequest.cinsiyet;
            kullanici.aktifMi = true;
            kullanici.yetkiID = (int)Yetkiler.Ogrenci;
            return kullanici;
        }
    }
}