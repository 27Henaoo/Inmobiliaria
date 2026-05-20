using LibModelos._5._1ModelosComunes;


namespace LibInmobiliaria.Interfaces.Patrimonio
{
    public interface IBienesNegocio
    {
        // Este método consulta y devuelve todos los Bienes
        List<Bienes> Consultar();
        // Este método guarda Bienes nuevos en la base de datos.
        Bienes Guardar(Bienes entidad);
        // Este método modifica un Bienes existentes en la base de datos.
        Bienes Modificar(Bienes entidad);
        // Este método borra Bienes existentes en la base de datos.
        Bienes Borrar(Bienes entidad);


    }
}
