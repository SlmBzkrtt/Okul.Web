using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Okul.Web.DTOs
{
    public class KullaniciDTO
    {
        public int KullaniciID { get; set; }
        public string Ad { get; set; }

        public string Soyad { get; set; }

        public string OgrenciNo { get; set; }

        public string Foto { get; set; }

        public string BirimAdi { get; set; }

        public string BolumAdi { get; set; }
    }
}