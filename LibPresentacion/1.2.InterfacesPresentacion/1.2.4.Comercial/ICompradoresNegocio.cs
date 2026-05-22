
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial
{
    public interface ICompradoresNegocio
    {
        // Este método consulta y devuelve todos los Compradores.
        List<Compradores> Consultar();

        // Este método guarda Compradores nuevos.
        Compradores Guardar(Compradores entidad);

        // Este método modifica Compradores existentes.
        Compradores Modificar(Compradores entidad);

        // Este método borra Compradores existentes.
        Compradores Borrar(Compradores entidad);
    }
}
