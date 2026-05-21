

using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH
{
    public interface IAdministradoresDepartamentosNegocio
    {
        List<AdministradoresDepartamentos> Consultar();

        // Este método guarda AdministradoresDepartamentos nuevos.
        AdministradoresDepartamentos Guardar(AdministradoresDepartamentos entidad);

        // Este método modifica AdministradoresDepartamentos existentes.
        AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad);

        // Este método borra AdministradoresDepartamentos existentes.
        AdministradoresDepartamentos Borrar(AdministradoresDepartamentos entidad);
    }
}
