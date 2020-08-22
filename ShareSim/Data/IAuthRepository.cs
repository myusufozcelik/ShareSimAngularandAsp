using ShareSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Data
{
  public  interface IAuthRepository
    {
        Task<Kullanici> KayitOl(Kullanici kullanici, string password);
        Task<Kullanici> GirisYap(string kullaniciAdi, string password);
        Task<bool> KullaniciVarMi(string kullaniciAdi);
    }
}
