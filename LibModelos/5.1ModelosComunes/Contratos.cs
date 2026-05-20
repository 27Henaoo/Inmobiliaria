using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // 1:N Clientes -> Contratos
    // 1:N Propiedades -> Contratos
    // 1:N Compradores -> Contratos
    // 1:N JefesSectores -> Contratos
    // N:N Contratos <-> EmpleadosSectores por ContratosEmpleados
    // N:N Contratos <-> Codeudores por ContratosCodeudores
    // =========================
    public class Contratos
    {
        public int Id { get; set; }

        public DateTime FechaContrato { get; set; }

        // En el SQL quedó obligatorio evitar Nulls
        public DateTime FechaFinalizacion { get; set; }

        // En el SQL quedó obligatorio evitar Nulls
        public string Observaciones { get; set; } = null!;

        public decimal PrecioAcordado { get; set; }
        public string ArriendoVenta { get; set; } = null!;

        // CLAVES FORÁNEAS

        public int Cliente { get; set; }
        public int Propiedad { get; set; }
        public int Comprador { get; set; }
        public int JefeSector { get; set; }

        // NAVEGACIONES
       
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }

        [ForeignKey("Propiedad")] public Propiedades? _Propiedad { get; set; }

        [ForeignKey("Comprador")] public Compradores? _Comprador { get; set; }

        [ForeignKey("JefeSector")]public JefesSectores? _JefeSector { get; set; }

        public List<ContratosEmpleados>? ContratosEmpleados { get; set; }
        public List<ContratosCodeudores>? ContratosCodeudores { get; set; }
    }
}