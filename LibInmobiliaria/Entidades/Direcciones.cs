using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // 1:N Personas -> Direcciones
    // =========================
    public class Direcciones
    {
        public int Id { get; set; }
        public string TipoVia { get; set; } = null!; // En el SQL quedó obligatorio evitar Nulls en llenado de datos o inserts
        public string Numero { get; set; } = null!; // En el SQL quedó obligatorio evitar Nulls en llenado de datos o inserts

        // En el SQL quedó obligatorio evitar Nulls en llenado de datos o inserts
        public string Complemento { get; set; } = null!;

        // FK
        public int Persona { get; set; }
        
        //Navegacion
        [ForeignKey("Persona")] public Personas? _Persona { get; set; }
    }
}