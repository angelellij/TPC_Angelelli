using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email{ get; set; }
        public string Contrasena { get; set; }
        public string FNacimiento { get; set; }
        public string UrlFoto { get; set; }
        public List<Espacio> ListadoEspacios { get; set; }
        public string TimestampUtc { get; set; }
        public Usuario() { }
        public Usuario(FirebaseObject<Usuario> usuario)
        {
            Id = usuario.Key;
            Nombre = usuario.Object.Nombre;
            Apellido = usuario.Object.Apellido;
            Email = usuario.Object.Email;
            Contrasena = usuario.Object.Contrasena;
            UrlFoto = usuario.Object.UrlFoto;
            FNacimiento = usuario.Object.FNacimiento;
            TimestampUtc = usuario.Object.TimestampUtc;
            ListadoEspacios = usuario.Object.ListadoEspacios;
        }

        public Usuario ReturnSmallUsuario()
        {
            Usuario SmallUsuario = new Usuario
            {
                Id = Id,
                Nombre = Nombre,
                Apellido = Apellido,
                UrlFoto = UrlFoto
            };
            return SmallUsuario;
        }
        

    }
}
