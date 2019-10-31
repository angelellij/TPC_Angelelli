using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Firebase.Database;
using Firebase.Database.Query;

namespace Negocio
{
    class ComentarioNegocio
    {
        private FireUrl Url { get; } = new FireUrl("comentarios");
        private Db Db { get; } = new Db();
        private string urlEspacios;
        private string urlUsuarios;
        public ComentarioNegocio(Comentario comentario)
        {
            urlUsuarios = Url.AddKey(Url.Usuarios, comentario.Usuario.Id);
            urlEspacios = Url.RootInOneEspacio(comentario.Post.Espacio);
        }
        
        public async Task<List<Comentario>> GetAllFrom(Comentario comentario, int opcion)
        {
            string url = "";
            if (opcion == 1) { url = urlEspacios; }
            if (opcion == 2) { url = urlUsuarios; }
            var data = await Db.Client()
                .Child(url)
                .OnceAsync<Comentario>();
            List<Comentario> comentarios = new List<Comentario>();
            foreach (FirebaseObject<Comentario> aux in data)
            {
                comentarios.Add(new Comentario(aux));
            }
            return comentarios;
        }

        public async Task<Comentario> GetObjectFrom(Comentario comentario, int opcion)
        {
            string url = "";
            if (opcion == 1) { url = urlEspacios; }
            if (opcion == 2) { url = urlUsuarios; }
            var x = await Db.Client()
                .Child(url)
                .OrderByKey()
                .EqualTo(comentario.Id)
                .OnceSingleAsync<Comentario>();

            if (x != null)
            {
                x.Id = comentario.Id;
            }
            return x;
        }
       
        public async Task Create(Comentario comentario)
        {
            comentario.Id = null;
            var x = await Db.Create(comentario.ReturnSmallComentario(),urlEspacios); ;
            if (x.Key != null)
            {
                await Db.Update(comentario.ReturnSmallComentario(),Url.AddKey(urlUsuarios, x.Key));
            }
        }
        public async Task Update(Comentario comentario)
        {
            string Key = comentario.Id;
            comentario.Id = null;
            await Db.Update(comentario.ReturnSmallComentario(),
                Url.AddKey(urlEspacios, Key));
            await Db.Update(comentario.ReturnSmallComentario(),
                Url.AddKey(urlUsuarios, Key));
        }
        public async Task Delete(Comentario comentario)
        {
            await Db.Delete(Url.AddKey(urlEspacios, comentario.Id));
            await Db.Delete(Url.AddKey(urlUsuarios, comentario.Id));
        }
    }
}
