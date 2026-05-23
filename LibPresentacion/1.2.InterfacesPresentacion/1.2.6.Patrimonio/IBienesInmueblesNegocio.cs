using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._6.Patrimonio
{
    public interface IBienesInmueblesNegocio
    {
        // Este método consulta y devuelve todos los BienesInmuebles.
        List<BienesInmuebles> Consultar();

        // Este método guarda BienesInmuebles nuevos.
        BienesInmuebles Guardar(BienesInmuebles entidad);

        // Este método modifica BienesInmuebles existentes.
        BienesInmuebles Modificar(BienesInmuebles entidad);

        // Este método borra BienesInmuebles existentes.
        BienesInmuebles Borrar(BienesInmuebles entidad);
    }
}
