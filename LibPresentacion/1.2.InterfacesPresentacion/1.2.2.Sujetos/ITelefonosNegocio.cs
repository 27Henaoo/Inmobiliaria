
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos
{
    public interface ITelefonosNegocio
    {

        // Este método consulta y devuelve todos los Telefonos.
        List<Telefonos> Consultar();

        // Este método guarda Telefonos nuevos.
        Telefonos Guardar(Telefonos entidad);

        // Este método modifica Telefonos existentes.
        Telefonos Modificar(Telefonos entidad);

        // Este método borra Telefonos existentes.
        Telefonos Borrar(Telefonos entidad);
    }
}
