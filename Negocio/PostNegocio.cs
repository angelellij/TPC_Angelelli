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
    public class PostNegocio
    {
        FireUrl Url { get; } = new FireUrl("posts");
        Db Db { get; } = new Db();
        public async Task<List<Post>> GetAllFromEspacio(Espacio Espacio)
        {
            var data = await Db.Client()
                .Child(Url.RootInOneEspacio(Espacio))
                .OnceAsync<Post>();
            List<Post> posts = new List<Post>();
            foreach (FirebaseObject<Post> aux in data)
            {
                posts.Add(new Post(aux));
            }
            return posts;
        }
        public async Task<List<Post>> GetAllFromUsuario(Usuario usuario)
        {
            var data = await Db.Client()
                .Child(Url.AddKey(Url.Usuarios,usuario.Id))
                .OnceAsync<Post>();
            List<Post> posts = new List<Post>();
            foreach (FirebaseObject<Post> aux in data)
            {
                posts.Add(new Post(aux));
            }
            return posts;
        }
        public async Task<Post> GetObjectFromEspacio(Espacio espacio, string key)
        {
            var x = await Db.Client()
                .Child(Url.RootInOneEspacio(espacio))
                .OrderByKey()
                .EqualTo(key)
                .OnceSingleAsync<Post>();

            if (x != null)
            {
                x.Id = key;
            }
            return x;
        }
        public async Task<Post> GetObjectFromUsuario(Usuario usuario, string key){
            var data = await Db.Client()
                .Child(Url.AddKey(Url.Usuarios,usuario.Id))
                .OrderByKey()
                .EqualTo(key)
                .OnceAsync<Post>();
            Post post = new Post();
            if (data != null)
            {
                foreach (var aux in data)
                post = new Post(aux);
            }
            return post;
        }
        public async Task Create(Post post)
        {
            post.Id = null;
            var x = await Db.Create(post.ReturnSmallPost(), 
                Url.RootInOneEspacio(post.Espacio));
            if (x.Key != null)
            { 
                await Db.Update(post.ReturnSmallPost(),
                    Url.AddKey(Url.AddKey(Url.Usuarios, post.Usuario.Id), x.Key));
            }    
        }
        public async Task Update(Post post)
        {
            string Key = post.Id;
            post.Id = null;
            await Db.Update(post.ReturnSmallPost(), 
                Url.AddKey(Url.RootInOneEspacio(post.Espacio),Key));
            await Db.Update(post.ReturnSmallPost(),
                Url.AddKey(Url.AddKey(Url.Usuarios, post.Usuario.Id), Key));
        }
        public async Task Delete(Post post)
        {
            await Db.Delete(
                Url.AddKey(
                    Url.RootInOneEspacio(
                        post.Espacio), post.Id));
            await Db.Delete(Url.AddKey(Url.AddKey(Url.Usuarios, post.Usuario.Id), post.Id));
        }
    }
}
