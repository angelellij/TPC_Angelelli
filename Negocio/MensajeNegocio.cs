using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Firebase.Database;
using Firebase.Database.Query;

namespace Negocio
{
    public class MensajeNegocio
    {
        private FireUrl Url { get; } = new FireUrl("Mensajes");
        private Db Db { get; } = new Db();
        private Go<Mensaje> Mensaje { get; } = new Go<Mensaje>();
        private string UrlEmisor { get; } = "";
        private string UrlReceptor { get; } = "";

        public MensajeNegocio() { }
        public MensajeNegocio(Go<Mensaje> mensaje)
        {
            Mensaje = new Go<Mensaje>(mensaje);
            UrlEmisor = Url.AddKey(Url.Usuarios,
                            Url.AddKey(Mensaje.Object.Emisor,
                                Url.AddKey(Url.Root,
                                    Mensaje.Object.Receptor)));
            UrlReceptor = Url.AddKey(Url.Usuarios,
                            Url.AddKey(Mensaje.Object.Receptor,
                                Url.AddKey(Url.Root,
                                    Mensaje.Object.Emisor)));
        }

        public async Task<List<Go<Mensaje>>> GetAllFrom(int opcion)
        {
            string url = "";
            if (opcion == 1) { url = UrlEmisor; }
            if (opcion == 2) { url = UrlReceptor; }
            var data = await Db.Client()
                .Child(url)
                .OnceAsync<Mensaje>();

            List<Go<Mensaje>> Mensajes = new List<Go<Mensaje>>();

            foreach (var aux in data)
            {
                Mensajes.Add(new Go<Mensaje>(aux));
            }
            return Mensajes;
        }

        public async Task<Go<Mensaje>> GetObjectFrom(int opcion)
        {
            string url = "";
            if (opcion == 1) { url = UrlEmisor; }
            if (opcion == 2) { url = UrlReceptor; }
            var x = await Db.Client()
                .Child(url)
                .OrderByKey()
                .EqualTo(Mensaje.Key)
                .OnceSingleAsync<Mensaje>();

            if (x == null) { Mensaje.Key = null; }
            else { Mensaje.Object = x; }
            return Mensaje;
        }

        public async Task Create()
        {
            var x = await Db.Create(Mensaje.Object.ReturnSmallMensaje(), UrlReceptor); ;
            if (x.Key != null)
            {
                await Db.Update(Mensaje.Object.ReturnSmallMensaje(), Url.AddKey(UrlEmisor, x.Key));
            }
        }

        public async Task Update()
        {
            await Db.Update(Mensaje.Object.ReturnSmallMensaje(),
                Url.AddKey(UrlEmisor, Mensaje.Key));
            await Db.Update(Mensaje.Object.ReturnSmallMensaje(),
                Url.AddKey(UrlReceptor, Mensaje.Key));
        }

        public async Task Delete()
        {
            await Db.Delete(Url.AddKey(UrlEmisor, Mensaje.Key));
            await Db.Delete(Url.AddKey(UrlReceptor, Mensaje.Key));
        }
    }
}
