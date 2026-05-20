using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // Tabla puente N:N
    // Contratos <-> EmpleadosSectores
    // =========================
    public class ContratosEmpleados
    {
        public int Id { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal PrecioAcordado { get; set; }
        public string VendidaArrendada { get; set; } = null!;

        // FKs
        public int Empleado { get; set; }
        public int Contrato { get; set; }

        // Navegaciones
        [ForeignKey("Empleado")] public EmpleadosSectores? _Empleado { get; set; }

        [ForeignKey("Contrato")] public Contratos? _Contrato { get; set; }
    }
}