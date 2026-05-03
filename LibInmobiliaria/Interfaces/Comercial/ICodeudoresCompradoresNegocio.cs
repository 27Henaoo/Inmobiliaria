using LibInmobiliaria.Entidades;

namespace LibInmobiliaria.Interfaces.Comercial
{
    public interface ICodeudoresCompradoresNegocio
    {
        // Este método consulta y devuelve todos los CodeudoresCompradores
        List<CodeudoresCompradores> Consultar();
        // Este método guarda CodeudoresCompradores nuevos en la base de datos.
        CodeudoresCompradores Guardar(CodeudoresCompradores entidad);
        // Este método modifica un CodeudoresCompradores existentes en la base de datos.
        CodeudoresCompradores Modificar(CodeudoresCompradores entidad);
        // Este método borra CodeudoresCompradores existentes en la base de datos.
        CodeudoresCompradores Borrar(CodeudoresCompradores entidad);


    }
}
