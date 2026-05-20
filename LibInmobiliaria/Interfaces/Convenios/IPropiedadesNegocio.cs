using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Convenios
{
    public interface IPropiedadesNegocio
    {
        // Este método consulta y devuelve todos los Propiedades
        List<Propiedades> Consultar();
        // Este método guarda Propiedades nuevos en la base de datos.
        Propiedades Guardar(Propiedades entidad);
        // Este método modifica un Propiedades existentes en la base de datos.
        Propiedades Modificar(Propiedades entidad);
        // Este método borra Propiedades existentes en la base de datos.
        Propiedades Borrar(Propiedades entidad);


    }

}
