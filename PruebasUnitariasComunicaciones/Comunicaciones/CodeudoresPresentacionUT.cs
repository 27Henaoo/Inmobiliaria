using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class CodeudoresPresentacionUT
    {
        private ICodeudoresNegocio? iCodeudoresNegocio;
        private Codeudores? entidad;
        private EstadosCiviles? estadoCivilCodeudor;
        private Nacionalidades? nacionalidadCodeudor;

        [TestMethod]
        public void Ejecutar()
        {
            this.iCodeudoresNegocio = new CodeudoresNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilCodeudor = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-Codeudor-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadCodeudor = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Codeudor-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.entidad = new Codeudores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "Codeudor UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "codeudor" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilCodeudor!.Id,
                Nacionalidad = this.nacionalidadCodeudor!.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iCodeudoresNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Codeudores a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iCodeudoresNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Codeudores guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Nombre = "Codeudor UT Modificado";
            this.entidad.Correo = "codeudor" + Guid.NewGuid().ToString("N") + "@correo.com";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iCodeudoresNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Nombre == "Codeudor UT Modificado")
                return;

            throw new Exception("Error al Modificar Codeudores a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iCodeudoresNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Codeudores a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new NacionalidadesNegocio().Borrar(this.nacionalidadCodeudor!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilCodeudor!);
        }
    }
}
