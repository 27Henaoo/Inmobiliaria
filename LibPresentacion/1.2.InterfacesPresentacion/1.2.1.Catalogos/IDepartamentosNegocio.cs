using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos
{
    public interface IDepartamentosNegocio
    {
        // Este método consulta y devuelve todos los Departamentos.
        List<Departamentos> Consultar();

        // Este método guarda Departamentos nuevos.
        Departamentos Guardar(Departamentos entidad);

        // Este método modifica Departamentos existentes.
        Departamentos Modificar(Departamentos entidad);

        // Este método borra Departamentos existentes.
        Departamentos Borrar(Departamentos entidad);
    }
}
