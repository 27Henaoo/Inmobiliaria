using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // Herencia: Trabajadores -> EmpleadosSectores
    // 1:N Sectores -> EmpleadosSectores
    // 1:N TiposContratos -> EmpleadosSectores
    // 1:N JefesSectores -> EmpleadosSectores
    // N:N Compradores <-> EmpleadosSectores por EmpleadosCompradores
    // N:N Contratos <-> EmpleadosSectores por ContratosEmpleados
    // =========================
    public class EmpleadosSectores : Trabajadores
    {
        // =========================
        // CLAVES FORÁNEAS
        // =========================
        public int JefeSector { get; set; }
        public int TipoContrato { get; set; }
        public int Sector { get; set; }

        // =========================
        // NAVEGACIONES
        // =========================

        // Navegación hacia el sector del empleado
        [ForeignKey("Sector")][InverseProperty("EmpleadosSectores")] public Sectores? _Sector { get; set; } //Se agrega InverseProperty("EmpleadosSectores") en _Sector,
                                                                                                            //para enlazar explícitamente esta navegación
                                                                                                            //public Sectores? _Sector { get; set; } con
                                                                                                            //colección en Sectores public List<EmpleadosSectores>? EmpleadosSectores { get; set; }

        // Navegación hacia el tipo de contrato
        [ForeignKey("TipoContrato")] public TiposContratos? _TipoContrato { get; set; }

        // Navegación hacia el jefe de sector
        [ForeignKey("JefeSector")][InverseProperty("EmpleadosSectores")]public JefesSectores? _JefeSector { get; set; }

        // Tablas puente
        public List<EmpleadosCompradores>? EmpleadosCompradores { get; set; }
        public List<ContratosEmpleados>? ContratosEmpleados { get; set; }

        public decimal CalcularSalario()
        {
            if (_TipoContrato?.Nombre == "Fijo" || _TipoContrato?.Nombre == "Indefinido")
                return 2550000.0m;
            else if (_TipoContrato?.Nombre == "Obra" || _TipoContrato?.Nombre == "Ocasional")
                return 1900000.0m;
            else if (_TipoContrato?.Nombre == "Aprendizaje")
                return 1500000.0m;
            else
                return 0m;
        }
    }
}