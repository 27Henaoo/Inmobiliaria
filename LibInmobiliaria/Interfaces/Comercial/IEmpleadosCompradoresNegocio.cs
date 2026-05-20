using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Comercial
{
    public interface IEmpleadosCompradoresNegocio
    {
        // Este método consulta y devuelve todos los EmpleadosCompradores
        List<EmpleadosCompradores> Consultar();
        // Este método guarda EmpleadosCompradores nuevos en la base de datos.
        EmpleadosCompradores Guardar(EmpleadosCompradores entidad);
        // Este método modifica un EmpleadosCompradores existentes en la base de datos.
        EmpleadosCompradores Modificar(EmpleadosCompradores entidad);
        // Este método borra EmpleadosCompradores existentes en la base de datos.
        EmpleadosCompradores Borrar(EmpleadosCompradores entidad);


    }
}
