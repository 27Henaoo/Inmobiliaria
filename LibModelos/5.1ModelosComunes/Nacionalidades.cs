

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // 1:N Nacionalidades -> Personas
    // =========================
    public class Nacionalidades
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        // Una nacionalidad puede pertenecer a muchas personas
        public List<Personas>? _Personas { get; set; }
    }
}
