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
        public Task<List<Tag>> GetAll()
        {
            return new TagNegocio().GetAll();
        }

        // GET: api/Tags/stringId
        public Task<Tag> Get(string id)
        {
            return new TagNegocio().GetObject(id);
        }

        // POST: api/Tags
        public async void Post([FromBody]Tag value)
        {
              await new TagNegocio().Create(value);
        }

        // PUT: api/Tags/stringId
        public async void Put(string id, [FromBody]Tag tag)
        {
            await new TagNegocio().Update(id, tag);
        }

        // DELETE: api/Tags/stringId
        public async void Delete(string value)
        {
            await new TagNegocio().Delete(value);
        }
    }
}
