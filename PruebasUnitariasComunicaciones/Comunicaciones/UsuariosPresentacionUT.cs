using LibModelos._5._2LoginRegisterEntidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._8.Seguridad;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class UsuariosPresentacionUT
    {
        private IUsuariosNegocio? iUsuariosNegocio;
        private Usuarios? entidad;
        private Roles? rol;

        [TestMethod]
        public void Ejecutar()
        {
            this.iUsuariosNegocio = new UsuariosNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crea primero el rol requerido por el usuario.
            this.rol = new RolesNegocio().Guardar(new Roles()
            {
                Nombre = "UT-Rol-Usuario-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.entidad = new Usuarios()
            {
                Nombre = "Usuario UT",
                Correo = "usuario" + Guid.NewGuid().ToString("N") + "@correo.com",
                ClaveHash = "hash-prueba",
                ClaveSalt = "salt-prueba",
                Rol = this.rol.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iUsuariosNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Usuarios a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iUsuariosNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Usuarios guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Nombre = "Usuario UT Modificado";
            this.entidad.Correo = "usuario" + Guid.NewGuid().ToString("N") + "@correo.com";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iUsuariosNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Nombre == "Usuario UT Modificado")
                return;

            throw new Exception("Error al Modificar Usuarios a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iUsuariosNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Usuarios a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new RolesNegocio().Borrar(this.rol!);
        }
    }
}
