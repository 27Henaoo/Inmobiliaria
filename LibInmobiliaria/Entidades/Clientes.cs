namespace LibInmobiliaria.Entidades
{
    // =========================
    // Herencia: Personas -> Clientes
    // 1:N Clientes -> Propiedades
    // 1:N Clientes -> Contratos
    // 1:1 Clientes -> ExpedientesFinancieros
    // =========================
    public class Clientes : Personas
    {
        public decimal PorcentajeComision { get; set; }
        public int CantidadContratos { get; set; }
        public string? PrioridadCliente { get; set; }
        public string? MotivoVenta { get; set; }

        // Un cliente tiene un solo expediente financiero.
        public ExpedientesFinancieros? _ExpedienteFinanciero { get; set; }
    }
}