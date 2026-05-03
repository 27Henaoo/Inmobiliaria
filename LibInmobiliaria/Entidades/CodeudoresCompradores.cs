using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // Tabla puente N:N
    // Compradores <-> Codeudores
    // =========================
    public class CodeudoresCompradores
    {
        public int Id { get; set; }
        public DateTime FechaUnion { get; set; }
        public string Relacion { get; set; } = null!;

        // FKs
        public int Comprador { get; set; }
        public int Codeudor { get; set; }

        // Navegaciones
        [ForeignKey("Comprador")]public Compradores? _Comprador { get; set; }

        [ForeignKey("Codeudor")]public Codeudores? _Codeudor { get; set; }
    }
}