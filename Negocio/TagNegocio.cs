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
       
        public async Task<List<Tag>> GetAll()
        {
            List<Tag> tags = new List<Tag>();
               var tagsAux = await Db.Client()
              .Child(Url.Root)
              .OnceAsync<Tag>();

            foreach (var tagAux in tagsAux)
            {
                tags.Add(new Tag(tagAux)); 
            }

            return tags;
        }

        public async Task<List<Tag>> GetAllFromEspacio(string urlEspacio)
        {
            List<Tag> tags = new List<Tag>();
            var tagsAux = await Db.Client()
           .Child(urlEspacio + "/" + Url.Root)
           .OnceAsync<Tag>();

            foreach (var tagAux in tagsAux)
            {
                tags.Add(new Tag(tagAux));
            }

            return tags;
        }

        public async Task<Tag> GetObject(string id)
            {
                Tag tag = new Tag();
                var tagsAux = await Db.Client()
               .Child(Url.Root)
               .OrderByKey()
               .EqualTo(id)
               .OnceAsync<Tag>();

                foreach (var tagAux in tagsAux)
                {
                    tag = new Tag(tagAux);
                }
                return tag;
            }

        public async Task Create(Tag tag)
        {
           await Db.Create(tag.ReturnSmallTag(), Url.Root);
           await Db.Create(tag, Url.AddKey(Url.Root,tag.Espacio.GetUrlEspacio()));
        }
      
        public async Task Update (string id, Tag tag)
        {
            tag.Id = null;
            await Db.Update(tag, Url.AddKey(Url.Root,id));
        }

        public async Task Delete (string id)
        {
            await Db.Delete(Url.AddKey(Url.Root,id));
        }

    }
}
