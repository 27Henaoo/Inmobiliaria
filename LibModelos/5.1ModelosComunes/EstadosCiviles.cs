
namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // 1:N EstadosCiviles -> Personas
    // =========================
    public class EstadosCiviles
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        // Un estado civil puede pertenecer a muchas personas
        public List<Personas>? _Personas { get; set; }
    }
}
