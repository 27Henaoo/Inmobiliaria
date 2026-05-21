
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH
{
    public interface IEmpleadosSectoresNegocio
    {
        List<EmpleadosSectores> Consultar();

        // Este método guarda EmpleadosSectores nuevos.
        EmpleadosSectores Guardar(EmpleadosSectores entidad);

        // Este método modifica EmpleadosSectores existentes.
        EmpleadosSectores Modificar(EmpleadosSectores entidad);

        // Este método borra EmpleadosSectores existentes.
        EmpleadosSectores Borrar(EmpleadosSectores entidad);
    }
}
