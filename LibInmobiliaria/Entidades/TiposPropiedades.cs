
namespace LibInmobiliaria.Entidades
{
    // =========================
    // 1:N TiposPropiedades -> Propiedades
    // =========================
    public class TiposPropiedades
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public List<Propiedades>? Propiedades { get; set; }
    }
}
