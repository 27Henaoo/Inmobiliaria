

using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial
{
    public interface ICodeudoresCompradoresNegocio
    {
        // Este método consulta y devuelve todos los CodeudoresCompradores.
        List<CodeudoresCompradores> Consultar();

        // Este método guarda CodeudoresCompradores nuevos.
        CodeudoresCompradores Guardar(CodeudoresCompradores entidad);

        // Este método modifica CodeudoresCompradores existentes.
        CodeudoresCompradores Modificar(CodeudoresCompradores entidad);

        // Este método borra CodeudoresCompradores existentes.
        CodeudoresCompradores Borrar(CodeudoresCompradores entidad);
    }
}
