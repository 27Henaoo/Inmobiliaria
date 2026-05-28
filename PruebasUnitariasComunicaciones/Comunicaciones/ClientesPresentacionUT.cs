using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class ClientesPresentacionUT
    {
        private IClientesNegocio? iClientesNegocio;
        private Clientes? entidad;
        private EstadosCiviles? estadoCivilCliente;
        private Nacionalidades? nacionalidadCliente;

        [TestMethod]
        public void Ejecutar()
        {
            this.iClientesNegocio = new ClientesNegocio();

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
                Nombre = "UT-EstadoCivil-Cliente-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadCliente = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Cliente-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.entidad = new Clientes()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "Cliente UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "cliente" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilCliente!.Id,
                Nacionalidad = this.nacionalidadCliente!.Id,
                PorcentajeComision = 3m,
                CantidadContratos = 1,
                PrioridadCliente = "Alta",
                MotivoVenta = "Prueba"
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iClientesNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Clientes a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iClientesNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Clientes guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.CantidadContratos = 2;
            this.entidad.PrioridadCliente = "Media";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iClientesNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.CantidadContratos == 2)
                return;

            throw new Exception("Error al Modificar Clientes a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iClientesNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Clientes a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new NacionalidadesNegocio().Borrar(this.nacionalidadCliente!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilCliente!);
        }
    }
}
