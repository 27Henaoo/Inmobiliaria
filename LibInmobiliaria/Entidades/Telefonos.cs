using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // 1:N Personas -> Telefonos
    // =========================
    public class Telefonos
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public string Prefijo { get; set; } = null!;

        // FK
        public int Persona { get; set; }

        //Navegacion
        [ForeignKey("Persona")] public Personas? _Persona { get; set; }
    }
}