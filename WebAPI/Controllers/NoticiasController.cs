using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Dominio;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class NoticiasController : ApiController
    {
        // GET: api/Noticia
        public async Task<List<Noticia>> Get()
        {
            return await new NoticiaNegocio().GetAll();
        }

        // GET: api/Noticia/5
        public string Get(string id)
        {
            return "value";
        }

        // POST: api/Noticia
        public async Task Post([FromBody]Noticia noticia)
        {
            await new NoticiaNegocio().Create(noticia);
        }

        // PUT: api/Noticia/5
        public async Task Put([FromBody]Noticia noticia, string id)
        {
            await new NoticiaNegocio().Update(noticia, id);
        }

        // DELETE: api/Noticia/5
        public async Task Delete(string id)
        {
            await new NoticiaNegocio().Delete(id);
        }
    }
}
