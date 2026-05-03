using LibInmobiliaria.Entidades;


namespace LibInmobiliaria.Interfaces.Patrimonio
{
    public interface IBienesMueblesNegocio
    {
        // Este método consulta y devuelve todos los BienesMuebles
        List<BienesMuebles> Consultar();
        // Este método guarda BienesMuebles nuevos en la base de datos.
        BienesMuebles Guardar(BienesMuebles entidad);
        // Este método modifica un BienesMuebles existentes en la base de datos.
        BienesMuebles Modificar(BienesMuebles entidad);
        // Este método borra BienesMuebles existentes en la base de datos.
        BienesMuebles Borrar(BienesMuebles entidad);


    }
}
