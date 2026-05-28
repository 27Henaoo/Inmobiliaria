using LibModelos._5._2LoginRegisterEntidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._8.Seguridad;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class RolesPresentacionUT
    {
        private IRolesNegocio? iRolesNegocio;
        private Roles? entidad;


        [TestMethod]
        public void Ejecutar()
        {
            this.iRolesNegocio = new RolesNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se prepara la entidad de prueba.
            this.entidad = new Roles()
            {
                Nombre = "UT-Rol-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iRolesNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Roles a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iRolesNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Roles guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Nombre = "UT-Rol-Modificado-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iRolesNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Nombre == this.entidad!.Nombre)
                return;

            throw new Exception("Error al Modificar Roles a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iRolesNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Roles a traves de la API.");

            // No tiene datos relacionados creados dentro de esta prueba.
        }
    }
}
