
using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Catalogo
{
    public interface INacionalidadesNegocio
    {
        // Este método consulta y devuelve todos los Nacicionalidades.
        List<Nacionalidades> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Nacionalidades Guardar(Nacionalidades entidad);

        // Este método modifica un avión existente en la base de datos.
        Nacionalidades Modificar(Nacionalidades entidad);

        // Este método borra un avión existente en la base de datos.
        Nacionalidades Borrar(Nacionalidades entidad);
    }
}
