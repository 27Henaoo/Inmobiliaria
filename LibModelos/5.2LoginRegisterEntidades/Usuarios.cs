using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._2LoginRegisterEntidades
{
    public class Usuarios
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string ClaveHash { get; set; } = null!;

        public string ClaveSalt { get; set; } = null!;

        // FK
        public int Rol { get; set; }

        // Navegacion
        [ForeignKey("Rol")]public Roles? _Rol { get; set; }
    }
}
