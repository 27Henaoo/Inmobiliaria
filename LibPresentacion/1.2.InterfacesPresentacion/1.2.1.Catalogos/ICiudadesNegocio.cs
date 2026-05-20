using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface ICiudadesNegocio
    {
        // Este método consulta y devuelve todos los Ciudades.
        List<Ciudades> Consultar();

        // Este método guarda Ciudades nuevos.
        Ciudades Guardar(Ciudades entidad);

        // Este método modifica Ciudades existentes.
        Ciudades Modificar(Ciudades entidad);

        // Este método borra Ciudades existentes.
        Ciudades Borrar(Ciudades entidad);
    }
}
