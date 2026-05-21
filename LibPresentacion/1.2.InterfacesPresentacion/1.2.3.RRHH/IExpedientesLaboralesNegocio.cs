
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH
{
    public interface IExpedientesLaboralesNegocio
    {
        // Este método consulta y devuelve todos los ExpedientesLaborales.
        List<ExpedientesLaborales> Consultar();

        // Este método guarda ExpedientesLaborales nuevos.
        ExpedientesLaborales Guardar(ExpedientesLaborales entidad);

        // Este método modifica ExpedientesLaborales existentes.
        ExpedientesLaborales Modificar(ExpedientesLaborales entidad);

        // Este método borra ExpedientesLaborales existentes.
        ExpedientesLaborales Borrar(ExpedientesLaborales entidad);
    }
}
