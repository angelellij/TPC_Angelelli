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
        private FireUrl Url { get; } = new FireUrl("Noticias");
        private Db Db { get; } = new Db();
        private string UrlEspacios { get; } = "";
        private Go<Noticia> Noticia { get; set; }

        public NoticiaNegocio() { }
        public NoticiaNegocio(Go<Noticia> noticia)
        {
            Noticia = new Go<Noticia>(noticia);
            UrlEspacios = Url.AddKey(Url.Espacios,
                    Url.AddKey(Noticia.Object.Espacio.Key,
                        Url.Root)
                    );
        }

        public async Task<IDictionary<string, Noticia>> GetAll()
        {
            IDictionary<string, Noticia> noticias = new Dictionary<string, Noticia>();

            var data = await Db.Client()
              .Child(Url.Espacios)
              .OnceAsync<Espacio>();

            foreach (var aux in data)
            {
                var datax = await Db.Client()
              .Child(Url.AddKey(Url.Espacios,
                    Url.AddKey(aux.Key,Url.Root)
                    ))
              .OnceAsync<Noticia>();

                foreach (var auxx in datax)
                {
                    noticias.Add(auxx.Key,auxx.Object);
                }
            }
            return noticias;
        }

        public async Task<Go<Noticia>> GetObject()
        {
            var x = await Db.Client()
                .Child(UrlEspacios)
                .OrderByKey()
                .EqualTo(Noticia.Key)
                .OnceSingleAsync<Noticia>();

            if (x == null) { Noticia.Key = null; }
            else { Noticia.Object = x; }
            return Noticia;
        }


        public async Task Create()
        {
            await Db.Create(Noticia.Object.ReturnNoticiaFire(), UrlEspacios);
        }

        public async Task Update()
        {
            await Db.Update(Noticia, 
                Url.AddKey(Url.Espacios,
                    Url.AddKey(UrlEspacios,Url.Root)
                    ));
        }

        public async Task Delete(string url)
        {
            await Db.Delete(url);
        }

    }

}

