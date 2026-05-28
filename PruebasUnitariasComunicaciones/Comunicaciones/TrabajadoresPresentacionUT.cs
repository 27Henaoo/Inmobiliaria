using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class TrabajadoresPresentacionUT
    {
        private ITrabajadoresNegocio? iTrabajadoresNegocio;
        private Trabajadores? entidad;
        private EstadosCiviles? estadoCivilTrabajador;
        private Nacionalidades? nacionalidadTrabajador;

        [TestMethod]
        public void Ejecutar()
        {
            this.iTrabajadoresNegocio = new TrabajadoresNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilTrabajador = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-Trabajador-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadTrabajador = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Trabajador-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.entidad = new Trabajadores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "Trabajador UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "trabajador" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilTrabajador!.Id,
                Nacionalidad = this.nacionalidadTrabajador!.Id,
                Sueldo = 2500000m,
                Estado = true,
                Jornada = "Diurna"
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iTrabajadoresNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Trabajadores a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iTrabajadoresNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Trabajadores guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Jornada = "Mixta";
            this.entidad.Sueldo = 2600000m;

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iTrabajadoresNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Jornada == "Mixta")
                return;

            throw new Exception("Error al Modificar Trabajadores a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iTrabajadoresNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Trabajadores a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new NacionalidadesNegocio().Borrar(this.nacionalidadTrabajador!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilTrabajador!);
        }
    }
}
