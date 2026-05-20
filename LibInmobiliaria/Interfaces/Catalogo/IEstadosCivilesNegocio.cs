using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Catalogo
{
    // Esta interfaz define las operaciones de negocio que se pueden realizar con la entidad EstadosCiviles.
    public interface IEstadosCivilesNegocio
    {
        // Este método consulta y devuelve todos los EstadosCiviles.
        List<EstadosCiviles> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        EstadosCiviles Guardar(EstadosCiviles entidad);

        // Este método modifica un avión existente en la base de datos.
        EstadosCiviles Modificar(EstadosCiviles entidad);

        // Este método borra un avión existente en la base de datos.
        EstadosCiviles Borrar(EstadosCiviles entidad);
    }
}
