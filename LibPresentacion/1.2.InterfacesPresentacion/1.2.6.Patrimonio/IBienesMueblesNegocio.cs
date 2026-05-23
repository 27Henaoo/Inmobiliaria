
using LibModelos._5._1ModelosComunes;

namespace LibPresentacion._1._2.InterfacesPresentacion._1._2._6.Patrimonio
{
    public interface IBienesMueblesNegocio
    {
        // Este método consulta y devuelve todos los BienesMuebles.
        List<BienesMuebles> Consultar();

        // Este método guarda BienesMuebles nuevos.
        BienesMuebles Guardar(BienesMuebles entidad);

        // Este método modifica BienesMuebles existentes.
        BienesMuebles Modificar(BienesMuebles entidad);

        // Este método borra BienesMuebles existentes.
        BienesMuebles Borrar(BienesMuebles entidad);
    }
}
