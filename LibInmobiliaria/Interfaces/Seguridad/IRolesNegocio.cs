
using LibModelos._5._2LoginRegisterEntidades;

namespace LibInmobiliaria.Interfaces.Seguridad
{
    public interface IRolesNegocio
    {
        List<Roles> Consultar();

        Roles Guardar(Roles entidad);

        Roles Modificar(Roles entidad);

        Roles Borrar(Roles entidad);
    }
}
