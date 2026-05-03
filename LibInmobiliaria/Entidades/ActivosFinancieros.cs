using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // 1:N ExpedientesFinancieros -> ActivosFinancieros
    // =========================
    public class ActivosFinancieros
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        // En el SQL quedó obligatorio por lo tanto aqui tambien para mantener esos NULLS fuera
        public string Descripcion { get; set; } = null!;

        public DateTime FechaAdquisicion { get; set; }
        public decimal Precio { get; set; }

        // FK
        public int ExpedienteFinanciero { get; set; }
        [ForeignKey("ExpedienteFinanciero")]public ExpedientesFinancieros? _ExpedienteFinanciero { get; set; }
    }
}