
using LibInmobiliaria.Entidades;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface IEstadosCivilesNegocio
    {
        // Este método consulta y devuelve todos los EstadosCiviles.
        List<EstadosCiviles> Consultar();

        // Este método guarda EstadosCiviles nuevos.
        EstadosCiviles Guardar(EstadosCiviles entidad);

        // Este método modifica EstadosCiviles existentes.
        EstadosCiviles Modificar(EstadosCiviles entidad);

        // Este método borra EstadosCiviles existentes.
        EstadosCiviles Borrar(EstadosCiviles entidad);
    }
}
