
using LibModelos._5._2LoginRegisterEntidades;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad
{
    public interface IRolesNegocio
    {
        List<Roles> Consultar();
        Roles Guardar(Roles entidad);
        Roles Modificar(Roles entidad);
        Roles Borrar(Roles entidad);
    }
}
