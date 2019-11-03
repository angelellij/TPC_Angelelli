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

namespace WebAPI.Controllers
{
    public class TagsController : ApiController
    {
        // GET: api/Tags
        public Task<IDictionary<string, Tag>> GetAll([FromBody]Espacio espacio)
        { 
            Go<Tag> tag = new Go<Tag>();
            tag.Object.Espacio = new Go<Espacio>(espacio);
            return new TagNegocio(tag).GetAllFromEspacios();
        }

        // GET: api/Tags/stringId
        public Task<Go<Tag>> Get([FromBody]Espacio espacio, string id)
        {
            Go<Tag> tag = new Go<Tag>(id);
            tag.Object.Espacio = new Go<Espacio>(espacio);
            return new TagNegocio(tag).GetObject();
        }

        // POST: api/Tags
        public async void Post([FromBody]Tag tag)
        {
            await new TagNegocio(new Go<Tag>(tag)).Create();
        }

        // PUT: api/Tags/stringId
        public async void Put([FromBody]Tag tag, string id)
        {
            await new TagNegocio(new Go<Tag>(id,tag)).Update();
        }

        // DELETE: api/Tags/stringId
        public async void Delete([FromBody]string id)
        {
            await new TagNegocio(new Go<Tag>(id)).Delete();
        }
    }
}
