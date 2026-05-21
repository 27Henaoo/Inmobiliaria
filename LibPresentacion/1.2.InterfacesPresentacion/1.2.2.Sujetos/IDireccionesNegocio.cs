using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos
{
    public interface IDireccionesNegocio
    {
        // Este método consulta y devuelve todos los Direcciones.
        List<Direcciones> Consultar();

        // Este método guarda Direcciones nuevos.
        Direcciones Guardar(Direcciones entidad);

        // Este método modifica Direcciones existentes.
        Direcciones Modificar(Direcciones entidad);

        // Este método borra Direcciones existentes.
        Direcciones Borrar(Direcciones entidad);
    }
}
