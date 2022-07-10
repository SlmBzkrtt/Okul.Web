using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Okul.Web.Request.OgrenciRequest
{
    public class EkleOgrenciRequest
    {
        public string ad { get; set; }

        public string soyad { get; set; }

        public string eposta { get; set; }

        public string personelNo { get; set; }

        public string ogrenciNo { get; set; }

        public string sifre { get; set; }

        public int? birimID { get; set; }

        public int? bolumID { get; set; }

        public string cinsiyet { get; set; }

        public DateTime? kayitTarihi { get; set; }

    }
}