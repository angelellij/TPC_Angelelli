using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Post
    {
        public Go<Espacio> Espacio { get; set; }
        public Go<Usuario> Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Go<Tag> Tag { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Post() { }

        public Post(FirebaseObject<Post> Post)
        {
           
            Titulo = Post.Object.Titulo;
            Descripcion = Post.Object.Descripcion;
            Date = Post.Object.Date;
            Deleted = Post.Object.Deleted;
            Espacio = new Go<Espacio>(Espacio);
            Usuario = new Go<Usuario>(Usuario);
            Tag = new Go<Tag>(Tag);
        }

        public Post ReturnSmallPost()
        {
           return new Post
            {
                Titulo = Titulo,
                Descripcion = Descripcion,
                Date = Date,
                Deleted = Deleted,
                Espacio = new Go<Espacio>(Espacio.Key, Espacio.Object.ReturnSmallEspacio()),
                Tag = new Go<Tag> (Tag),
                Usuario = new Go<Usuario>(Usuario.Key, Usuario.Object.ReturnSmallUsuario())     
            };
        }
    }
}
