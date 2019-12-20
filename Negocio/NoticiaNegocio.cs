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
        private Go<Noticia> Noticia { get; set; }

        public NoticiaNegocio() { }
        public NoticiaNegocio(Go<Noticia> noticia)
        {
            Noticia = new Go<Noticia>(noticia);
        }

        public async Task<List<Go<Noticia>>> GetAll()
        {
            List<Go<Noticia>> noticias = new List<Go<Noticia>>();

            var data = await Db.Client()
              .Child(Url.Root)
              .OnceAsync<Noticia>();

            foreach (var aux in data)
            {
               noticias.Add(new Go<Noticia>(aux));
            }
            return noticias;
        }

        public async Task<Go<Noticia>> GetObject()
        {
            var x = await Db.Client()
                .Child(Url.Root)
                .OrderByKey()
                .EqualTo(Noticia.Key)
                .OnceSingleAsync<Noticia>();

            if (x == null) { Noticia.Key = null; }
            else { Noticia.Object = x; }
            return Noticia;
        }


        public async Task<Go<Noticia>> Create()
        {
            await Db.Create(Noticia.Object.ReturnSmallNoticia(), Url.Root);
            return this.Noticia;
        }

        public async Task Update()
        {
            await Db.Update(Noticia, 
                Url.AddKey(Url.Root,
                    this.Noticia.Key));
        }

        public async Task Delete(string url)
        {
            await Db.Delete(url);
        }

    }

}

