using Dominio;
using Firebase.Database;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    public class TagsController : ApiController
    {
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // GET: api/Tags
        public Task<List<Go<Tag>>> GetAll(string espacioUrl)
        {
            espacioUrl = espacioUrl.Replace("--", "/-");
            return new TagNegocio().GetAllFromEspacios(espacioUrl);
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // GET: api/Tags/stringId
        public Task<Go<Tag>> Get([FromBody]Espacio espacio, string id)
        {
            Go<Tag> tag = new Go<Tag>(id);
            tag.Object.Espacio = new Go<Espacio>(espacio);
            return new TagNegocio(tag).GetObject();
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // POST: api/Tags
        public async Task<Go<Tag>> Post([FromBody]Tag tag)
        {
           return await new TagNegocio(new Go<Tag>(tag)).Create();
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // PUT: api/Tags/stringId
        public async Task<Go<Tag>> Put([FromBody]Go<Tag> tag)
        {
           return await new TagNegocio(tag).Update();
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // DELETE: api/Tags/stringId
        public async Task<Go<Tag>> Delete(string url)
        {
            return await new TagNegocio().Delete(url);
        }
    }
}
