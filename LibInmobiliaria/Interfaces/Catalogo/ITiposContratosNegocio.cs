
using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Catalogo
{
    public interface ITiposContratosNegocio
    {
        // Este método consulta y devuelve todos los Contratos.
        List<TiposContratos> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        TiposContratos Guardar(TiposContratos entidad);

        // Este método modifica un avión existente en la base de datos.
        TiposContratos Modificar(TiposContratos entidad);

        // Este método borra un avión existente en la base de datos.
        TiposContratos Borrar(TiposContratos entidad);
    }
}
