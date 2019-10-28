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
            return new TagNegocio().getAll();
        }

        // GET: api/Tags/stringId
        public Task<Tag> Get(string id)
        {
            return new TagNegocio().getTag(id);
        }

        // POST: api/Tags
        public async void Post([FromBody]Tag value)
        {
                new TagNegocio().create(value);          
        }

        // PUT: api/Tags/stringId
        public void Put(string id, [FromBody]Tag tag)
        {
            new TagNegocio().update(id, tag);
        }

        // DELETE: api/Tags/stringId
        public void Delete(string value)
        {
            new TagNegocio().delete(value);
        }
    }
}
