using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Firebase.Database;
using Firebase.Auth;

namespace Negocio
{
    public class UsuarioNegocio
    {
        private string Root { get; } = "usuarios";
        private Db Db { get; } = new Db();
        public async Task<Usuario> Create(Usuario usuario) {

            var authProvider = Db.FirebaseNewUser();
            // var facebookAccessToken = "<login with facebook and get oauth access token>";

            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(usuario.Email, usuario.Contrasena);
           
            var firebase = new FirebaseClient(
              Db.DatabaseURL,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
              });

            var usuarios = await firebase
            .Child("usuarios")
            .OnceAsync<Usuario>();

            foreach (var usuariox in usuarios)
            {
                //Console.WriteLine($"{usuariox.Key} is {usuariox.Object.Id}m high.");
            }

            return usuario;
        }

    }
}
