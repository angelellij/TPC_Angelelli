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
        public TagNegocio(Go<Tag> tag)
        {
            Tag = new Go<Tag>(tag);
            UrlEspacios = Url.AddKey(Url.Espacios,
                    Url.AddKey(Url.ConvertSavedUrlToFireUrl(Tag.Object.Espacio.Object.UrlEspacio),
                        Url.AddKey(Tag.Object.Espacio.Key, 
                            Url.Root)));
        }

        public async Task<IDictionary<string, Tag>> GetAllFromEspacios()
        {
            IDictionary<string, Tag> tags = new Dictionary<string, Tag>();
               var data = await Db.Client()
              .Child(UrlEspacios)
              .OnceAsync<Tag>();

            foreach (var aux in data)
            {
                tags.Add(aux.Key,aux.Object); 
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

        public async Task Create()
        {
           await Db.Create(Tag, UrlEspacios);
        }
      
        public async Task Update ()
        {
            await Db.Update(Tag, Url.AddKey(UrlEspacios,Tag.Key));
        }

        public async Task Delete ()
        {
            await Db.Delete(Url.AddKey(UrlEspacios,Tag.Key));
        }

    }
}
