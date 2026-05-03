
namespace LibInmobiliaria.Entidades
{
    // =========================
    // 1:N TiposContratos -> EmpleadosSectores
    // =========================
    public class TiposContratos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public List<EmpleadosSectores>? EmpleadosSectores { get; set; }
    }
}
