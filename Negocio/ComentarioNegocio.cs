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
        private FireUrl Url { get; } = new FireUrl("Comentarios");
        private Db Db { get; } = new Db();

        private Go<Comentario> Comentario { get; set; }
        private string UrlEspacios { get; }
        private string UrlUsuarios { get; }
        public ComentarioNegocio(Go<Comentario> comentario)
        {
            Comentario = new Go<Comentario>(comentario);
            UrlUsuarios = Url.AddKey(Url.Usuarios, Comentario.Object.Usuario.Key);
            UrlEspacios = Url.RootInOneEspacio(Comentario.Object.Post.Object.Espacio);
        }
        
        public async Task<IDictionary<string, Comentario>> GetAllFrom(int opcion)
        {
            string url = "";
            if (opcion == 1) { url = UrlEspacios; }
            if (opcion == 2) { url = UrlUsuarios; }
            var data = await Db.Client()
                .Child(url)
                .OnceAsync<Comentario>();
            IDictionary<string, Comentario> comentarios = new Dictionary<string, Comentario>();
            foreach (FirebaseObject<Comentario> aux in data)
            {
                comentarios.Add(aux.Key, aux.Object);
            }
            return comentarios;
        }
        public async Task<Go<Comentario>> GetObjectFrom(int opcion)
        {
            string url = "";
            if (opcion == 1) { url = UrlEspacios; }
            if (opcion == 2) { url = UrlUsuarios; }

            var x = await Db.Client()
                .Child(url)
                .OrderByKey()
                .EqualTo(Comentario.Key)
                .OnceSingleAsync<Comentario>();

            if (x == null)
            {
                Comentario.Key = null;
            }
            return Comentario;
        }
        public async Task Create()
        {
            Comentario.Key = null;
            var x = await Db.Create(Comentario.Object.ReturnSmallComentario(),UrlEspacios); ;
            if (x.Key != null)
            {
                await Db.Update(Comentario.Object.ReturnSmallComentario(),Url.AddKey(UrlUsuarios, Comentario.Key));
            }
        }
        public async Task Update()
        {
            await Db.Update(Comentario.Object.ReturnSmallComentario(),
                Url.AddKey(UrlEspacios, Comentario.Key));
            await Db.Update(Comentario.Object.ReturnSmallComentario(),
                Url.AddKey(UrlUsuarios, Comentario.Key));
        }
        public async Task Delete()
        {
            await Db.Delete(Url.AddKey(UrlEspacios, Comentario.Key));
            await Db.Delete(Url.AddKey(UrlUsuarios, Comentario.Key));
        }
    }
}
