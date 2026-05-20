
using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Sujetos
{
    public interface IDireccionesNegocio
    {
        // Este método consulta y devuelve todos los Direcciones.
        List<Direcciones> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Direcciones Guardar(Direcciones entidad);

        // Este método modifica un avión existente en la base de datos.
        Direcciones Modificar(Direcciones entidad);

        // Este método borra un avión existente en la base de datos.
        Direcciones Borrar(Direcciones entidad);

    }
}
