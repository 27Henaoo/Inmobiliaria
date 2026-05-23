using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._6.Patrimonio
{
    public interface IBienesNegocio
    {
        // Este método consulta y devuelve todos los Bienes.
        List<Bienes> Consultar();

        // Este método guarda Bienes nuevos.
        Bienes Guardar(Bienes entidad);

        // Este método modifica Bienes existentes.
        Bienes Modificar(Bienes entidad);

        // Este método borra Bienes existentes.
        Bienes Borrar(Bienes entidad);
    }
}
