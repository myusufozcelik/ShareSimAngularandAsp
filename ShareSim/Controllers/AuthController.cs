using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShareSim.Data;
using ShareSim.Dtos;
using ShareSim.Models;

namespace ShareSim.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository,IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("kayitOl")]
        public async Task<IActionResult> KayitOl([FromBody]KullaniciIcinKayit kullaniciIcinKayitDto)
        {
           if(await _authRepository.KullaniciVarMi(kullaniciIcinKayitDto.KullaniciAdi))
            {
                // Model hatası eklemek için;
                ModelState.AddModelError("KullaniciAdi", "Kullanıcı adı zaten var");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
           var KullaniciOlustur = new Kullanici
            {
                KullaniciAdi = kullaniciIcinKayitDto.KullaniciAdi
            };
            
            var olusturulanKullanici = await _authRepository.KayitOl(KullaniciOlustur,kullaniciIcinKayitDto.Password);

            return StatusCode(201);
        }
        [HttpPost("giris")]
            public async Task<ActionResult> GirisYap([FromBody] KullaniciIcinGirisDto kullaniciIcinGirisDto)
        {
            var kullanici = await _authRepository.GirisYap(kullaniciIcinGirisDto.KullaniciAdi, kullaniciIcinGirisDto.Password);

            if(kullanici==null)
            {
                return Unauthorized(); 
            }
            // eğer giriş değerleri doğru ise token göndermemiz gerekli
            var tokenHandler = new JwtSecurityTokenHandler();
            //AppSettings içerisinde token adında gizli anahtar değerinde token oluşturmuştuk.
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,kullanici.Id.ToString()),
                    new Claim(ClaimTypes.Name,kullanici.KullaniciAdi)
                }),
            Expires = DateTime.Now.AddDays(1), // Token'in kaç gün geçerli olduğunu belirtiriz.
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token); // Tokenin string değerini verir.

            return Ok(tokenString);
        }
    }
}