
namespace LibInmobiliaria.Entidades
{
    // =========================
    // 1:N Departamentos -> Ciudades
    // Probable 1:1 Departamentos <-> AdministradoresDepartamentos
    // =========================
    public class Departamentos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Poblacion { get; set; }

        // FK
        // public int AdministradorId { get; set; } SE QUITA PARA QUE EF Edientifique que es una relacion 1 A 1

        // Navegación 1:1
       public AdministradoresDepartamentos? _AdministradorDepartamento { get; set; }

        public List<Ciudades>? Ciudades { get; set; }
    }
}

