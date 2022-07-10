using Okul.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Okul.Web.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet] // sayfayı görmek istiyorum
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] // sayfadan veri gönderince 
        public ActionResult Index(string No, string sifre)
        {
            OkulContext db = new OkulContext();

            Kullanici personel = db.Kullanici.Where((x => x.eposta == No && x.sifre == sifre)).FirstOrDefault();
            Kullanici kullanici = db.Kullanici.Where((x => x.ogrenciNo == No && x.sifre == sifre)).FirstOrDefault();

            if (kullanici != null)
            {
                if (kullanici.aktifMi == true)
                {
                    Session["Kullanici"] = kullanici;
                    return RedirectToAction("OgrenciListele", "Home");
                }
            }
            else if (personel != null)
            {
                if (personel.aktifMi == true)
                {
                    Session["Kullanici"] = personel;
                    return RedirectToAction("Index", "Ogrenci");
                }
            }
            return View();
        }

        [HttpGet] // sayfayı görmek istiyorum
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}