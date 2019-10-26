using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Dominio;

namespace WebAPI.Controllers
{
    public class NoticiaController : ApiController
    {
        // GET: api/Noticia
        public IEnumerable<Noticia> Get()
        {
            return (IEnumerable<Noticia>)new NoticiaNegocio().getAll();
        }

        // GET: api/Noticia/5
        /*public string Get(int id)
        {
            return "value";
        }
*/
        // POST: api/Noticia
        public void Post([FromBody]Noticia noticia)
        {
            new NoticiaNegocio().create(noticia);
        }

        // PUT: api/Noticia/5
        public void Put([FromBody]Noticia noticia, [FromBody]Noticia noticia2)
        {
            new NoticiaNegocio().update(noticia, noticia2);
        }

        // DELETE: api/Noticia/5
        public void Delete(int id)
        {
        }
    }
}
