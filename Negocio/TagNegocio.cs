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
        private String root = "tags";
        private Db db = new Db();
        private Tag FirebaseObjectToObject(FirebaseObject<Tag> tagAux)
        {
            Tag tag = new Tag();
            tag.Id = tagAux.Key;
            tag.Nombre = tagAux.Object.Nombre;
            tag.Espacio = tagAux.Object.Espacio;
            tag.ColorLetra = tagAux.Object.ColorLetra;
            tag.ColorBackground = tagAux.Object.ColorBackground;
            return tag;
        }
        public async Task<List<Tag>> getAll()
        {
            List<Tag> tags = new List<Tag>();
               var tagsAux = await db.Client()
              .Child(root)
              .OnceAsync<Tag>();

            foreach (var tagAux in tagsAux)
            {
                tags.Add(FirebaseObjectToObject(tagAux)); 
            }

            return tags;
        }

            public async Task<Tag> getTag(string id)
            {
                Tag tag = new Tag();
                var tagsAux = await db.Client()
               .Child(root)
               .OrderByKey()
               .EqualTo(id)
               .OnceAsync<Tag>();

                foreach (var tagAux in tagsAux)
                {
                    tag = FirebaseObjectToObject(tagAux);
                }
                return tag;
            }

            public async Task create(Tag tag)
        {
            tag.Id = null;
            var result = await db.Client()
          .Child(root)
          .PostAsync(tag);
        }
      
        public async Task update (string id, Tag tag)
        {
            tag.Id = null;
            await db.Client()
            .Child(root)
            .Child(id)
            .PutAsync(tag);
        }

        public async Task delete (string id)
        {
            await db.Client()
              .Child(root)
              .Child(id)
              .DeleteAsync();
        }

    }
}
