using LibModelos._5._1ModelosComunes;

namespace LibInmobiliaria.Interfaces.Comercial
{
    public interface ICodeudoresNegocio
    {
        // Este método consulta y devuelve todos los Codeudores
        List<Codeudores> Consultar();
        // Este método guarda Codeudores nuevos en la base de datos.
        Codeudores Guardar(Codeudores entidad);
        // Este método modifica un Codeudores existentes en la base de datos.
        Codeudores Modificar(Codeudores entidad);
        // Este método borra Codeudores existentes en la base de datos.
        Codeudores Borrar(Codeudores entidad);


    }
}
