using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Convenios
{
    public interface IContratosNegocio
    {
        // Este método consulta y devuelve todos los Contratos
        List<Contratos> Consultar();
        // Este método guarda Contratos nuevos en la base de datos.
        Contratos Guardar(Contratos entidad);
        // Este método modifica un Contratos existentes en la base de datos.
        Contratos Modificar(Contratos entidad);
        // Este método borra Contratos existentes en la base de datos.
        Contratos Borrar(Contratos entidad);


    }

}
