
using LibInmobiliaria.Entidades;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface INacionalidadesNegocio
    {
        // Este método consulta y devuelve todos los Nacionalidades.
        List<Nacionalidades> Consultar();

        // Este método guarda Nacionalidades nuevos.
        Nacionalidades Guardar(Nacionalidades entidad);

        // Este método modifica Nacionalidades existentes.
        Nacionalidades Modificar(Nacionalidades entidad);

        // Este método borra Nacionalidades existentes.
        Nacionalidades Borrar(Nacionalidades entidad);
    }
}
