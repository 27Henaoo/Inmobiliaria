using LibInmobiliaria.Entidades;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface ITiposPropiedadesNegocio
    {
        // Este método consulta y devuelve todos los TiposPropiedades.
        List<TiposPropiedades> Consultar();

        // Este método guarda TiposPropiedades nuevos.
        TiposPropiedades Guardar(TiposPropiedades entidad);

        // Este método modifica TiposPropiedades existentes.
        TiposPropiedades Modificar(TiposPropiedades entidad);

        // Este método borra TiposPropiedades existentes.
        TiposPropiedades Borrar(TiposPropiedades entidad);
    }
}
