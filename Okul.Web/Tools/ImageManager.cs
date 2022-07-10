using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace FirmaApp.Web.Tools
{
    public class ImageManager
    {
        public ImageResult Ekle(HttpPostedFileBase foto)
        {
            ImageResult imageResult = new ImageResult();

            string uzanti = Path.GetExtension(foto.FileName);
            if (!(uzanti == ".png" || uzanti == ".jpg" || uzanti == ".jpeg"))
            {
                imageResult.mesaj = "Lütfen .png, .jpg veya .jpeg uzantılı dosya giriniz.";
                imageResult.basariliMi = false;
                return imageResult;
            }
            if (foto.ContentLength > 1024000)
            {
                imageResult.mesaj = "Max 1MB dosya yükleyebilirsiniz.";
                imageResult.basariliMi = false;
                return imageResult;
            }

            string resimAdi = Guid.NewGuid().ToString() + uzanti;
            string dosyaYolu = HttpContext.Current.Server.MapPath("~/Content/images/kullanici/") + resimAdi;

            // Belirtilen dosya yoluna resmi kaydet
            try
            {
                foto.SaveAs(dosyaYolu);
            }
            catch (Exception ex)
            {
                imageResult.mesaj = ex.Message;
                imageResult.basariliMi = false;
                return imageResult;
            }

            imageResult.ad = resimAdi;
            imageResult.basariliMi = true;
            return imageResult;

        }
        public ImageResult Ekle(HttpPostedFileBase foto,int boyut)
        {
            ImageResult imageResult = new ImageResult();

            string uzanti = Path.GetExtension(foto.FileName);
            if (!(uzanti == ".png" || uzanti == ".jpg" || uzanti == ".jpeg"))
            {
                imageResult.mesaj = "Lütfen .png, .jpg veya .jpeg uzantılı dosya giriniz.";
                imageResult.basariliMi = false;
                return imageResult;
            }
            if (foto.ContentLength > boyut)
            {
                imageResult.mesaj = "Max dosya sınırını aştınız. Lütfen daha küçük bir boyutta resim yükleyiniz.";
                imageResult.basariliMi = false;
                return imageResult;
            }

            string resimAdi = Guid.NewGuid().ToString() + uzanti;
            string dosyaYolu = HttpContext.Current.Server.MapPath("~/Content/images/kullanici/") + resimAdi;

            // Belirtilen dosya yoluna resmi kaydet
            try
            {
                foto.SaveAs(dosyaYolu);
            }
            catch (Exception ex)
            {
                imageResult.mesaj = ex.Message;
                imageResult.basariliMi = false;
                return imageResult;
            }

            imageResult.ad = resimAdi;
            imageResult.basariliMi = true;
            return imageResult;

        }
        public ImageResult Ekle(HttpPostedFileBase foto, string dosyaYolu)
        {

            //WebImage img = new WebImage(foto.InputStream);
            //if (img.Width > 1000)
            //    img.Resize(1000, 1000);
            //img.Save("path");

            ImageResult imageResult = new ImageResult();

            string uzanti = Path.GetExtension(foto.FileName);
            if (!(uzanti == ".png" || uzanti == ".jpg" || uzanti == ".jpeg"))
            {
                imageResult.mesaj = "Lütfen .png, .jpg veya .jpeg uzantılı dosya giriniz.";
                imageResult.basariliMi = false;
                return imageResult;
            }
            if (foto.ContentLength > 1024000)
            {
                imageResult.mesaj = "Max dosya sınırını aştınız. Lütfen daha küçük bir boyutta resim yükleyiniz.";
                imageResult.basariliMi = false;
                return imageResult;
            }

            string resimAdi = Guid.NewGuid().ToString() + uzanti;
            string yol = HttpContext.Current.Server.MapPath("~/Content/images/") + dosyaYolu + resimAdi;

            // Belirtilen dosya yoluna resmi kaydet
            try
            {
                foto.SaveAs(yol);
            }
            catch (Exception ex)
            {
                imageResult.mesaj = ex.Message;
                imageResult.basariliMi = false;
                return imageResult;
            }

            imageResult.ad = resimAdi;
            imageResult.basariliMi = true;
            return imageResult;

        }
    }

}