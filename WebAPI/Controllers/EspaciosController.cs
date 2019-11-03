using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections;
using Dominio;
using Negocio;

namespace WebAPI.Controllers
{
    public class EspaciosController : ApiController
    {
        // GET: api/Tags
        public async Task<IDictionary<string, Espacio>> GetAll()
        {
            return await new EspacioNegocio().GetAll();
        }

        // GET: api/Tags/stringId
        public async Task<Go<Espacio>> Get(string id)
        {
            return await new EspacioNegocio(new Go<Espacio>(id)).GetObject();
        }

        // POST: api/Tags
        public async Task Post([FromBody]Espacio Espacio)
        {
            await new EspacioNegocio(new Go<Espacio>(Espacio)).Create();
        }

        // PUT: api/Tags/stringId
        public async void Put(string id, [FromBody]Espacio Espacio)
        {
            await new EspacioNegocio(new Go<Espacio>(id,Espacio)).Update();
        }

        // DELETE: api/Tags/stringId
        public async void Delete(string id)
        {
            await new EspacioNegocio(new Go<Espacio>(id)).Delete();
        }
    }
}
