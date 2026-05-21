
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH
{
    public interface ITrabajadoresNegocio
    {
        // Este método consulta y devuelve todos los Trabajadores.
        List<Trabajadores> Consultar();

        // Este método guarda Trabajadores nuevos.
        Trabajadores Guardar(Trabajadores entidad);

        // Este método modifica Trabajadores existentes.
        Trabajadores Modificar(Trabajadores entidad);

        // Este método borra Trabajadores existentes.
        Trabajadores Borrar(Trabajadores entidad);
    }
}
