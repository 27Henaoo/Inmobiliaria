
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios
{
    public interface IContratosEmpleadosNegocio
    {
        // Este método consulta y devuelve todos los ContratosEmpleados.
        List<ContratosEmpleados> Consultar();

        // Este método guarda ContratosEmpleados nuevos.
        ContratosEmpleados Guardar(ContratosEmpleados entidad);

        // Este método modifica ContratosEmpleados existentes.
        ContratosEmpleados Modificar(ContratosEmpleados entidad);

        // Este método borra ContratosEmpleados existentes.
        ContratosEmpleados Borrar(ContratosEmpleados entidad);
    }
}
