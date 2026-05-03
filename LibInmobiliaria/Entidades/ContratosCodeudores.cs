using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // Tabla puente N:N
    // Contratos <-> Codeudores
    // =========================
    public class ContratosCodeudores
    {
        public int Id { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal PrecioAcordado { get; set; }
        public string VendidaArrendada { get; set; } = null!;

        // FKs
        public int Codeudor { get; set; }
        public int Contrato { get; set; }

        // Navegaciones
        [ForeignKey("Codeudor")]public Codeudores? _Codeudor { get; set; }

        [ForeignKey("Contrato")]public Contratos? _Contrato { get; set; }
    }
}