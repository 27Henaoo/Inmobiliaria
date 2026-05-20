

using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH
{
    public interface IJefesSectoresNegocio
    {
        // Este método consulta y devuelve todos los JefesSectores.
        List<JefesSectores> Consultar();

        // Este método guarda JefesSectores nuevos.
        JefesSectores Guardar(JefesSectores entidad);

        // Este método modifica JefesSectores existentes.
        JefesSectores Modificar(JefesSectores entidad);

        // Este método borra JefesSectores existentes.
        JefesSectores Borrar(JefesSectores entidad);
    }
}
