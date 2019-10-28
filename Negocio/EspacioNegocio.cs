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
        private String root = "Espacios";
        private Db db = new Db();
        private Espacio FirebaseObjectToObject(FirebaseObject<Espacio> espaciox)
        {
            Espacio espacio = new Espacio();
            espacio.Id = espaciox.Key;
            espacio.Nombre = espaciox.Object.Nombre;
            espacio.Descripcion = espaciox.Object.Descripcion;
            espacio.UrlEspacio = espaciox.Object.UrlEspacio;
            espacio.Miembros = espaciox.Object.Miembros;
            espacio.Administradores = espaciox.Object.Administradores;
            espacio.Date = espaciox.Object.Date;
            espacio.Deleted = espaciox.Object.Deleted;
            return espacio;
        }
        public async Task<List<Espacio>> getAll()
        {
            List<Espacio> espacios = new List<Espacio>();
            List<string> urlEspacios = new List<string>();
            urlEspacios.Add(root);

            while (urlEspacios.Count > 0)
            {
                try
                {
                    var espaciosx = await db.Client()
                                   .Child(urlEspacios[0])
                                   .OnceAsync<Espacio>();

                    foreach (var espaciox in espaciosx)
                    {
                        urlEspacios.Add(urlEspacios[0] + "/" + espaciox.Key);
                        espacios.Add(FirebaseObjectToObject(espaciox));
                    }
                }
                finally { }
                
                urlEspacios.Remove(urlEspacios[0]);
            }
            return espacios;
        }

        public async Task<Espacio> getObject(string urlEspacios)
        {
            string[] url = (string[])urlEspacios.Split('-');
            urlEspacios = root;
            Espacio espacio = new Espacio();
            espacio.Id = "-" + url[url.Count() - 1];
            for(int x =1; x < url.Count()-1; x++)
            {
                urlEspacios = urlEspacios + "/-" + url[x];
            }
            var espaciosx = await db.Client()
              .Child(urlEspacios)
              .OrderByKey()
              .EqualTo(espacio.Id)
              .OnceAsync<Espacio>();

            foreach (var espaciox in espaciosx)
            {
                espacio = FirebaseObjectToObject(espaciox);
            }
            return espacio;
        }

        public async Task create(Espacio espacio)
        {
            espacio.Id = null;
            if (espacio.UrlEspacio == null){
                var result = await db.Client()
                 .Child(root)
                 .PostAsync(espacio);
            }
            else
            {
              var result = await db.Client()
             .Child(root)
             .Child(espacio.UrlEspacio)
             .PostAsync(espacio);
            }
            
        }

        public async Task update(string id, Espacio tag)
        {
            tag.Id = null;
            await db.Client()
            .Child(root)
            .Child(id)
            .PutAsync(tag);
        }

        public async Task delete(string id)
        {
            await db.Client()
              .Child(root)
              .Child(id)
              .DeleteAsync();
        }

    }
}
