using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Firebase.Database.Query;

namespace Negocio
{
    public class NoticiaNegocio
    {
        private FireUrl Url { get; } = new FireUrl("noticias");
        private Db Db { get; } = new Db();
        public async Task<List<Noticia>> GetAll()
        {
            List<Noticia> noticias = new List<Noticia>();

            var espaciosAux = await Db.Client()
              .Child(Url.Espacios)
              .OnceAsync<Espacio>();

            foreach (var espaciox in espaciosAux)
            {
                var noticiasAux = await Db.Client()
              .Child(Url.AddKeyToUrl(Url.Espacios,
                    Url.AddKeyToUrl(espaciox.Key,Url.Root)
                    ))
              .OnceAsync<Noticia>();

                foreach (var noticia in noticiasAux)
                {
                    noticias.Add(new Noticia(noticia));
                }
            }
            return noticias;
        }
       
        public async Task Create(Noticia noticia)
        {
            await Db.Create(noticia.ReturnNoticiaFire(),
                Url.AddKeyToUrl(Url.Espacios,
                    Url.AddKeyToUrl(noticia.Espacio.Id,
                        Url.Root)
                    ));
        }

        public async Task Update(Noticia Noticia, string url)
        {
            await Db.Update(Noticia, 
                Url.AddKeyToUrl(Url.Espacios,
                    Url.AddKeyToUrl(url,Url.Root)
                    ));
        }

        public async Task Delete(string url)
        {
            await Db.Delete(url);
        }

    }

}

