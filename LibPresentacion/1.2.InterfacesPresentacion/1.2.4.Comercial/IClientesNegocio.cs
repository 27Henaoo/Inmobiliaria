
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial
{
    public interface IClientesNegocio
    {
        // Este método consulta y devuelve todos los Clientes.
        List<Clientes> Consultar();

        // Este método guarda Clientes nuevos.
        Clientes Guardar(Clientes entidad);

        // Este método modifica Clientes existentes.
        Clientes Modificar(Clientes entidad);

        // Este método borra Clientes existentes.
        Clientes Borrar(Clientes entidad);
    }
}
