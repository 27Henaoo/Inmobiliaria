using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Financiero
{
    public interface IExpedientesFinancierosNegocio
    {
        // Este método consulta y devuelve todos los ExpedientesFinancieros
        List<ExpedientesFinancieros> Consultar();
        // Este método guarda ExpedientesFinancieros nuevos en la base de datos.
        ExpedientesFinancieros Guardar(ExpedientesFinancieros entidad);
        // Este método modifica un ExpedientesFinancieros existentes en la base de datos.
        ExpedientesFinancieros Modificar(ExpedientesFinancieros entidad);
        // Este método borra ExpedientesFinancieros existentes en la base de datos.
        ExpedientesFinancieros Borrar(ExpedientesFinancieros entidad);


    }
}
