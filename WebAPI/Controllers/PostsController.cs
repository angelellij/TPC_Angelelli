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
        public async Task<List<Post>> Get([FromBody]Espacio espacio)
        {
            return await new PostNegocio().GetAllFromEspacio(espacio);
        }
        [Route("~/api/posts/usuarios/")]
        public async Task<List<Post>> Get([FromBody]Usuario usuario)
        {
            return await new PostNegocio().GetAllFromUsuario(usuario);
        }
        [Route("~/api/posts/espacios/{id}")]
        public async Task<Post> Get(string id, [FromBody]Espacio espacio)
        {
            return await new PostNegocio().GetObjectFromEspacio(espacio, id);
        }

        [Route("~/api/posts/usuarios/{id}")]
        public async Task<Post> Get(string id,[FromBody]Usuario usuario)
        {
            return await new PostNegocio().GetObjectFromUsuario(usuario,id);
        }

        // POST: api/Post
        public async Task Post([FromBody]Post value)
        {
            await new PostNegocio().Create(value);
        }

        // PUT: api/Post/5
        public async Task Put([FromBody]Post post)
        {
            await new PostNegocio().Update(post);
        }

        // DELETE: api/Post/5
        public async Task Delete([FromBody]Post post)
        {
            await new PostNegocio().Delete(post);
        }
    }
}
