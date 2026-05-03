using LibInmobiliaria.Entidades;


namespace LibInmobiliaria.Interfaces.RRHH
{
    public interface ITrabajadoresNegocio
    {
        // Este método consulta y devuelve todos los Trabajadores.
        List<Trabajadores> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Trabajadores Guardar(Trabajadores entidad);

        // Este método modifica un avión existente en la base de datos.
        Trabajadores Modificar(Trabajadores entidad);

        // Este método borra un avión existente en la base de datos.
        Trabajadores Borrar(Trabajadores entidad);
    }
}
