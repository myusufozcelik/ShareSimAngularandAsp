using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Dtos
{
    public class ResimCekmekIcinDto
    { // Bu dto ları kullanarak controlleri yaz ! 
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
        public string PublicId
        {
            get; set;
        }
    }
}
