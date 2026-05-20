
using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Sujetos
{
    public interface IPersonasNegocio
    {
        // Este método consulta y devuelve todos los Personas.
        List<Personas> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Personas Guardar(Personas entidad);

        // Este método modifica un avión existente en la base de datos.
        Personas Modificar(Personas entidad);

        // Este método borra un avión existente en la base de datos.
        Personas Borrar(Personas entidad);
        
    }
}
