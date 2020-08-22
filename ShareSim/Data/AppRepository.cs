using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareSim.Models;

namespace ShareSim.Data
{
    public class AppRepository : Repository // Repository class ini implemente edebilmesi için oluşturuldu
    {
        private DataContext _context; // veritabanı bağlantısına ulaşabilmek için oluşturuldu

        public AppRepository(DataContext context)
        {
            _context = context; // alt taraflarda da ulaşabilmek için global alanda tanımlayıp context değerini _context e attık
        }

        public void Ekle<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public Kategori GetKategoriById(int kategoriId)
        {
            var kategori = _context.Kategoriler.Include(k => k.Resimler).FirstOrDefault(k => k.Id == kategoriId); // First or Default tek bir kategoriyi çekmeye yarar
            return kategori;
        }

        public List<Kategori> GetKategoriler()
        {
            var kategoriler = _context.Kategoriler.Include(k=>k.Resimler).ToList(); // veritabanında ki kategorileri getir ve include ile resimleri de dahil edip getir
            return kategoriler;
        }

        public Resim GetResim(int id)
        {
            var resim = _context.Resimler.FirstOrDefault(r => r.Id == id);
            return resim;
        }

        public List<Resim> GetResimlerByKategori(int KategoriId) // context ile veritabanında ki datalara ulaştık 
        {
            var resim = _context.Resimler.Where(r => r.KategoriId == KategoriId).ToList();
            return resim;
        }

        public bool HepsiniKaydet()
        {
            return _context.SaveChanges() > 0; // insert veya update işlemi varsa true döndür
        }

        public void Sil<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
    }
}
