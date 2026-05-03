using LibInmobiliaria.Entidades;

namespace LibInmobiliaria.Interfaces.Convenios
{
    public interface IContratosCodeudoresNegocio
    {
        // Este método consulta y devuelve todos los ContratosCodeudores
        List<ContratosCodeudores> Consultar();
        // Este método guarda ContratosCodeudores nuevos en la base de datos.
        ContratosCodeudores Guardar(ContratosCodeudores entidad);
        // Este método modifica un ContratosCodeudores existentes en la base de datos.
        ContratosCodeudores Modificar(ContratosCodeudores entidad);
        // Este método borra ContratosCodeudores existentes en la base de datos.
        ContratosCodeudores Borrar(ContratosCodeudores entidad);


    }

}
