using LibModelos._5._1ModelosComunes;


namespace LibInmobiliaria.Interfaces.Patrimonio
{
    public interface IBienesInmueblesNegocio
    {
        // Este método consulta y devuelve todos los BienesInmuebles
        List<BienesInmuebles> Consultar();
        // Este método guarda BienesInmuebles nuevos en la base de datos.
        BienesInmuebles Guardar(BienesInmuebles entidad);
        // Este método modifica un BienesInmuebles existentes en la base de datos.
        BienesInmuebles Modificar(BienesInmuebles entidad);
        // Este método borra BienesInmuebles existentes en la base de datos.
        BienesInmuebles Borrar(BienesInmuebles entidad);


    }
}
