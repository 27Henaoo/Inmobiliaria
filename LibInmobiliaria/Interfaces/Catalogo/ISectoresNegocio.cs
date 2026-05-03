
using LibInmobiliaria.Entidades;

namespace LibInmobiliaria.Interfaces.Catalogo
{
    public interface ISectoresNegocio
    {
        // Este método consulta y devuelve todos los Sectores.
        List<Sectores> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Sectores Guardar(Sectores entidad);

        // Este método modifica un avión existente en la base de datos.
        Sectores Modificar(Sectores entidad);

        // Este método borra un avión existente en la base de datos.
        Sectores Borrar(Sectores entidad);
    }
}
