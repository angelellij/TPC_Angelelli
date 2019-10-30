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
        // GET: api/Post
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Post/5
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
        public async Task Put(string id, [FromBody]Post post)
        {
            await new PostNegocio().Update(post,id);
        }

        // DELETE: api/Post/5
        public async Task Delete(string id, [FromBody]Post post)
        {
            await new PostNegocio().Delete(post, id);
        }
    }
}
