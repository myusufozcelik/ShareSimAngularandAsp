using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Models
{
    public class Kullanici
    {

        public Kullanici()
        {
            Kategoriler = new List<Kategori>();
        }

        public int Id
        {
            get; set;
        }
        public string KullaniciAdi
        {
            get; set;
        }

        public byte[] PasswordHash
        {
            get; set;
        }
        public byte[] PasswordSalt
        {
            get; set;
        }

        public List<Kategori> Kategoriler
        {
            get; set;
        }
        
    }
}
