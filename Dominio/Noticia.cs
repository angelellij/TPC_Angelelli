﻿using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Noticia
    {
        public Go<Usuario> Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Date { get; set; }
        public bool Deleted { get; set; }

        public Noticia() { }
        public Noticia(FirebaseObject<Noticia> noticia)
        {
            Titulo = noticia.Object.Titulo;
            Descripcion = noticia.Object.Descripcion;
            Date = noticia.Object.Date;
            Deleted = noticia.Object.Deleted;
            Usuario = new Go<Usuario>(noticia.Object.Usuario);
        }

        public Noticia ReturnSmallNoticia()
        {
           return new Noticia
            {
                Titulo = Titulo,
                Descripcion = Descripcion,
                Date = Date,
                Deleted = Deleted,
                Usuario = new Go<Usuario>(Usuario.Key, Usuario.Object.ReturnSmallUsuario())
            };
        }
    }
}
