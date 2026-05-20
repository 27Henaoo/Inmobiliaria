using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // 1:N Personas -> ExpedientesLaborales
    // =========================
    public class ExpedientesLaborales
    {
        public int Id { get; set; }

        public string NombreEmpresa { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public decimal SalarioPagado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }

        // En el SQL quedaron obligatorios evitar llenar con Nulls
        public string Desempeno { get; set; } = null!;
        public string MotivoSalida { get; set; } = null!;

        // FK
        public int Persona { get; set; }
        [ForeignKey("Persona")]public Personas? _Persona { get; set; }
    }
}