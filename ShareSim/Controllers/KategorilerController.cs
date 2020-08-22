using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareSim.Data;
using ShareSim.Dtos;
using ShareSim.Models;

namespace ShareSim.Controllers
{
    [Produces("application/json")]
    [Route("api/kategoriler")]
    [ApiController]
    public class KategorilerController : ControllerBase
    {
        private Repository _repository;
        private IMapper _mapper;

        public KategorilerController(Repository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ActionResult GetKategoriler() // UYGULAMANIN ANA EKRANINDA KATEGORİLERİ LİSTELEMEK İÇİN 
        {
            var kategoriler = _repository.GetKategoriler();
            var kategorilerToReturn = _mapper.Map<List<KategoriIcinListeDto>>(kategoriler); // biz kategorileri veriyoruz kategoriicinlistedto ya map etmesini istiyoruz
            return Ok(kategorilerToReturn); 
            
        }
        [HttpPost]
        [Route("ekle")]
        public ActionResult KategoriEkle([FromBody]Kategori kategori) 
        {
            _repository.Ekle(kategori);
            _repository.HepsiniKaydet();
            return Ok(kategori);
            
        }
        [HttpGet]
        [Route("detay")]
        public ActionResult GetKategorilerById(int id) // Tüm kategoriler yerine tek bir kategori getirmek için ;
        {
            var kategori = _repository.GetKategoriById(id);
            var kategoriToReturn = _mapper.Map<KategoriIcinListeDto>(kategori);
            return Ok(kategoriToReturn);

        }

        //Kategori resimlerini Listelemek için ; 

        [HttpGet]
        [Route("resimler")]
        public ActionResult GetResimlerByKategoriler(int kategoriId)
        {
            var resimler = _repository.GetResimlerByKategori(kategoriId);
            return Ok(resimler);

        }

    }
}