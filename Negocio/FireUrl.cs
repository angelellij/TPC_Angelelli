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
            string url = "";
            foreach (string Key in Keys)
            {
                url = url + "/" + Key;
            }
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
        public string GetFireKeyUrl(string Url)
        {
            Url.Replace("/", "");
            return Url;
        }

        public string AddKeyToUrl(string Url, string Key)
        {
            return Url + "/" + Key;
        }

    }
}
