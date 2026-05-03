using LibInmobiliaria.Entidades;


namespace LibInmobiliaria.Interfaces.RRHH
{
    public interface IExpedientesLaboralesNegocio
    {
        // Este método consulta y devuelve todos los ExpedientesLaborales.
        List<ExpedientesLaborales> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        ExpedientesLaborales Guardar(ExpedientesLaborales entidad);

        // Este método modifica un avión existente en la base de datos.
        ExpedientesLaborales Modificar(ExpedientesLaborales entidad);

        // Este método borra un avión existente en la base de datos.
        ExpedientesLaborales Borrar(ExpedientesLaborales entidad);
    }
}
