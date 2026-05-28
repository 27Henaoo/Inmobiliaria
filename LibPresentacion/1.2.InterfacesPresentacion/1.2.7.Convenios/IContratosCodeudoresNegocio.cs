
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios
{
    public interface IContratosCodeudoresNegocio
    {
        // Este método consulta y devuelve todos los ContratosCodeudores.
        List<ContratosCodeudores> Consultar();

        // Este método guarda ContratosCodeudores nuevos.
        ContratosCodeudores Guardar(ContratosCodeudores entidad);

        // Este método modifica ContratosCodeudores existentes.
        ContratosCodeudores Modificar(ContratosCodeudores entidad);

        // Este método borra ContratosCodeudores existentes.
        ContratosCodeudores Borrar(ContratosCodeudores entidad);
    }
}
