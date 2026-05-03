using LibInmobiliaria.Entidades;


namespace LibInmobiliaria.Interfaces.RRHH
{
    public interface IJefesSectoresNegocio
    {
        // Este método consulta y devuelve todos los JefesSectores.
        List<JefesSectores> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        JefesSectores Guardar(JefesSectores entidad);

        // Este método modifica un avión existente en la base de datos.
        JefesSectores Modificar(JefesSectores entidad);

        // Este método borra un avión existente en la base de datos.
        JefesSectores Borrar(JefesSectores entidad);
    }
}
