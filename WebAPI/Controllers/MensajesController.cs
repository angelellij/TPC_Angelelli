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
    public class MensajesController : ApiController
    {
        // GET: api/Mensajes
        public async Task<IDictionary<string, Mensaje>> Get()
        {
            return await new MensajeNegocio().GetAllFrom(1);
        }

        // GET: api/Mensajes/5
        public async Task<Go<Mensaje>> Get(string id)
        {
            return await new MensajeNegocio(new Go<Mensaje>(id)).GetObjectFrom(1);
        }

        // POST: api/Mensajes
        public async Task Post([FromBody]Mensaje Mensaje)
        {
            await new MensajeNegocio(new Go<Mensaje>(Mensaje)).Create();
        }

        // PUT: api/Mensajes/5
        public async Task Put(string id, [FromBody]Mensaje Mensaje)
        {
            await new MensajeNegocio(new Go<Mensaje>(id, Mensaje)).Update();
        }

        // DELETE: api/Mensajes/5
        public async Task Delete(string id)
        {
            await new MensajeNegocio(new Go<Mensaje>(id)).Delete();
        }
    }
}
