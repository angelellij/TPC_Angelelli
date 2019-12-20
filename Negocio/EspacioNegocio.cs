using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Firebase.Database;
using Firebase.Database.Query;

namespace Negocio
{
    public class EspacioNegocio
    {
        private FireUrl Url { get; } = new FireUrl("Espacios");
        private Db Db { get; } = new Db();
        private Go<Espacio> Espacio;
        private string UrlEspacios { get; set; } = "";
        private List<string> UrlUsuarios { get; } = new List<string>();
        public EspacioNegocio() { }
        public EspacioNegocio(Go<Espacio> espacio)
        {
            Espacio = new Go<Espacio>(espacio);

            if (Espacio.Object.UrlEspacio != null & Espacio.Object.UrlEspacio != "") {
                UrlEspacios = Url.AddKey(Url.Root,
                                Url.ConvertSavedUrlToFireUrl(Espacio.Object.UrlEspacio));
            }
            else { UrlEspacios = Url.Root; }
            if (Espacio.Object.Administradores != null)
            {
                foreach (var x in Espacio.Object.Administradores)
                {
                    UrlUsuarios.Add(Url.AddKey(Url.Usuarios,
                                        Url.AddKey((string)x.Key, Url.Administradores)));
                }
            }
            if (Espacio.Object.Miembros != null)
            {
                foreach (var x in espacio.Object.Miembros)
                    UrlUsuarios.Add(Url.AddKey(Url.Usuarios,
                                        Url.AddKey(x.Key, Url.Miembros)));
            }
        }

        public async Task<IDictionary<string, Espacio>> GetAll()
        {
            IDictionary<string, Espacio> espacios = new Dictionary<string, Espacio>();
            List<string> urlEspacios = new List<string>
            {
                Url.Root
            };

            while (urlEspacios.Count > 0)
            {
                var espaciosx = await Db.Client()
                .Child(urlEspacios[0])
                .OnceAsync<Espacio>();

                if (espaciosx != null)
                {
                    foreach (var espaciox in espaciosx)
                    {
                        urlEspacios.Add(Url.AddKey(urlEspacios[0], espaciox.Key));
                        espacios.Add(espaciox.Key, espaciox.Object);
                    }
                }
                urlEspacios.Remove(urlEspacios[0]);
            }
            return espacios;
        }

        public async Task<Go<Espacio>> GetObject()
        {
            var espaciosx = await Db.Client()
              .Child(UrlEspacios)
              .OrderByKey()
              .EqualTo(Espacio.Key)
              .OnceAsync<Espacio>();

            this.Espacio.Key = null;
            foreach (var espaciox in espaciosx)
            {
                this.Espacio = new Go<Espacio>(espaciox);
            }
            return this.Espacio;
        }

        public async Task Create()
        {
            var x = await Db.Create(Espacio.Object, UrlEspacios);
            if (x != null)
            {
                Espacio.Key = x.Key;
                foreach (var urlaux in UrlUsuarios)
                {
                    await Db.Update(Espacio.Object.ReturnSmallEspacio(), Url.AddKey(urlaux, Espacio.Key));
                }
            }
        }

        public async Task Update()
        {
            await Db.Update(Espacio.Object, Url.AddKey(UrlEspacios, Espacio.Key));
            foreach (string urlaux in UrlUsuarios)
            {
                await Db.Update(Espacio.Object, Url.AddKey(urlaux, Espacio.Key));
            }
        }

        public async Task Delete()
        {
            await Db.Delete(Url.AddKey(UrlEspacios, Espacio.Key));
            foreach (string urlaux in UrlUsuarios)
            {
                await Db.Delete(Url.AddKey(urlaux, Espacio.Key));
            }
        }

        public async Task<List<Go<Espacio>>> GetAllFromUsuario(string idUsuario)
        {
            List<Go<Espacio>> espacios = new List<Go<Espacio>>();
      
            var espaciosx = await Db.Client()
            .Child(Url.AddKey(
                Url.Usuarios,
                    Url.AddKey(idUsuario,
                        Url.Administradores)))
            .OnceAsync<Espacio>();

            if (espaciosx != null)
            {
                foreach (var espaciox in espaciosx)
                {
                    this.UrlEspacios = Url.AddKey(Url.Root, espaciox.Object.UrlEspacio);
                    this.Espacio = new Go<Espacio>(espaciox.Key, espaciox.Object);
                    espacios.Add(await GetObject());
                }
            }
            return espacios;
        }

    }
}
