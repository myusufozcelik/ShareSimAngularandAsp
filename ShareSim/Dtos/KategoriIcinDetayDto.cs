using ShareSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Dtos
{
    public class KategoriIcinDetayDto
    { // Kategorinin altındaki tüm resimleri çekmek için ;
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
         public List<Resim> Resimler
        {
            get; set;
        }
    }
}
