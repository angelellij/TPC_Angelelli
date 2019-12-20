using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Dominio;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    public class NoticiasController : ApiController
    {
        // GET: api/Noticia
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task<List<Go<Noticia>>> Get()
        {
            return await new NoticiaNegocio().GetAll();
        }

        // GET: api/Noticia/5
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task<Go<Noticia>> Get(string id)
        {
            return await new NoticiaNegocio(new Go<Noticia>(id)).GetObject();
        }

        // POST: api/Noticia
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task<Go<Noticia>> Post([FromBody]Noticia noticia)
        {
            return await new NoticiaNegocio(new Go<Noticia>(noticia)).Create();
        }

        // PUT: api/Noticia/5
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task Put([FromBody]Noticia noticia, string id)
        {
            await new NoticiaNegocio(new Go<Noticia>(id,noticia)).Update();
        }

        // DELETE: api/Noticia/5
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        public async Task Delete(string id)
        {
            await new NoticiaNegocio(new Go<Noticia>(id)).Delete(id);
        }
    }
}
