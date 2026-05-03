namespace LibInmobiliaria.Entidades
{
    // =========================
    // Herencia: Personas -> Compradores
    // 1:N Compradores -> Contratos
    // N:N Compradores <-> EmpleadosSectores por EmpleadosCompradores
    // N:N Compradores <-> Codeudores por CodeudoresCompradores
    // =========================
    public class Compradores : Personas
    {
        public List<Contratos>? Contratos { get; set; }
        public List<EmpleadosCompradores>? EmpleadosCompradores { get; set; }
        public List<CodeudoresCompradores>? CodeudoresCompradores { get; set; }
    }
}