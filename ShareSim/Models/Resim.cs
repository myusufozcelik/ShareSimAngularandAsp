using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Models
{
    public class Resim
    {
        public Resim()
        {

        }

        public int Id
        {
            get; set;
        }
        public string Url
        {
            get; set;
        }
        public string Aciklama
        {
            get; set;
        }
        public DateTime EklenmeTarihi

        {
            get; set;
        }
        public bool IlkMi
        {
            get; set;
        }
        public int KategoriId
        {
            get; set;
        }
        public string PublicId
        {
            get; set;
        }

        public Kategori Kategori { // Hangi kategoriye ait ? 
            get; set;
        }
    }
}
