
using LibInmobiliaria.Entidades;

namespace LibInmobiliaria.Interfaces.Catalogo
{
    public interface ITiposPropiedadesNegocio
    {
        // Este método consulta y devuelve todos los TiposProiedades.
        List<TiposPropiedades> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        TiposPropiedades Guardar(TiposPropiedades entidad);

        // Este método modifica un avión existente en la base de datos.
        TiposPropiedades Modificar(TiposPropiedades entidad);

        // Este método borra un avión existente en la base de datos.
        TiposPropiedades Borrar(TiposPropiedades entidad);
    }
}
