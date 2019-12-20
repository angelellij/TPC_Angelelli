using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // GET: api/Login/5
        public async Task<Go<Usuario>> Get([FromBody]Usuario Usuario)
        {
            return await new UsuarioNegocio(new Go<Usuario>(Usuario)).Login();
        }
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // POST: api/Login
        public async Task<Go<Usuario>> Post([FromBody]Usuario Usuario)
        {
            return await new UsuarioNegocio(new Go<Usuario>(Usuario)).Login();
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
