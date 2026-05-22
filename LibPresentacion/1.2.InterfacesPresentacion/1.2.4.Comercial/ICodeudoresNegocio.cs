
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial
{
    public interface ICodeudoresNegocio
    {
        // Este método consulta y devuelve todos los Codeudores.
        List<Codeudores> Consultar();

        // Este método guarda Codeudores nuevos.
        Codeudores Guardar(Codeudores entidad);

        // Este método modifica Codeudores existentes.
        Codeudores Modificar(Codeudores entidad);

        // Este método borra Codeudores existentes.
        Codeudores Borrar(Codeudores entidad);
    }
}
