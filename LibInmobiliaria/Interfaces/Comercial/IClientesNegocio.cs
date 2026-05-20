using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Comercial
{
    public interface IClientesNegocio
    {
        // Este método consulta y devuelve todos los Clientes
        List<Clientes> Consultar();
        // Este método guarda Clientes nuevos en la base de datos.
        Clientes Guardar(Clientes entidad);
        // Este método modifica un Clientes existentes en la base de datos.
        Clientes Modificar(Clientes entidad);
        // Este método borra Clientes existentes en la base de datos.
        Clientes Borrar(Clientes entidad);


    }
}
