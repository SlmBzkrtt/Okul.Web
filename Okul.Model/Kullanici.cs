namespace Okul.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        public int kullaniciID { get; set; }

        public int? yetkiID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(50)]
        public string soyad { get; set; }

        [StringLength(50)]
        public string eposta { get; set; }

        [StringLength(50)]
        public string personelNo { get; set; }

        [StringLength(50)]
        public string ogrenciNo { get; set; }

        [Required]
        [StringLength(50)]
        public string sifre { get; set; }

        [StringLength(150)]
        public string foto { get; set; }

        public int? birimID { get; set; }

        public int? bolumID { get; set; }

        [StringLength(10)]
        public string cinsiyet { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? kayitTarihi { get; set; }

        public bool? aktifMi { get; set; }

        public virtual Birim Birim { get; set; }

        public virtual Bolum Bolum { get; set; }

        public virtual Yetki Yetki { get; set; }
    }
}
