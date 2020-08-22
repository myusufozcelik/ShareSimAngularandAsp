using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Models
{
    public class Kategori
    {

        public  Kategori()
        {  // Aşağıda tanımlı resimler için Liste referansı olduğu için new yapmamız gerekli
            Resimler = new List<Resim>();
        }

        public int Id
        {
            get; set;
        }
        public string Isim
        {
            get; set;
        }

        public string Aciklama
        {
            get; set;
        }
        public int KullaniciId
        {
            get; set;
        }
        public List<Resim> Resimler
        { // Kategorinin resimleri çok 
            get; set;
        }
        public Kullanici Kullanici
        { // Kategorinin kullanıcısı bir tane 
            get; set;
        }
    }
}
