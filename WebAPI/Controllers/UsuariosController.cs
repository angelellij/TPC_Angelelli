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
    public class UsuariosController : ApiController
    {
        // GET: api/Usuarios
        public async Task<List<Usuario>> Get()
        {
           return await new UsuarioNegocio().GetAll();
        }

        // GET: api/Usuarios/5
        public async Task<Usuario> Get(string id)
        {
            return await new UsuarioNegocio().GetObject(id);
        }

        // POST: api/Usuarios
        public async Task Post([FromBody]Usuario usuario)
        {
            await new UsuarioNegocio().Create(usuario); 

        }

        // PUT: api/Usuarios/5
        public async Task Put(string id, [FromBody]Usuario value)
        {
            await new UsuarioNegocio().Update(id,value);
        }

        // DELETE: api/Usuarios/5
        public async Task Delete(string id)
        {
            await new UsuarioNegocio().Delete(id);
        }
    }
}
