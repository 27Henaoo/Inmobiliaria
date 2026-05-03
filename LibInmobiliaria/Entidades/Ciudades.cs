
using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // 1:N Departamentos -> Ciudades
    // =========================
    public class Ciudades
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public string? Poblacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? CodigoPostal { get; set; }

        // FK
        public int Departamento { get; set; }

        // Navegación
        [ForeignKey("Departamento")] public Departamentos? _Departamento { get; set; }

        public List<Sectores>? _Sectores { get; set; }
    }

}
