

using LibModelos._5._2LoginRegisterEntidades;

namespace LibInmobiliaria.Interfaces.Seguridad
{
    public interface IUsuariosNegocio
    {
        List<Usuarios> Consultar();

        Usuarios Guardar(Usuarios entidad);

        Usuarios Modificar(Usuarios entidad);

        Usuarios Borrar(Usuarios entidad);

        Usuarios? BuscarPorCorreo(string correo);

        Usuarios? ValidarLogin(string correo, string clave);
    }
}
