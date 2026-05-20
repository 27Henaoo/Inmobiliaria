using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // 1:N Clientes -> Propiedades
    // 1:N TiposPropiedades -> Propiedades
    // 1:N Propiedades -> Contratos
    // =========================
    public class Propiedades
    {
        public int Id { get; set; }

        public int NumeroHabitaciones { get; set; }
        public int NumeroBanos { get; set; }
        public bool Patio { get; set; }
        public int Entradas { get; set; }
        public int Pisos { get; set; }
        public DateTime AnioConstruccion { get; set; }
        public decimal ValorPropiedad { get; set; }
        public decimal ValorArriendo { get; set; }

        // En el SQL este campo quedó obligatorio para evitar los NULLS
        public string Estado { get; set; } = null!;

        // =========================
        // CLAVES FORÁNEAS
        // =========================

        //Debe apuntar específicamente a Clientes.
        public int Cliente { get; set; }

        public int TipoPropiedad { get; set; }

        // =========================
        // NAVEGACIONES
        // =========================

        [ForeignKey("Cliente")]public Clientes? _Cliente { get; set; }

        [ForeignKey("TipoPropiedad")]public TiposPropiedades? _TipoPropiedad { get; set; }

        public List<Contratos>? Contratos { get; set; }
    }
}