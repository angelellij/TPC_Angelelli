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
        public async Task<List<Espacio>> GetAll()
        {
            return await new EspacioNegocio().GetAll();
        }

        // GET: api/Tags/stringId
        public Task<Espacio> Get(string id)
        {
            return new EspacioNegocio().GetObject(id);
        }

        // POST: api/Tags
        public async Task Post([FromBody]Espacio value)
        {
            await new EspacioNegocio().Create(value);
        }

        // PUT: api/Tags/stringId
        public async void Put(string id, [FromBody]Espacio espacio)
        {
            await new EspacioNegocio().Update(id, espacio);
        }

        // DELETE: api/Tags/stringId
        public async void Delete(string value)
        {
            await new EspacioNegocio().Delete(value);
        }
    }
}
