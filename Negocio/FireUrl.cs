﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    class FireUrl
    {
        public string Root {get;set;}
        public string Espacios { get; } = "Espacios";
        public string Usuarios { get; } = "Usuarios";
        public string Administradores { get; } = "Administradores";
        public string Miembros { get; } = "Miembros";

        public FireUrl(string Root) => this.Root = Root;

        public string AddKey(string Url, string Key)
        {
            return Url + "/" + Key;
        }

        public string GetUrlFromList(List<string> Keys)
        {
            string url;
            if (Keys.Count != 0) { 
            url = Keys[0];
            for (int i = 1; i < Keys.Count(); i++)
            {
                url = url + "/" + Keys[i];
            }
                //            url = url.Replace("-","");
            }
            else { url = ""; }
            return url;
        }

        public List<string> GetKeyList(string Url)
        {
            string[] UrlListAux = (string[])Url.Split('-');
            List<string> UrlList = new List<string>();
            foreach (string UrlAux in UrlListAux)
            {
                if (!(UrlAux == null || UrlAux == ""))
                {
                    UrlAux.Replace("/", "");
                    UrlList.Add("-" + UrlAux);
                }
            }
            return UrlList;
        }

        public string ConvertSavedUrlToFireUrl(string SavedUrl)
        {
            return GetUrlFromList(GetKeyList(SavedUrl));
        }

        public string GetLastKeyFromUrl(string Url)
        {
            List<string> Keys;
            Keys = GetKeyList(Url);
            return Keys[Keys.Count() - 1];    
        }

        public string GetFirstKeyFromUrl(string Url)
        {
            return GetKeyList(Url)[0];
        }
       
        // H
        public string GetFireKeyUrl(string Url)
        {
            Url.Replace("/", "");
            return Url;
        }
    
        public string RootInOneEspacio(Go<Espacio> Espacio)
        {
            return AddKey(Espacios,
                       AddKey(ConvertSavedUrlToFireUrl(Espacio.Object.UrlEspacio),
                            Espacio.Key));
        }

        public string GetUrlWithoutLastKey(string Url)
        {
            List<string> Keys;
            Keys = GetKeyList(Url);
            Keys.Remove(Keys[Keys.Count() - 1]);
            return GetUrlFromList(Keys);
        }

    }
}
