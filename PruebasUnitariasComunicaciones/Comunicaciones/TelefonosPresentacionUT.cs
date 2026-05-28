using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._2.Sujetos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class TelefonosPresentacionUT
    {
        private ITelefonosNegocio? iTelefonosNegocio;
        private Telefonos? entidad;
        private EstadosCiviles? estadoCivilPersona;
        private Nacionalidades? nacionalidadPersona;
        private Personas? persona;

        [TestMethod]
        public void Ejecutar()
        {
            this.iTelefonosNegocio = new TelefonosNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilPersona = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-PersonaRelacionada-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadPersona = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-PersonaRelacionada-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.persona = new PersonasNegocio().Guardar(new Personas()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "PersonaRelacionada UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "personarel" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilPersona!.Id,
                Nacionalidad = this.nacionalidadPersona!.Id
            });

            this.entidad = new Telefonos()
            {
                Numero = "300" + DateTime.Now.ToString("HHmmssfff"),
                Prefijo = "+57",
                Persona = this.persona.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iTelefonosNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Telefonos a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iTelefonosNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Telefonos guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Prefijo = "+01";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iTelefonosNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Prefijo == "+01")
                return;

            throw new Exception("Error al Modificar Telefonos a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iTelefonosNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Telefonos a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new PersonasNegocio().Borrar(this.persona!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadPersona!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilPersona!);
        }
    }
}
