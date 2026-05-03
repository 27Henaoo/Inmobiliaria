
using LibInmobiliaria.Entidades;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface IEstadosCivilesNegocio
    {
        List<EstadosCiviles> Consultar();
        EstadosCiviles Guardar(EstadosCiviles entidad);
        //EstadosCiviles Modificar(EstadosCiviles entidad);
        //EstadosCiviles Borrar(EstadosCiviles entidad);
    }
}
