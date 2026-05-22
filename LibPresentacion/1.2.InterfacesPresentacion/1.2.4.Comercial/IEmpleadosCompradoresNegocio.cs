
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial
{
    public interface IEmpleadosCompradoresNegocio
    {
        // Este método consulta y devuelve todos los EmpleadosCompradores.
        List<EmpleadosCompradores> Consultar();

        // Este método guarda EmpleadosCompradores nuevos.
        EmpleadosCompradores Guardar(EmpleadosCompradores entidad);

        // Este método modifica EmpleadosCompradores existentes.
        EmpleadosCompradores Modificar(EmpleadosCompradores entidad);

        // Este método borra EmpleadosCompradores existentes.
        EmpleadosCompradores Borrar(EmpleadosCompradores entidad);
    }
}
