using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareSim.Helpers
{
    public static class JwtExtension // Extension metodun static olması gerekir. Jwl kullanmamızın amacı; sitede bulunan
        // bazı özellikleri kullanıcı giriş yapınca gösterebilmeyi kontrol etmek içindir. Böylelikle kullanıcı giriş yapınca token tutup
        // veri alışverişi yapabiliriz.
    {
        public static void AddApplicationError(this HttpResponse response , string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Allow-Origin","*");
            response.Headers.Add("Access-Control-Expose-Header","Application-Error");

        }
    }
}
