using LibInmobiliaria.Entidades;


namespace LibInmobiliaria.Interfaces.RRHH
{
    public interface IAdministradoresDepartamentosNegocio
    {
        // Este método consulta y devuelve todos los AdministradoresDepartamentos.
        List<AdministradoresDepartamentos> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        AdministradoresDepartamentos Guardar(AdministradoresDepartamentos entidad);

        // Este método modifica un avión existente en la base de datos.
        AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad);

        // Este método borra un avión existente en la base de datos.
        AdministradoresDepartamentos Borrar(AdministradoresDepartamentos entidad);
    }
}
