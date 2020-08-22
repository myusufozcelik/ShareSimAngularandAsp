using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShareSim.Data;
using ShareSim.Dtos;
using ShareSim.Helpers;
using ShareSim.Models;

namespace ShareSim.Controllers
{
    [Route("api/kategoriler/{kategoriId}/resimler")]
    [ApiController]
    public class ResimlerController : ControllerBase
    {
        private Repository _repository;
        private IMapper _mapper;
        IOptions<CloudinarySettings> _cloudinaryConfig;

        private Cloudinary _cloudinary;

        public ResimlerController(Repository repository,IMapper mapper , IOptions<CloudinarySettings> cloudinaryConfig) 
        {
            _repository = repository;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(

                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);  
            }
        [HttpPost] // FromBody ile dto yu isteriz
        public ActionResult KategoriIcinResimEkle(int kategoriId , [FromBody]ResimOlusturmakIcinDto resimOlusturmakIcinDto)
        {
            var kategori = _repository.GetKategoriById(kategoriId);

            if(kategori == null) {
                // data değiştirilirse diye hata döndür
                return BadRequest("Geçersiz kategori");

            }
            var currentUserId =int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if(currentUserId!=kategori.KullaniciId) // Kullanıcının farklı bir kullanıcının resimlerine ekleme yapmaması için
            {
                return Unauthorized();
            }

            // Gelen dosyayı okuyup kaydetmek için ;
            var file = resimOlusturmakIcinDto.File;

            var uploadResult = new ImageUploadResult();

            if(file.Length>0) // Yani dosya varsa

            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams {
                        File = new FileDescription(file.Name,stream)
                        
                    };
                    uploadResult = _cloudinary.Upload(uploadParams); // upload in yerini aldık 

                }
            }
            resimOlusturmakIcinDto.Url = uploadResult.Uri.ToString();
            resimOlusturmakIcinDto.PublicId = uploadResult.PublicId;

            var resim = _mapper.Map<Resim>(resimOlusturmakIcinDto);
            resim.Kategori = kategori; // resimin kategorisini belirttik

            if(!kategori.Resimler.Any(p=>p.IlkMi))
            {
                resim.IlkMi = true;
            }
            kategori.Resimler.Add(resim);
            if(_repository.HepsiniKaydet())
            {
                var photoToReturn = _mapper.Map<ResimCekmekIcinDto>(resim);
                return CreatedAtRoute("ResimCek", new { id = resim.Id }, photoToReturn);

            }
            // işlem gerçekleşmezse ; 
            return BadRequest("Resim eklenemedi");
        }
        [HttpGet("{id}",Name ="ResimCek")]
        public ActionResult ResimCek(int id)
        {
            var resimDb = _repository.GetResim(id);
            var resim = _mapper.Map<ResimCekmekIcinDto>(resimDb);

            return Ok(resim);
        } 
        }
    }
