namespace LibModelos._5._1ModelosComunes
{
    // =========================
    // Herencia: Personas -> Trabajadores
    // Tabla hija 1:1 de Personas
    // =========================
    public class Trabajadores : Personas /*Se cambian los string? de Jorna con la finalidad de que no queden NULL en SQL por lo tanto aca deben quedar igual,
                                        para evitar que a la hora de llenar los campos no queden NULL, por lo tanto se vuelve obligatorio llenarlos*/
    {
        public decimal Sueldo { get; set; }
        public bool Estado { get; set; }
        public string Jornada { get; set; } = null!;
    }
}