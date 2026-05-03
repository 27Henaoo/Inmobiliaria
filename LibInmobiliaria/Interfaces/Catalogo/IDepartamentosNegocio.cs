
using LibInmobiliaria.Entidades;

namespace LibInmobiliaria.Interfaces.Catalogo
{
    public interface IDepartamentosNegocio
    {
        // Este método consulta y devuelve todos los Departamentos.
        List<Departamentos> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Departamentos Guardar(Departamentos entidad);

        // Este método modifica un avión existente en la base de datos.
        Departamentos Modificar(Departamentos entidad);

        // Este método borra un avión existente en la base de datos.
        Departamentos Borrar(Departamentos entidad);
    }
}
