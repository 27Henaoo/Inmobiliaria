
namespace LibInmobiliaria.Interfaces
{
    public class Configuraciones
    {
        public static string obtener(string clave)
        {
            return "Server=JUANES\\DEV;Integrated Security=True;TrustServerCertificate=True;Database=db_inmobiliaria;";
        }
    }
}
