using System.ComponentModel.DataAnnotations.Schema;

namespace LibInmobiliaria.Entidades
{
    // =========================
    // Herencia: Trabajadores -> JefesSectores
    // 1:N AdministradoresDepartamentos -> JefesSectores
    // 1:1 Sectores <-> JefesSectores
    // 1:N JefesSectores -> EmpleadosSectores
    // 1:N JefesSectores -> Contratos
    // =========================
    public class JefesSectores : Trabajadores
    {
        public decimal PresupuestoSector { get; set; }

        // FK hacia el administrador que supervisa este jefe
        public int AdministradorDepartamento { get; set; }

        // FK hacia el sector que este jefe administra
        public int Sector { get; set; }

        // Navegación hacia el administrador
        [ForeignKey("AdministradorDepartamento")] public AdministradoresDepartamentos? _AdministradorDepartamento { get; set; } /*Se agrega InverseProperty("_JefeSector") en _Sector
        Para decirle a EF que esta navegación public Sectores? _Sector { get; set; }, corresponde exactamente con esta propiedad de Sectores:
        public JefesSectores? _JefeSector { get; set; }*/

        // Navegación hacia el sector
        // InverseProperty se usa para empatarla con la navegación
        // _JefeSector que existe en Sectores
        [ForeignKey("Sector")][InverseProperty("_JefeSector")] public Sectores? _Sector { get; set; }

        // Colección de empleados que dependen de este jefe
        [InverseProperty("_JefeSector")] public List<EmpleadosSectores>? EmpleadosSectores { get; set; }

        // Contratos gestionados por este jefe de sector
        public List<Contratos>? Contratos { get; set; }
    }
}
