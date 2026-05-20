
using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // 1:N Ciudades -> Sectores
    // 1:N Sectores -> EmpleadosSectores
    // Probable 1:1 Sectores <-> JefesSectores
    // =========================
    public class Sectores
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        // FK
        public int Ciudad { get; set; }
        [ForeignKey("Ciudad")] public Ciudades? _Ciudad { get; set; }

        // Navegación 1:1
        [InverseProperty("_Sector")] public JefesSectores? _JefeSector { get; set; } //_JefeSector se relaciona con la propiedad _Sector de JefesSectores

        [InverseProperty("_Sector")] public List<EmpleadosSectores>? EmpleadosSectores { get; set; }//EmpleadosSectores se relaciona con la propiedad _Sector de EmpleadosSectores
    }
}


