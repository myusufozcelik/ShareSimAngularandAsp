using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Dtos
{
    public class KategoriIcinListeDto
        
    {

        public int Id
        {
            get; set;
        }
        public string Isim
        {
            get; set;
        }
        public string PhotoUrl 
        {
            get; set;
        }
        public string Aciklama
        {
            get; set;
        }
    }
}
