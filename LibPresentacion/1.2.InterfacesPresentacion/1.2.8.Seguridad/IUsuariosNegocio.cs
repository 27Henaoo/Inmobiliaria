
using LibModelos._5._2LoginRegisterEntidades;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad
{
    public interface IUsuariosNegocio
    {
        List<Usuarios> Consultar();
        Usuarios Guardar(Usuarios entidad);
        Usuarios Modificar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);
        Usuarios ValidarLogin(Usuarios entidad);
    }
}
