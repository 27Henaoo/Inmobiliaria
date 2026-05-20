using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // Clase base
    // 1:N Personas -> Telefonos
    // 1:N Personas -> Direcciones
    // 1:N Personas -> ExpedientesLaborales
    // =========================
    public class Personas
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public char Genero { get; set; }
        public string? Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }

        // FK
        public int EstadoCivil { get; set; }
        public int Nacionalidad { get; set; }

        // Navegaciones
        [ForeignKey("EstadoCivil")] public EstadosCiviles? _EstadoCivil { get; set; }

        [ForeignKey("Nacionalidad")] public Nacionalidades? _Nacionalidad { get; set; }

        public List<Telefonos>? Telefonos { get; set; }
        public List<Direcciones>? Direcciones { get; set; }

        // OJO:
        // ExpedientesFinancieros sale de Personas porque en el nuevo SQL
        // esa relación ya no es Personas -> ExpedientesFinancieros,
        // sino Clientes -> ExpedientesFinancieros.
        public List<ExpedientesLaborales>? ExpedientesLaborales { get; set; }

        public Personas()
        {
            FechaRegistro = DateTime.Now;
        }

        public int TiempoRegistrado()
        {
            return DateTime.Now.Year - FechaRegistro.Year;
        }
    }
}