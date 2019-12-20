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
        private FireUrl Url { get; set; } = new FireUrl("Usuarios");
        private Db Db { get; } = new Db();

        private Go<Usuario> Usuario { get; set; }
        private List<string> UrlEspaciosAdministrador { get; } = new List<string>();
        private List<string> UrlEspaciosMiembro { get; } = new List<string>();
        private List<string> UrlEspacios { get; } = new List<string>();
        public UsuarioNegocio() { }
        public UsuarioNegocio(Go<Usuario> usuario)
        {
            Usuario = new Go<Usuario>(usuario);
            if (Usuario.Object.EspaciosAdministrador != null)
            {
                foreach (var x in Usuario.Object.EspaciosAdministrador)
                {
                    UrlEspaciosAdministrador.Add(Url.AddKey(Url.Espacios,
                                    Url.AddKey(Url.ConvertSavedUrlToFireUrl(x.Value.UrlEspacio),
                                        x.Key)));
                    UrlEspacios.Add(Url.AddKey(Url.Espacios,
                                    Url.AddKey(Url.ConvertSavedUrlToFireUrl(x.Value.UrlEspacio),
                                        x.Key)));
                }
            }
            if (usuario.Object.EspaciosMiembro != null)
            {
                foreach (var x in usuario.Object.EspaciosMiembro)
                {
                    UrlEspaciosMiembro.Add(Url.AddKey(Url.Espacios,
                                    Url.AddKey(Url.ConvertSavedUrlToFireUrl(x.Value.UrlEspacio),
                                        x.Key)));
                    UrlEspacios.Add(Url.AddKey(Url.Espacios,
                                    Url.AddKey(Url.ConvertSavedUrlToFireUrl(x.Value.UrlEspacio),
                                        x.Key)));
                }
            }
        }

        public async Task FirebaseNewUser()
        {
            var AuthProvider = new FirebaseAuthProvider(new FirebaseConfig(Db.ApiKey));
            FirebaseAuthLink auth = await AuthProvider
                .CreateUserWithEmailAndPasswordAsync(
                    Usuario.Object.Email, 
                    Usuario.Object.Contrasena, 
                    Usuario.Object.Nombre + " " + Usuario.Object.Apellido);
                var firebase = new FirebaseClient(
                    Db.DatabaseURL,
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
                    });
            Usuario.Key = auth.User.LocalId;
            AuthProvider.Dispose();
            firebase.Dispose();
        }

        public async Task<Go<Usuario>> Login()
        {
            var AuthProvider = new FirebaseAuthProvider(new FirebaseConfig(Db.ApiKey));
            FirebaseAuthLink auth = await AuthProvider
                .SignInWithEmailAndPasswordAsync(
                    Usuario.Object.Email,
                    Usuario.Object.Contrasena);
            var firebase = new FirebaseClient(
                Db.DatabaseURL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
                });
            Usuario.Key = auth.User.LocalId;
            Usuario = await GetObject();
            AuthProvider.Dispose();
            firebase.Dispose();
            return Usuario;
        }

        public async Task<List<Go<Usuario>>> GetAll()
        {
            List < Go < Usuario >> Usuarios = new List<Go<Usuario>>();
            var data = await Db.Client()
           .Child(Url.Root)
           .OnceAsync<Usuario>();

            foreach (var x in data)
            {
                Usuarios.Add(new Go<Usuario>(x.Key, x.Object));
            }
            return Usuarios;
        }
        public async Task<IDictionary<string, Usuario>> GetAllFromEspacio()
        { 
            IDictionary<string, Usuario> usuarios = new Dictionary<string, Usuario>();
            var data = await Db.Client()
           .Child(Url.AddKey(UrlEspacios[0], Url.Administradores))
           .OnceAsync<Usuario>();

            foreach (var aux in data)
            {
                usuarios.Add(aux.Key,aux.Object);
            }

            data = await Db.Client()
           .Child(Url.AddKey(UrlEspacios[0], Url.Miembros))
           .OnceAsync<Usuario>();

            foreach (var aux in data)
            {
                usuarios.Add(aux.Key, aux.Object);
            }
            return usuarios;
        }

        public async Task<Go<Usuario>> GetObject()
        {
            var data = await Db.Client()
           .Child(Url.Root)
           .OrderByKey()
           .EqualTo(Usuario.Key)
           .OnceAsync<Usuario>();

            foreach(var x in data)
            {
                Usuario.Key = x.Key;
                Usuario.Object = x.Object;
            }
            return Usuario;
        }

        public async Task Create()
        {
            await FirebaseNewUser();
            if (Usuario.Key != null)
            {
                await Db.Update(Usuario.Object, Url.AddKey(Url.Root, Usuario.Key));
                foreach (string url in UrlEspaciosAdministrador)
                {
                    await Db.Update(Usuario.Object, Url.AddKey(url, Usuario.Key));
                }
                foreach (string url in UrlEspaciosMiembro)
                {
                    await Db.Update(Usuario.Object, Url.AddKey(url, Usuario.Key));
                }
            }
        }

        public async Task Update()
        {
            await Db.Update(Usuario.Object, Url.AddKey(Url.Root,Usuario.Key));
            foreach (string url in 
                UrlEspaciosAdministrador)
            {
                await Db.Update(Usuario.Object, Url.AddKey(url,Usuario.Key));
            }
            foreach (string url in UrlEspaciosMiembro)
            {
                await Db.Update(Usuario.Object, Url.AddKey(url, Usuario.Key));
            }
        }

        public async Task Delete()
        {
            foreach (var urlAux in UrlEspacios)
            {
                await Db.Delete(Url.AddKey(urlAux,
                                    Url.AddKey(Url.Administradores,Usuario.Key)));
                await Db.Delete(Url.AddKey(urlAux,
                                    Url.AddKey(Url.Miembros, Usuario.Key)));
            }
            await Db.Delete(Url.AddKey(Url.Root,Usuario.Key));
        }

    }
}
