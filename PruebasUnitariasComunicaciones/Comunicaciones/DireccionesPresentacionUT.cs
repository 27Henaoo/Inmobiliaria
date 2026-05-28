using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._2.Sujetos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class DireccionesPresentacionUT
    {
        private IDireccionesNegocio? iDireccionesNegocio;
        private Direcciones? entidad;
        private EstadosCiviles? estadoCivilPersona;
        private Nacionalidades? nacionalidadPersona;
        private Personas? persona;

        [TestMethod]
        public void Ejecutar()
        {
            this.iDireccionesNegocio = new DireccionesNegocio();

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

            this.entidad = new Direcciones()
            {
                TipoVia = "Calle",
                Numero = "10-20",
                Complemento = "Apto UT",
                Persona = this.persona.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iDireccionesNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Direcciones a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iDireccionesNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Direcciones guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Complemento = "Apto UT Modificado";
            this.entidad.Numero = "30-40";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iDireccionesNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Complemento == "Apto UT Modificado")
                return;

            throw new Exception("Error al Modificar Direcciones a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iDireccionesNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Direcciones a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new PersonasNegocio().Borrar(this.persona!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadPersona!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilPersona!);
        }
    }
}
