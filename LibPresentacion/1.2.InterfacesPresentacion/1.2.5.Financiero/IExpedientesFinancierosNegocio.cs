
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero
{
    public interface IExpedientesFinancierosNegocio
    {
        // Este método consulta y devuelve todos los ExpedientesFinancieros.
        List<ExpedientesFinancieros> Consultar();

        // Este método guarda ExpedientesFinancieros nuevos.
        ExpedientesFinancieros Guardar(ExpedientesFinancieros entidad);

        // Este método modifica ExpedientesFinancieros existentes.
        ExpedientesFinancieros Modificar(ExpedientesFinancieros entidad);

        // Este método borra ExpedientesFinancieros existentes.
        ExpedientesFinancieros Borrar(ExpedientesFinancieros entidad);
    }
}
