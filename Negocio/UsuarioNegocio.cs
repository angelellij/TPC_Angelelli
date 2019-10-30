using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Database.Query;

namespace Negocio
{
    public class UsuarioNegocio
    {
        private FireUrl Url { get; set; } = new FireUrl("usuarios");
        private Db Db { get; } = new Db();
        public async Task<string> FirebaseNewUser(Usuario usuario)
        {
            var AuthProvider = new FirebaseAuthProvider(new FirebaseConfig(Db.ApiKey));
            FirebaseAuthLink auth = await AuthProvider
                .CreateUserWithEmailAndPasswordAsync(usuario.Email, usuario.Contrasena, usuario.Nombre + usuario.Apellido);
                var firebase = new FirebaseClient(
                    Db.DatabaseURL,
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
                    });

            return auth.User.LocalId;
        }

        public async Task<List<Usuario>> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            var usuariosx = await Db.Client()
           .Child(Url.Root)
           .OnceAsync<Usuario>();

            foreach (var usuariox in usuariosx)
            {
                usuarios.Add(new Usuario(usuariox));
            }
            return usuarios;
        }

        public async Task<List<Usuario>> GetAllFromEspacio(string urlEspacio)
        {
            List<Usuario> usuarios = new List<Usuario>();
            var usuariosx = await Db.Client()
           .Child(urlEspacio + "/" + Url.Root)
           .OnceAsync<Usuario>();

            foreach (var usuariox in usuariosx)
            {
                usuarios.Add(new Usuario(usuariox));
            }
            return usuarios;
        }

        public async Task<Usuario> GetObject(string id)
        {
            Usuario usuario = new Usuario();
            var usuariosx = await Db.Client()
           .Child(Url.Root)
           .OrderByKey()
           .EqualTo(id)
           .OnceAsync<Usuario>();

            foreach (var usuariox in usuariosx)
            {
                usuario = new Usuario(usuariox);
            }
            return usuario;
        }

        public async Task Create(Usuario usuario)
        {
            string key = await FirebaseNewUser(usuario);
            if (key != null)
            {
                await Db.Update(usuario, Url.GetRootUrlFromKey(key));
                int i = 0;
                if (usuario.ListadoEspacios != null)
                {
                    foreach (Espacio espacio in usuario.ListadoEspacios)
                    {
                        await Db.Update(
                            usuario.ReturnSmallUsuario(),
                            Url.AddKeyToUrl("Espacios",
                                Url.AddKeyToUrl(usuario.ListadoEspacios[i].GetUrlEspacio(),
                                    Url.AddKeyToUrl(Url.GetRootUrlFromKey(usuario.ListadoEspacios[i].Id),
                                        key))));
                        i++;
                    }
                }
            }
        }

        public async Task Update(string id, Usuario usuario)
        {
            usuario.Id = null;
            await Db.Update(usuario, Url.GetRootUrlFromKey(id));
        }

        public async Task Delete(string id)
        {
            await Db.Delete(Url.GetRootUrlFromKey(id));
        }

    }
}
