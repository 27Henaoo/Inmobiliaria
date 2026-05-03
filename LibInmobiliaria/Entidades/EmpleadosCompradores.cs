using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // Tabla puente N:N
    // Compradores <-> EmpleadosSectores
    // =========================
    public class EmpleadosCompradores
    {
        public int Id { get; set; }
        public DateTime FechaAsesoramiento { get; set; }

        //Fk

        public int Comprador { get; set; }
        public int Empleado { get; set; }

    
        //Navegacion
        [ForeignKey("Comprador")]public Compradores? _Comprador { get; set; }

        [ForeignKey("Empleado")]public EmpleadosSectores? _Empleado { get; set; }
    }
}