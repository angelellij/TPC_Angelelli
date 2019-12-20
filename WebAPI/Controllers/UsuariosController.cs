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
    public class UsuariosController : ApiController
    {
        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*",SupportsCredentials = true)]
        // GET: api/Usuarios
        public async Task<List<Go< Usuario>>> Get()
        {
           return await new UsuarioNegocio().GetAll();
        }

        // GET: api/Usuarios/5
        public async Task<Go<Usuario>> Get(string id)
        {
            return await new UsuarioNegocio(new Go<Usuario>(id)).GetObject();
        }

        [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*", SupportsCredentials = true)]
        // POST: api/Usuarios
        public async Task Post([FromBody]Usuario usuario)
        {
            await new UsuarioNegocio(new Go<Usuario>(usuario)).Create(); 
        }

        // PUT: api/Usuarios/5
        public async Task Put([FromBody]Usuario usuario, string id)
        {
            await new UsuarioNegocio(new Go<Usuario>(id, usuario)).Update();
        }

        // DELETE: api/Usuarios/5
        public async Task Delete(string id)
        {
            await new UsuarioNegocio(new Go<Usuario>(id)).Delete();
        }
    }
}
