
using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Catalogo
{
    public interface ICiudadesNegocio
    {
        // Este método consulta y devuelve todos los Ciudades.
        List<Ciudades> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Ciudades Guardar(Ciudades entidad);

        // Este método modifica un avión existente en la base de datos.
        Ciudades Modificar(Ciudades entidad);

        // Este método borra un avión existente en la base de datos.
        Ciudades Borrar(Ciudades entidad);
    }
}
