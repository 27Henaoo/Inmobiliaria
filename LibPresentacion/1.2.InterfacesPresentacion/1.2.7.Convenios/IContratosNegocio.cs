using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios
{
    public interface IContratosNegocio
    {
        // Este método consulta y devuelve todos los Contratos.
        List<Contratos> Consultar();

        // Este método guarda Contratos nuevos.
        Contratos Guardar(Contratos entidad);

        // Este método modifica Contratos existentes.
        Contratos Modificar(Contratos entidad);

        // Este método borra Contratos existentes.
        Contratos Borrar(Contratos entidad);
    }
}
