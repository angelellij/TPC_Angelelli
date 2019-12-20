using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections;
using Dominio;
using Negocio;

namespace WebAPI.Controllers
{
    public class EspaciosController : ApiController
    {
        // GET: api/Tags
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task<List<Go<Espacio>>> GetAll([FromBody]string idUsuario)
        {
            return await new EspacioNegocio().GetAllFromUsuario(idUsuario);
        }

        // GET: api/Tags/stringId
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task<List<Go<Espacio>>> Get(string id)
        {
            if (id.Contains("usuario"))
            {
                id = id.Replace("usuario", "");
                return await new EspacioNegocio().GetAllFromUsuario(id);
            }
            else
            {
                List<Go<Espacio>> espacioD = new List<Go<Espacio>>();
                espacioD.Add(await new EspacioNegocio(new Go<Espacio>(id)).GetObject());
                return espacioD;
            }  
        }

        // POST: api/Tags
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task Post([FromBody]Espacio Espacio)
        {
            await new EspacioNegocio(new Go<Espacio>(Espacio)).Create();
        }

        // PUT: api/Tags/stringId
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task Put([FromBody]Go<Espacio> Espacio)
        {
            await new EspacioNegocio(Espacio).Update();
        }

        // DELETE: api/Tags/stringId
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task Delete(string id)
        {
            await new EspacioNegocio(new Go<Espacio>(id)).Delete();
        }
    }
}
