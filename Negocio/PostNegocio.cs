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

        public PostNegocio()
        {
        }

        public PostNegocio(Go<Post> post)
        {
            Post = new Go<Post>(post);
            UrlEspacios = Url.RootInOneEspacio(Post.Object.Espacio);
            UrlUsuarios = Url.AddKey(Url.Usuarios, Post.Object.Usuario.Key);
        }
        public async Task<List<Go<Post>>> GetAllFromEspacio(string urlEspacio)
        {
            string url = Url.AddKey(Url.Espacios,
                            Url.AddKey(urlEspacio,
                                Url.Root));
            var data = await Db.Client()
                .Child(url)
                .OnceAsync<Post>();

            List<Go<Post>> posts = new List<Go<Post>>();
            foreach (FirebaseObject<Post> aux in data)
            {
                posts.Add(new Go<Post>(aux));
            }
            return posts;
        }
        public async Task<List<Go<Post>>> GetAllFromUsuario()
        {
            var data = await Db.Client()
                .Child(UrlUsuarios)
                .OnceAsync<Post>();
            List<Go<Post>> posts = new List<Go<Post>>();
            foreach (FirebaseObject<Post> aux in data)
            {
                posts.Add(new Go<Post>(aux));
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
        public async Task<Go<Post>> Create(Post post)
        {
            string url = Url.AddKey(Url.Espacios,
                            Url.AddKey(post.Espacio.Object.UrlEspacio,
                                Url.AddKey(post.Espacio.Key,
                                    Url.Root)));
            await Db.Create(post.ReturnSmallPost(),url);
            return new Go<Post>(post);
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
