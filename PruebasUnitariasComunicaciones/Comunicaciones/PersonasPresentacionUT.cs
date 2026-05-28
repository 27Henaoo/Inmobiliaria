using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._2.Sujetos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class PersonasPresentacionUT
    {
        private IPersonasNegocio? iPersonasNegocio;
        private Personas? entidad;
        private EstadosCiviles? estadoCivil;
        private Nacionalidades? nacionalidad;

        [TestMethod]
        public void Ejecutar()
        {
            this.iPersonasNegocio = new PersonasNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivil = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-Persona-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidad = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Persona-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.entidad = new Personas()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "Persona UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "persona" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivil!.Id,
                Nacionalidad = this.nacionalidad!.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iPersonasNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Personas a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iPersonasNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Personas guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Nombre = "Persona UT Modificada";
            this.entidad.Correo = "persona" + Guid.NewGuid().ToString("N") + "@correo.com";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iPersonasNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Nombre == "Persona UT Modificada")
                return;

            throw new Exception("Error al Modificar Personas a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iPersonasNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Personas a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new NacionalidadesNegocio().Borrar(this.nacionalidad!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivil!);
        }
    }
}
