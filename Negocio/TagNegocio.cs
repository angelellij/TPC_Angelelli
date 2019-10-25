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
        public async Task<List<Tag>> getAll()
        {
            List<Tag> tags = new List<Tag>();

            var tagsAux = await db.Client()
              .Child(root)
              .OnceAsync<Tag>();

            foreach (var tag in tagsAux)
            {
                tags.Add((Tag) tag.Object);
            }

            return tags;
        }

        public async void create(Tag tag)
        {
            var result = await db.Client()
          .Child(root)
          .PostAsync(tag);
        }
      
        public async void update (Tag tagAGuardar, Tag tagACambiar)
        {
            await db.Client()
            .Child(root)
            .Child(tagACambiar.id)
            .PutAsync(tagAGuardar);
        }

        public async void delete (Tag tag)
        {
            await db.Client()
              .Child(root)
              .Child(tag.id)
              .DeleteAsync();
        }

    }
}
