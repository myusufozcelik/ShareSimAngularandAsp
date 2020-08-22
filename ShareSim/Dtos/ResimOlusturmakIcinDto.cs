using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Dtos
{
    public class ResimOlusturmakIcinDto
    {
        public ResimOlusturmakIcinDto() {
            EklenmeTarihi = DateTime.Now;
        }

        public string Url
        {
            get; set;
        }

        public IFormFile File
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
        public string PublicId
        {

            get; set;
        }
    }
}
