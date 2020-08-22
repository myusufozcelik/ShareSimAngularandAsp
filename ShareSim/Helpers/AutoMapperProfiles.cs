using AutoMapper;
using ShareSim.Dtos;
using ShareSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Helpers
{
    public class AutoMapperProfiles : Profile
    {
     public AutoMapperProfiles()
        {  // Kategori modelinde photourl diye tanım yok o yüzden Automapper ile Automapper sınıfında tanımlamamız gerekli
            CreateMap<Kategori, KategoriIcinListeDto>().ForMember(dest=>dest.PhotoUrl,opt=>
            {
                opt.MapFrom(src => src.Resimler.FirstOrDefault(r => r.IlkMi).Url);
            }); // mapper kütüphanesine göre yaptık , photourl i map et kaynaktaki yani kategoride ki kategorinin resimlerinden ilk mi nin ik minin url inin altında 

            CreateMap<Kategori, KategoriIcinDetayDto>(); // Eğer birisi Kategori ile KategoriIcinDetayDto yu map ederse direkt map et!
            CreateMap<Resim, ResimOlusturmakIcinDto>();
            CreateMap<ResimOlusturmakIcinDto, Resim>();
        }   
       
    }
}
