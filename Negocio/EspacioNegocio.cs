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
        private string urlEspacios { get; } = "";
        private List<string> urlUsuarios { get; } = new List <string>();
        public EspacioNegocio() { }
        public EspacioNegocio(Go<Espacio> espacio)
        {
            this.Espacio = new Go<Espacio>(espacio);

            if (Espacio.Object.UrlEspacio != null & Espacio.Object.UrlEspacio != ""){
                urlEspacios = Url.AddKey(Url.Root,
                                Url.ConvertSavedUrlToFireUrl(Espacio.Object.UrlEspacio));
            }
            else { urlEspacios = Url.Root; }
            if(Espacio.Object.Administradores != null)
            {
                foreach (var x in Espacio.Object.Administradores)
                {
                    urlUsuarios.Add(Url.AddKey(Url.Usuarios,
                                        Url.AddKey((string)x.Key, Url.Administradores)));
                }
            }
            if (Espacio.Object.Miembros != null)
            {
                foreach (var x in espacio.Object.Miembros)
                    urlUsuarios.Add(Url.AddKey(Url.Usuarios,
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
              .Child(urlEspacios)
              .OrderByKey()
              .EqualTo(Espacio.Key)
              .OnceAsync<Espacio>();

            Espacio.Key = null;
            foreach (var espaciox in espaciosx)
            {
                Espacio = new Go<Espacio>(espaciox);
            }
            return Espacio;
        }

        public async Task Create()
        {
            var x = await Db.Create(Espacio, urlEspacios);
            if (x != null)
            {
                foreach (var urlaux in urlUsuarios)
                {
                    await Db.Update(Espacio.Object.ReturnSmallEspacio(), Url.AddKey(urlaux, Espacio.Key));
                }  
            }
        }

        public async Task Update()
        {
            await Db.Update(Espacio, Url.AddKey(urlEspacios, Espacio.Key));
            foreach (string urlaux in urlUsuarios)
            {
                await Db.Update(Espacio, Url.AddKey(urlaux,Espacio.Key));
            }
        }

        public async Task Delete()
        {
            await Db.Delete(Url.AddKey(urlEspacios, Espacio.Key));
            foreach (string urlaux in urlUsuarios)
            {
                await Db.Delete(Url.AddKey(urlaux,Espacio.Key));
            }
        }

    }
}
