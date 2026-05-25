namespace LibModelos._5._2LoginRegisterEntidades
{
    public class Roles
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public List<Usuarios>? Usuarios { get; set; }
    }
}
