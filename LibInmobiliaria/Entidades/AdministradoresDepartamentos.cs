using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // Herencia: Trabajadores -> AdministradoresDepartamentos
    // 1:1 Departamentos <-> AdministradoresDepartamentos
    // 1:N AdministradoresDepartamentos -> JefesSectores
    // =========================
    public class AdministradoresDepartamentos : Trabajadores
    {
        public decimal PresupuestoDepartamento { get; set; }

        // FK real hacia Departamentos
        public int Departamento { get; set; }

        // Navegación hacia el departamento administrado
        [ForeignKey("Departamento")] public Departamentos? _Departamento { get; set; }

        // Un administrador puede tener varios jefes de sector a cargo
        public List<JefesSectores>? JefesSectores { get; set; }
    }
}