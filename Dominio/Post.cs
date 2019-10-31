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
        public string Id { get; set; }
        public Espacio Espacio { get; set; }
        public Usuario Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Tag Tag { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Post() { }

        public Post(FirebaseObject<Post> Post)
        {
            Id = Post.Key;
            Espacio = new Espacio();
            Espacio = Post.Object.Espacio;
            Usuario = new Usuario();
            Usuario = Post.Object.Usuario;
            Titulo = Post.Object.Titulo;
            Descripcion = Post.Object.Descripcion;
            Tag = new Tag();
            Tag = Post.Object.Tag;
            Date = Post.Object.Date;
            Deleted = Post.Object.Deleted;
        }

        public Post ReturnSmallPost()
        {
            return new Post
            {
                Id = Id,
                Titulo = Titulo,
                Descripcion = Descripcion,
                Tag = Tag,
                Date = Date,
                Deleted = Deleted,
                Espacio = Espacio.ReturnSmallEspacio(),
                Usuario = Usuario.ReturnSmallUsuario()
            };
        }
    }
}
