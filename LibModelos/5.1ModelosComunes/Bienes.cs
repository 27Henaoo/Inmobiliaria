using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // Clase base de la jerarquía de bienes
    // 1:N ExpedientesFinancieros -> Bienes
    // =========================
    public class Bienes
    {
        public int Id { get; set; }

        // En el SQL estos campos quedaron NOT NULL, por eso aquí ya no deberían ir como string? Es decir para que se obligatorio introducirlos
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public DateTime FechaAdquisicion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal ValorActual { get; set; }

        // FK hacia ExpedientesFinancieros
        public int ExpedienteFinanciero { get; set; }

        // Navegación hacia el expediente financiero
        [ForeignKey("ExpedienteFinanciero")] public ExpedientesFinancieros? _ExpedienteFinanciero { get; set; }
    }
}