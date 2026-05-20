using System.ComponentModel.DataAnnotations.Schema;

namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // 1:1 Clientes -> ExpedientesFinancieros
    // 1:N ExpedientesFinancieros -> Bienes
    // 1:N ExpedientesFinancieros -> ActivosFinancieros
    // =========================
    public class ExpedientesFinancieros
    {
        public int Id { get; set; }

        // FK
        public int Persona { get; set; }

        // FK no cae sobre Personas, sino sobre Clientes.
        [ForeignKey("Persona")]public Clientes? _Persona { get; set; }

        public List<Bienes>? Bienes { get; set; }
        public List<ActivosFinancieros>? ActivosFinancieros { get; set; }

        public decimal CalcularPrecioBienes()
        {
            decimal suma = 0;

            if (Bienes != null)
            {
                foreach (var bien in Bienes)
                {
                    suma += bien.ValorActual;
                }
            }

            return suma;
        }
    }
}