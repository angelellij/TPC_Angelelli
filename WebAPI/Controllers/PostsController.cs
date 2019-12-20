using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Negocio;

namespace WebAPI.Controllers
{
    public class PostsController : ApiController
    {
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task<List<Go<Post>>> Get(string espacioUrl)
        {
            espacioUrl = espacioUrl.Replace("--", "/-");
            return await new PostNegocio().GetAllFromEspacio(espacioUrl);
        }
        

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // POST: api/Post
        public async Task<Go<Post>> Post([FromBody]Post post)
        {
            return await new PostNegocio().Create(post);
        }
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // PUT: api/Post/5
        public async Task Put([FromBody]Post post, string id)
        {
            await new PostNegocio(new Go<Post>(id, post)).Update();
        }
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // DELETE: api/Post/5
        public async Task Delete([FromBody]string id)
        {
            await new PostNegocio(new Go<Post>(id)).Delete();
        }
    }
}
