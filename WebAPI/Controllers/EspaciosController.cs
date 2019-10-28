using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Dominio;
using Negocio;

namespace WebAPI.Controllers
{
    public class EspaciosController : ApiController
    {
        // GET: api/Tags
        public Task<List<Espacio>> GetAll()
        {
            return new EspacioNegocio().getAll();
        }

        // GET: api/Tags/stringId
        public Task<Espacio> Get(string id)
        {
            return new EspacioNegocio().getObject(id);
        }

        // POST: api/Tags
        public async void Post([FromBody]Espacio value)
        {
            new EspacioNegocio().create(value);
        }

        // PUT: api/Tags/stringId
        public void Put(string id, [FromBody]Espacio espacio)
        {
            new EspacioNegocio().update(id, espacio);
        }

        // DELETE: api/Tags/stringId
        public void Delete(string value)
        {
            new EspacioNegocio().delete(value);
        }
    }
}
