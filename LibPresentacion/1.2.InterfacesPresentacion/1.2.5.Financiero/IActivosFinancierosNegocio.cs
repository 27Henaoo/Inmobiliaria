using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero
{
    public interface IActivosFinancierosNegocio
    {
        // Este método consulta y devuelve todos los ActivosFinancieros.
        List<ActivosFinancieros> Consultar();

        // Este método guarda ActivosFinancieros nuevos.
        ActivosFinancieros Guardar(ActivosFinancieros entidad);

        // Este método modifica ActivosFinancieros existentes.
        ActivosFinancieros Modificar(ActivosFinancieros entidad);

        // Este método borra ActivosFinancieros existentes.
        ActivosFinancieros Borrar(ActivosFinancieros entidad);
    }
}
