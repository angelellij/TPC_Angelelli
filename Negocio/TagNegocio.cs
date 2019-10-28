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
        private Tag FirebaseToTag(FirebaseObject<Tag> tagAux)
        {
            Tag tag = new Tag();
            tag.id = tagAux.Key;
            tag.nombre = tagAux.Object.nombre;
            tag.espacio = tagAux.Object.espacio;
            tag.colorLetra = tagAux.Object.colorLetra;
            tag.colorBackground = tagAux.Object.colorBackground;
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
                tags.Add(FirebaseToTag(tagAux)); 
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
                    tag = FirebaseToTag(tagAux);
                }
                return tag;
            }

            public async Task create(Tag tag)
        {
            tag.id = null;
            var result = await db.Client()
          .Child(root)
          .PostAsync(tag);
        }
      
        public async Task update (string id, Tag tag)
        {
            tag.id = null;
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
