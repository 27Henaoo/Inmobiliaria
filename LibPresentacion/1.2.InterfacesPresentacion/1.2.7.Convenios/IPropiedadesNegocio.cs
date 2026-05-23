
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios
{
    public interface IPropiedadesNegocio
    {
        // Este método consulta y devuelve todos los Propiedades.
        List<Propiedades> Consultar();

        // Este método guarda Propiedades nuevos.
        Propiedades Guardar(Propiedades entidad);

        // Este método modifica Propiedades existentes.
        Propiedades Modificar(Propiedades entidad);

        // Este método borra Propiedades existentes.
        Propiedades Borrar(Propiedades entidad);
    }
}
