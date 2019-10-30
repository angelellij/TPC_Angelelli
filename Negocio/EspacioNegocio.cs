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
    public class EspacioNegocio
    {
        private FireUrl Url { get; } = new FireUrl("espacios");
        private Db Db { get; } = new Db();
        public async Task<List<Espacio>> GetAll()
        {
            List<Espacio> espacios = new List<Espacio>();
            List<string> urlEspacios = new List<string>
            {
                Url.Root
            };

            while (urlEspacios.Count > 0)
            {
                try
                {
                    var espaciosx = await Db.Client()
                                   .Child(urlEspacios[0])
                                   .OnceAsync<Espacio>();
                    foreach (var espaciox in espaciosx)
                    {
                        urlEspacios.Add(Url.AddKeyToUrl(urlEspacios[0], espaciox.Key));
                        espacios.Add(new Espacio(espaciox));
                    }
                }
                finally { }
                
                urlEspacios.Remove(urlEspacios[0]);
            }
            return espacios;
        }

        public async Task<Espacio> GetObject(string UrlEspacios)
        {
            var espaciosx = await Db.Client()
              .Child(Url.GetRootUrlFromKey(Url.GetUrlWithoutLastKey(UrlEspacios)))
              .OrderByKey()
              .EqualTo(Url.GetLastKeyFromUrl(UrlEspacios))
              .OnceAsync<Espacio>();

            Espacio espacio = new Espacio();
            foreach (var espaciox in espaciosx)
            {
                espacio = new Espacio(espaciox);
            }
            return espacio;
        }

        public async Task Create(Espacio espacio)
        {
            espacio.Id = null;
            if (espacio.UrlEspacio == null || espacio.UrlEspacio == ""){
                await Db.Create(espacio, Url.Root);
            }
            else
            {
                espacio.UrlEspacio = Url.GetFireKeyUrl(espacio.UrlEspacio);
                await Db.Create(espacio, Url.GetRootUrlFromKey(espacio.GetUrlEspacio()));
            }
            
        }

        public async Task Update(string url, Espacio espacio)
        {
            espacio.Id = null;
            await Db.Update(espacio, Url.GetRootUrlFromKey(url));
        }

        public async Task Delete(string url)
        {
            await Db.Delete(Url.GetRootUrlFromKey(url));
        }

    }
}
