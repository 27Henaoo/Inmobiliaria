using LibInmobiliaria.Entidades;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface ITiposContratosNegocio
    {
        // Este método consulta y devuelve todos los TiposContratos.
        List<TiposContratos> Consultar();

        // Este método guarda TiposContratos nuevos.
        TiposContratos Guardar(TiposContratos entidad);

        // Este método modifica TiposContratos existentes.
        TiposContratos Modificar(TiposContratos entidad);

        // Este método borra TiposContratos existentes.
        TiposContratos Borrar(TiposContratos entidad);
    }
}
