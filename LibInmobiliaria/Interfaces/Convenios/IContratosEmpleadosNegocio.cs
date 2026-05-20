using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Convenios
{
    public interface IContratosEmpleadosNegocio
    {
        // Este método consulta y devuelve todos los ContratosEmpleados
        List<ContratosEmpleados> Consultar();
        // Este método guarda ContratosEmpleados nuevos en la base de datos.
        ContratosEmpleados Guardar(ContratosEmpleados entidad);
        // Este método modifica un ContratosEmpleados existentes en la base de datos.
        ContratosEmpleados Modificar(ContratosEmpleados entidad);
        // Este método borra ContratosEmpleados existentes en la base de datos.
        ContratosEmpleados Borrar(ContratosEmpleados entidad);


    }

}
