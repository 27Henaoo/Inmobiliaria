using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Comercial
{
    public interface ICompradoresNegocio
    {
        // Este método consulta y devuelve todos los Compradores
        List<Compradores> Consultar();
        // Este método guarda Compradores nuevos en la base de datos.
        Compradores Guardar(Compradores entidad);
        // Este método modifica un Compradores existentes en la base de datos.
        Compradores Modificar(Compradores entidad);
        // Este método borra Compradores existentes en la base de datos.
        Compradores Borrar(Compradores entidad);


    }
}
