namespace LibInmobiliaria.Entidades
{
    // =========================
    // Herencia: Bienes -> BienesInmuebles
    // Tabla hija 1:1 del bien base
    // =========================
    public class BienesInmuebles : Bienes /*Se cambian los string? con la finalidad de que no queden NULL en SQL por lo tanto aca deben quedar igual,
                                          para evitar que a la hora de llenar los campos queden NULL, por lo tanto se vuelve obligatorio llenarlos*/
    {
        public decimal MetrosCuadrados { get; set; }

        public string Direccion { get; set; } = null!;
        public string EstadoConservacion { get; set; } = null!;
        public string Estrato { get; set; } = null!;
        public string NumeroHabitaciones { get; set; } = null!;
        public string NumeroBanos { get; set; } = null!;
        public string CodigoCUC { get; set; } = null!;
        public string EncargosDeudas { get; set; } = null!;
    }
}