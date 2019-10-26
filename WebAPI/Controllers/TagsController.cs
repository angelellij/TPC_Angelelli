using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class TagsController : ApiController
    {
        // GET: api/Tags
        public IEnumerable<Tag> GetAll()
        {
            return (IEnumerable<Tag>)new TagNegocio().getAll();
        }

        // GET: api/Tags/5
        public int Get(int id)
        {
            return id;
        }

        // POST: api/Tags
        public void Post([FromBody]Object value)
        {
            new TagNegocio().create((Tag)value);
        }

        // PUT: api/Tags/5
        public void Put([FromBody]Object value, [FromBody]Object value2)
        {
            new TagNegocio().update((Tag)value, (Tag)value2);
        }

        // DELETE: api/Tags/5
        public void Delete([FromBody]Object value)
        {
            new TagNegocio().delete((Tag)value);
        }
    }
}
