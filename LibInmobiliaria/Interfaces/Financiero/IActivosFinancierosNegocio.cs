using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Financiero
{
    public interface IActivosFinancierosNegocio
    {
        // Este método consulta y devuelve todos los ActivosFinancieros
        List<ActivosFinancieros> Consultar();
        // Este método guarda ActivosFinancieros nuevos en la base de datos.
        ActivosFinancieros Guardar(ActivosFinancieros entidad);
        // Este método modifica un ActivosFinancieros existentes en la base de datos.
        ActivosFinancieros Modificar(ActivosFinancieros entidad);
        // Este método borra ActivosFinancieros existentes en la base de datos.
        ActivosFinancieros Borrar(ActivosFinancieros entidad);


    }
}
