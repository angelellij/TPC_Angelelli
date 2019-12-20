using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Dominio;
using Negocio;

namespace WebAPI.Controllers
{
    public class MensajesController : ApiController
    {
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // GET: api/Mensajes
        public async Task<List<Go<Mensaje>>> Get(string usuarios)
        {
            string[] usuariosx = usuarios.Split('-');
            Mensaje mensajex = new Mensaje();
            mensajex.Emisor = usuariosx[0];
            mensajex.Receptor = usuariosx[1];
            return await new MensajeNegocio(new Go<Mensaje>(mensajex)).GetAllFrom(1);
        }

        /*[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // GET: api/Mensajes/5
        public async Task<Go<Mensaje>> Get(string id)
        {
            return await new MensajeNegocio(new Go<Mensaje>(id)).GetObjectFrom(1);
        }*/

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // POST: api/Mensajes
        public async Task Post([FromBody]Mensaje mensaje)
        {
            await new MensajeNegocio(new Go<Mensaje>(mensaje)).Create();
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // PUT: api/Mensajes/5
        public async Task Put(string id, [FromBody]Mensaje Mensaje)
        {
            await new MensajeNegocio(new Go<Mensaje>(id, Mensaje)).Update();
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // DELETE: api/Mensajes/5
        public async Task Delete(string id)
        {
            await new MensajeNegocio(new Go<Mensaje>(id)).Delete();
        }
    }
}
