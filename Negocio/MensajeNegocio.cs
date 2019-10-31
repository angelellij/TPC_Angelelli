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
        private FireUrl Url { get; } = new FireUrl("mensajes");
        private Db Db { get; } = new Db();

        private string urlEmisor;
        private string urlReceptor;

        public MensajeNegocio(Mensaje Mensaje)
        {
            urlEmisor = Url.AddKey(Url.Usuarios,
                            Url.AddKey(Mensaje.Emisor.Id,
                                Url.AddKey(Url.Root,
                                    Mensaje.Receptor.Id)));
            urlReceptor = Url.AddKey(Url.Usuarios,
                            Url.AddKey(Mensaje.Receptor.Id,
                                Url.AddKey(Url.Root,
                                    Mensaje.Emisor.Id)));
        }

        public async Task<List<Mensaje>> GetAllFrom(Mensaje Mensaje, int opcion)
        {
            string url = "";
            if (opcion == 1) { url = urlEmisor; }
            if (opcion == 2) { url = urlReceptor; }
            var data = await Db.Client()
                .Child(url)
                .OnceAsync<Mensaje>();
            List<Mensaje> Mensajes = new List<Mensaje>();
            foreach (FirebaseObject<Mensaje> aux in data)
            {
                Mensaje MensajeAux = new Mensaje(aux);
                MensajeAux.Emisor = new Usuario();
                MensajeAux.Emisor = Mensaje.Emisor;
                MensajeAux.Receptor = new Usuario();
                MensajeAux.Receptor = Mensaje.Receptor;
                Mensajes.Add(MensajeAux);
            }
            return Mensajes;
        }

        public async Task<Mensaje> GetObjectFrom(Mensaje Mensaje, int opcion)
        {
            string url = "";
            if (opcion == 1) { url = urlEmisor; }
            if (opcion == 2) { url = urlReceptor; }
            var x = await Db.Client()
                .Child(url)
                .OrderByKey()
                .EqualTo(Mensaje.Id)
                .OnceSingleAsync<Mensaje>();

            if (x != null)
            {
                x.Id = Mensaje.Id;
                x.Emisor = new Usuario();
                x.Emisor = Mensaje.Emisor;
                x.Receptor = new Usuario();
                x.Receptor = Mensaje.Receptor;
            }
            return x;
        }

        public async Task Create(Mensaje Mensaje)
        {
            Mensaje.Id = null;
            var x = await Db.Create(Mensaje.ReturnSmallMensaje(), urlReceptor); ;
            if (x.Key != null)
            {
                await Db.Update(Mensaje.ReturnSmallMensaje(), Url.AddKey(urlEmisor, x.Key));
            }
        }

        public async Task Update(Mensaje Mensaje)
        {
            string Key = Mensaje.Id;
            Mensaje.Id = null;
            await Db.Update(Mensaje.ReturnSmallMensaje(),
                Url.AddKey(urlEmisor, Key));
            await Db.Update(Mensaje.ReturnSmallMensaje(),
                Url.AddKey(urlReceptor, Key));
        }

        public async Task Delete(Mensaje Mensaje)
        {
            await Db.Delete(Url.AddKey(urlEmisor, Mensaje.Id));
            await Db.Delete(Url.AddKey(urlReceptor, Mensaje.Id));
        }
    }
}
