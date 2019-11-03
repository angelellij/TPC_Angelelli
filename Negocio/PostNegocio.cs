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
        private FireUrl Url { get; } = new FireUrl("Posts");
        private Db Db { get; } = new Db();
        private string UrlEspacios { get; } = "";
        private string UrlUsuarios { get; } = "";
        private Go<Post> Post { get; }

        public PostNegocio(Go<Post> post)
        {
            Post = new Go<Post>(post);
            UrlEspacios = Url.RootInOneEspacio(Post.Object.Espacio);
            UrlUsuarios = Url.AddKey(Url.Usuarios, Post.Object.Usuario.Key);
        }
        public async Task<IDictionary<string, Post>> GetAllFromEspacio()
        {
            var data = await Db.Client()
                .Child(UrlEspacios)
                .OnceAsync<Post>();
            IDictionary<string, Post> posts = new Dictionary<string, Post>();
            foreach (FirebaseObject<Post> aux in data)
            {
                posts.Add(aux.Key, aux.Object);
            }
            return posts;
        }
        public async Task<List<Post>> GetAllFromUsuario()
        {
            var data = await Db.Client()
                .Child(UrlUsuarios)
                .OnceAsync<Post>();
            List<Post> posts = new List<Post>();
            foreach (FirebaseObject<Post> aux in data)
            {
                posts.Add(new Post(aux));
            }
            return posts;
        }
        public async Task<Go<Post>> GetObjectFromEspacio()
        {
            var x = await Db.Client()
                .Child(UrlEspacios)
                .OrderByKey()
                .EqualTo(Post.Key)
                .OnceSingleAsync<Post>();

            if (x == null) { Post.Key = null; }
            else { Post.Object = x; }
            return Post;
        }
        public async Task<Go<Post>> GetObjectFromUsuario(){
            var data = await Db.Client()
                .Child(UrlUsuarios)
                .OrderByKey()
                .EqualTo(Post.Key)
                .OnceSingleAsync<Post>();

            if (data == null) { Post.Key = null; } 
            else { Post.Object = data;  }

            return Post;
        }
        public async Task Create()
        {
            var x = await Db.Create(Post.Object.ReturnSmallPost(), 
               UrlEspacios);
            if (x.Key != null)
            { 
                await Db.Update(Post.Object.ReturnSmallPost(),
                    Url.AddKey(UrlUsuarios, Post.Key));
            }    
        }
        public async Task Update()
        {
            await Db.Update(Post.Object.ReturnSmallPost(), 
                Url.AddKey(UrlEspacios,Post.Key));
            await Db.Update(Post.Object.ReturnSmallPost(),
                Url.AddKey(UrlUsuarios, Post.Key));
        }
        public async Task Delete()
        {
            await Db.Delete(Url.AddKey(UrlEspacios, Post.Key));
            await Db.Delete(Url.AddKey(UrlUsuarios, Post.Key));
        }
    }
}
