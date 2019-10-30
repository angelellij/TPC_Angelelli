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
        FireUrl Url = new FireUrl("posts");
        Db Db = new Db();
        public async Task<List<Post>> GetAllFromEspacio(Espacio Espacio)
        {
            var data = await Db.Client()
                .Child(Url.RootInOneEspacio(Espacio.UrlEspacio ,Espacio.Id))
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
                .Child(Url.RootInOneUsuario(usuario.Id))
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
                .Child(Url.RootInOneEspacio(espacio.UrlEspacio,espacio.Id))
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
            var x = await Db.Client()
                .Child(Url.RootInOneUsuario(usuario.Id))
                .OrderByKey()
                .EqualTo(key)
                .OnceSingleAsync<Post>();
           
            if (x != null)
            {
                x.Id = key;    
            }
            return x;
        }
        public async Task Create(Post post)
        {
            post.Id = null;
            var x = await Db.Create(post.ReturnSmallPost(), 
                Url.RootInOneEspacio(post.Espacio.UrlEspacio,post.Espacio.Id));
            if (x.Key != null)
            { 
                await Db.Update(post.ReturnSmallPost(),
                    Url.AddKeyToUrl(Url.RootInOneUsuario(post.Usuario.Id), x.Key));
            }    
        }
        public async Task Update(Post post, string Key)
        {
            post.Id = null;
            await Db.Update(post.ReturnSmallPost(), 
                Url.AddKeyToUrl(Url.RootInOneEspacio(post.Espacio.UrlEspacio, post.Espacio.Id),Key));
            await Db.Update(post.ReturnSmallPost(),
                Url.AddKeyToUrl(Url.RootInOneUsuario(post.Usuario.Id), Key));
        }
        public async Task Delete(Post post, string Key)
        {
            await Db.Delete(Url.AddKeyToUrl(Url.RootInOneEspacio(post.Espacio.UrlEspacio, post.Espacio.Id), Key));
            await Db.Delete(Url.AddKeyToUrl(Url.RootInOneUsuario(post.Usuario.Id), Key));
        }
    }
}
