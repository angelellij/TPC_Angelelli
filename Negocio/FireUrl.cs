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
        public string Espacios { get; } = "espacios";
        public string Usuarios { get; } = "usuarios";
        public FireUrl(string Root) => this.Root = Root;
        public string GetRootUrlFromKey(string Key)
        {
            if (Key.StartsWith("/"))
            {
                return Root + Key;
            }
            return Root + "/" + Key;
        }
        public string GetUrlFromList(List<string> Keys)
        {
            string url = Keys[0];
            for (int i = 1; i < Keys.Count(); i++)
            {
                url = url + "/" + Keys[i];
            }
            url = url.Replace("-","");
            return url;
        }
        public List<string> GetUrlList(string Url)
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
        public string GetUrlWithoutLastKey(string Url)
        {
            List<string> Keys;
            Keys = GetUrlList(Url);
            Keys.Remove(Keys[Keys.Count()-1]);
            return GetUrlFromList(Keys);
        }
        public string GetLastKeyFromUrl(string Url)
        {
            List<string> Keys;
            Keys = GetUrlList(Url);
            return Keys[Keys.Count() - 1];    
        }
        public string GetFirstKeyFromUrl(string Url)
        {
            return GetUrlList(Url)[0];
        }
        public string GetFireKeyUrl(string Url)
        {
            Url.Replace("/", "");
            return Url;
        }

        public string AddKeyToUrl(string Url, string Key)
        {
            return Url + "/" + Key;
        }

        public string RootInOneEspacio(string UrlEspacio, string EspacioKey)
        {
            return AddKeyToUrl(Espacios,
                       AddKeyToUrl(GetUrlFromList(GetUrlList(UrlEspacio)),
                            AddKeyToUrl(EspacioKey, 
                                Root)));
        }
        public string RootInOneUsuario(string KeyUsuario)
        {
            return AddKeyToUrl(Usuarios,
                       AddKeyToUrl(ConvertSavedUrlToFireUrl(KeyUsuario),
                                Root));
        }
        public string ConvertSavedUrlToFireUrl(string SavedUrl)
        {
            return GetUrlFromList(GetUrlList(SavedUrl));
        }
    }
}
