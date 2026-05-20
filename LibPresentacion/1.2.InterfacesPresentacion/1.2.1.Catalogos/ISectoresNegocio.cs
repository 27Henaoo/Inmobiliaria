using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface ISectoresNegocio
    {
        // Este método consulta y devuelve todos los Sectores.
        List<Sectores> Consultar();

        // Este método guarda Sectores nuevos.
        Sectores Guardar(Sectores entidad);

        // Este método modifica Sectores existentes.
        Sectores Modificar(Sectores entidad);

        // Este método borra Sectores existentes.
        Sectores Borrar(Sectores entidad);
    }
}
