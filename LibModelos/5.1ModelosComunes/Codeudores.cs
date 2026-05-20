namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // Herencia: Personas -> Codeudores
    // N:N Codeudores <-> Contratos por ContratosCodeudores
    // N:N Codeudores <-> Compradores por CodeudoresCompradores
    // =========================
    public class Codeudores : Personas
    {
        public List<ContratosCodeudores>? ContratosCodeudores { get; set; }
        public List<CodeudoresCompradores>? CodeudoresCompradores { get; set; }
    }
}