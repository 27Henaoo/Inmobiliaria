
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos
{
    public interface IPersonasNegocio
    {
        // Este método consulta y devuelve todos los Personas.
        List<Personas> Consultar();

        // Este método guarda Personas nuevos.
        Personas Guardar(Personas entidad);

        // Este método modifica Personas existentes.
        Personas Modificar(Personas entidad);

        // Este método borra Personas existentes.
        Personas Borrar(Personas entidad);
    }
}
