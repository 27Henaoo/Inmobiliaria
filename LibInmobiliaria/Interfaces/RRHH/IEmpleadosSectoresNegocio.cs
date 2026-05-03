using LibInmobiliaria.Entidades;


namespace LibInmobiliaria.Interfaces.RRHH
{
    public interface IEmpleadosSectoresNegocio
    {
        // Este método consulta y devuelve todos los EmpleadosSectores.
        List<EmpleadosSectores> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        EmpleadosSectores Guardar(EmpleadosSectores entidad);

        // Este método modifica un avión existente en la base de datos.
        EmpleadosSectores Modificar(EmpleadosSectores entidad);

        // Este método borra un avión existente en la base de datos.
        EmpleadosSectores Borrar(EmpleadosSectores entidad);
    }
}
