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
    public class TagNegocio
    {
        private FireUrl Url { get; set; } = new FireUrl("Tags");
        private Db Db { get; } = new Db();

        private Go<Tag> Tag { get; }

        private string UrlEspacios { get; } = "";

        public TagNegocio()
        { }
            public TagNegocio(Go<Tag> tag)
        {
            Tag = new Go<Tag>(tag);
            if(Tag.Object.Espacio.Object.UrlEspacio == null)
            {
                Tag.Object.Espacio.Object.UrlEspacio = "";
            }
            UrlEspacios = Url.AddKey(Url.Espacios,
                    Url.AddKey(Url.ConvertSavedUrlToFireUrl(Tag.Object.Espacio.Object.UrlEspacio),
                        Url.AddKey(Tag.Object.Espacio.Key, 
                            Url.Root)));
        }

        public async Task<List<Go<Tag>>> GetAllFromEspacios(string UrlEspacio)
        {
            UrlEspacio = Url.AddKey(Url.Espacios,
                   Url.AddKey(UrlEspacio,
                           Url.Root));

            List<Go<Tag>> tags = new List<Go<Tag>>();
               var data = await Db.Client()
              .Child(UrlEspacio)
              .OnceAsync<Tag>();

            foreach (var aux in data)
            {
                tags.Add(new Go<Tag>(aux)); 
            }

            return tags;
        }

        public async Task<Go<Tag>> GetObject()
            {
                var data = await Db.Client()
               .Child(UrlEspacios)
               .OrderByKey()
               .EqualTo(Tag.Key)
               .OnceSingleAsync<Tag>();

                if (data == null) { Tag.Key = null; }
                else { Tag.Object = data;  }
                
                return Tag;
            }

        public async Task<Go<Tag>> Create()
        {
          await Db.Create(Tag.Object.ReturnSmallTag(), UrlEspacios);
            return Tag;
        }
      
        public async Task<Go<Tag>> Update ()
        {
           await Db.Update(Tag.Object.ReturnSmallTag(), Url.AddKey(UrlEspacios,Tag.Key));
            return Tag;
        }

        public async Task<Go<Tag>> Delete (string url)
        {
            await Db.Delete(url);
            return Tag;
        }

    }
}
