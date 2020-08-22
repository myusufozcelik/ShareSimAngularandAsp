using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareSim.Models;

namespace ShareSim.Data
{

    public class AuthRepository : IAuthRepository
    {
        private DataContext _context;

        public AuthRepository(DataContext _context)
        {
            this._context = _context;
      
        }

        public async Task<Kullanici> GirisYap(string kullaniciAdi, string password)
        {

            // ilk önce böyle bir kullanıcı veritabanında var mı kontrol et 
            // şifreyi salt ve hash leyip veritabanındaki ile kontrol et
            var kullanici = await _context.Kullanicilar.FirstOrDefaultAsync(x => x.KullaniciAdi == kullaniciAdi);
            if (kullanici == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, kullanici.PasswordHash, kullanici.PasswordSalt))
            {
                return null;
            }
            return kullanici;

        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
        {
            using (System.Security.Cryptography.HMACSHA512 hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // Hash hesabı için , yukarıda verilen passwordSalt bilgisi ile hash e çevirip for döngüsü ile tek tek kontrol et
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //Aynılar mı kontrol et
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<Kullanici> KayitOl(Kullanici kullanici, string password)
        {
            // password için önce salt işlemini yap sonra hashle bunun için  ;
            byte[] passwordHash, passwordSalt; // değişkenleri CreatePasswordHash metoduna gönderip üretmeye çalış
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            kullanici.PasswordHash = passwordHash;
            kullanici.PasswordSalt = passwordSalt;
            await _context.Kullanicilar.AddAsync(kullanici);
            
            await _context.SaveChangesAsync();

            return kullanici;   // KULLANICI KAYIT ISLEMİ TAMAMLANDI 

            //asenkronluk için ; 

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // HMACSHA512 algoritması ile Hash oluştur
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }


        public async Task<bool> KullaniciVarMi(string kullaniciAdi)
        {
            try
            {
                if (await _context.Kullanicilar.AnyAsync(x => x.KullaniciAdi == kullaniciAdi))
                {
                    return true;
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return false;
        }
    }
}
