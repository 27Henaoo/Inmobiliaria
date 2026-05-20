
using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Sujetos
{
    public interface ITelefonosNegocio
    {
        // Este método consulta y devuelve todos los Telefonos.
        List<Telefonos> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Telefonos Guardar(Telefonos entidad);

        // Este método modifica un avión existente en la base de datos.
        Telefonos Modificar(Telefonos entidad);

        // Este método borra un avión existente en la base de datos.
        Telefonos Borrar(Telefonos entidad);

    }
}
