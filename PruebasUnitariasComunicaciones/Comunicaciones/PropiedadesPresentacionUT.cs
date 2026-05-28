using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class PropiedadesPresentacionUT
    {
        private IPropiedadesNegocio? iPropiedadesNegocio;
        private Propiedades? entidad;
        private EstadosCiviles? estadoCivilCliente;
        private Nacionalidades? nacionalidadCliente;
        private Clientes? cliente;
        private TiposPropiedades? tipoPropiedad;

        [TestMethod]
        public void Ejecutar()
        {
            this.iPropiedadesNegocio = new PropiedadesNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilCliente = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-ClienteRelacionado-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadCliente = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-ClienteRelacionado-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.cliente = new ClientesNegocio().Guardar(new Clientes()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "ClienteRelacionado UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "clienterel" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilCliente!.Id,
                Nacionalidad = this.nacionalidadCliente!.Id,
                PorcentajeComision = 3m,
                CantidadContratos = 1,
                PrioridadCliente = "Alta",
                MotivoVenta = "Prueba"
            });

            this.tipoPropiedad = new TiposPropiedadesNegocio().Guardar(new TiposPropiedades()
            {
                Nombre = "UT-TipoPropiedad-Propiedad-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.entidad = new Propiedades()
            {
                NumeroHabitaciones = 3,
                NumeroBanos = 2,
                Patio = true,
                Entradas = 1,
                Pisos = 1,
                AnioConstruccion = new DateTime(2018, 1, 1),
                ValorPropiedad = 250000000m,
                ValorArriendo = 1500000m,
                Estado = "Disponible",
                Cliente = this.cliente.Id,
                TipoPropiedad = this.tipoPropiedad.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iPropiedadesNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Propiedades a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iPropiedadesNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Propiedades guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Estado = "Ocupada";
            this.entidad.ValorArriendo = 1600000m;

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iPropiedadesNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Estado == "Ocupada")
                return;

            throw new Exception("Error al Modificar Propiedades a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iPropiedadesNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Propiedades a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new TiposPropiedadesNegocio().Borrar(this.tipoPropiedad!);
            new ClientesNegocio().Borrar(this.cliente!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadCliente!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilCliente!);
        }
    }
}
