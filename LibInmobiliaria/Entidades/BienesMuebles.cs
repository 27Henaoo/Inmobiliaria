namespace LibInmobiliaria.Entidades
{
    // =========================
    // Herencia: Bienes -> BienesMuebles
    // Tabla hija 1:1 del bien base
    // =========================
    public class BienesMuebles : Bienes /*Se cambian los string? con la finalidad de que no queden NULL en SQL por lo tanto aca deben quedar igual,
                                        para evitar que a la hora de llenar los campos queden NULL, por lo tanto se vuelve obligatorio llenarlos*/
    {
        public string Tipo { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string UbicacionActual { get; set; } = null!;
        public string Garantia { get; set; } = null!;
        public string Estado { get; set; } = null!;
    }
}