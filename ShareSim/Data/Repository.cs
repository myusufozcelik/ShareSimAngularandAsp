using ShareSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Data
{
  public interface Repository
    {
        void Ekle<T>(T entity) where T:class; // jenerik kod
        void Sil<T>(T entity) where T:class; 
        bool HepsiniKaydet();

        List<Kategori> GetKategoriler();  // veri okuma kodları , Tüm kategoriyi getirir
        List<Resim> GetResimlerByKategori(int KategoriId); // resimlerde ki kategorinin id sini çek
        Kategori GetKategoriById(int kategoriId);
        Resim GetResim(int id);  


    }
}
