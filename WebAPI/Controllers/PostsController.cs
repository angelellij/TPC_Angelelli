using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Negocio;

namespace WebAPI.Controllers
{
    public class PostsController : ApiController
    {
        [Route("~/api/posts/espacios/")]
        public async Task<IDictionary<string, Post>> Get([FromBody]Go<Espacio> espacio)
        {
            Go<Post> Post = new Go<Post>();
            Post.Object.Espacio = new Go<Espacio>(espacio);
            return await new PostNegocio(Post).GetAllFromEspacio();
        }
        [Route("~/api/posts/usuarios/")]
        public async Task<List<Post>> Get([FromBody]Go<Usuario> usuario)
        {
            Go<Post> Post = new Go<Post>();
            Post.Object.Usuario = new Go<Usuario>(usuario);
            return await new PostNegocio(Post).GetAllFromUsuario();
        }
        [Route("~/api/posts/espacios/{id}")]
        public async Task<Go<Post>> Get(string id, [FromBody]Espacio espacio)
        {
            Go<Post> Post = new Go<Post>(id);
            Post.Object.Espacio = new Go<Espacio>(espacio);
            return await new PostNegocio(Post).GetObjectFromEspacio();
        }

        [Route("~/api/posts/usuarios/{id}")]
        public async Task<Go<Post>> Get(string id,[FromBody]Usuario usuario)
        {
            Go<Post> Post = new Go<Post>();
            Post.Object.Usuario = new Go<Usuario>(usuario);
            return await new PostNegocio(Post).GetObjectFromUsuario();
        }

        // POST: api/Post
        public async Task Post([FromBody]Post post)
        {
            await new PostNegocio(new Go<Post>(post)).Create();
        }

        // PUT: api/Post/5
        public async Task Put([FromBody]Post post, string id)
        {
            await new PostNegocio(new Go<Post>(id, post)).Update();
        }

        // DELETE: api/Post/5
        public async Task Delete([FromBody]string id)
        {
            await new PostNegocio(new Go<Post>(id)).Delete();
        }
    }
}
