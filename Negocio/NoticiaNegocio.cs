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
        private String root = "noticias";
        private Db db = new Db();
        public async Task<List<Noticia>> getAll()
        {
            List<Noticia> noticias = new List<Noticia>();

            var noticiasAux = await db.Client()
              .Child(root)
              .OnceAsync<Noticia>();

            foreach (var noticia in noticiasAux)
            {
                noticias.Add((Noticia)noticia.Object);
            }

            return noticias;
        }

        public async void create(Noticia noticia)
        {
            var result = await db.Client()
          .Child(root)
          .PostAsync(noticia);
        }

        public async void update(Noticia noticiaAGuardar, Noticia noticiaACambiar)
        {
            await db.Client()
            .Child(root)
            .Child(noticiaAGuardar.Id)
            .PutAsync(noticiaACambiar);
        }

        public async void delete(Noticia noticia)
        {
            await db.Client()
              .Child(root)
              .Child(noticia.Id)
              .DeleteAsync();
        }

    }

}

