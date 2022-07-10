using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Okul.Model
{
    public partial class OkulContext : DbContext
    {
        public OkulContext()
            : base("name=OkulContext")
        {
        }

        public virtual DbSet<Birim> Birim { get; set; }
        public virtual DbSet<Bolum> Bolum { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Yetki> Yetki { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Birim>()
                .HasMany(e => e.Bolum)
                .WithRequired(e => e.Birim)
                .WillCascadeOnDelete(false);
        }
    }
}