using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._5.Financiero;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._6.Patrimonio;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._6.Patrimonio;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class BienesPresentacionUT
    {
        private IBienesNegocio? iBienesNegocio;
        private Bienes? entidad;
        private EstadosCiviles? estadoCivilCliente;
        private Nacionalidades? nacionalidadCliente;
        private Clientes? cliente;
        private ExpedientesFinancieros? expedienteFinanciero;

        [TestMethod]
        public void Ejecutar()
        {
            this.iBienesNegocio = new BienesNegocio();

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

            this.expedienteFinanciero = new ExpedientesFinancierosNegocio().Guardar(new ExpedientesFinancieros()
            {
                Persona = this.cliente.Id
            });

            this.entidad = new Bienes()
            {
                Nombre = "UT-Bien-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Descripcion = "Bien de prueba",
                FechaAdquisicion = DateTime.Now,
                PrecioCompra = 500000m,
                ValorActual = 700000m,
                ExpedienteFinanciero = this.expedienteFinanciero.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iBienesNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Bienes a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iBienesNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Bienes guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.ValorActual = 750000m;
            this.entidad.Descripcion = "Bien modificado";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iBienesNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.ValorActual == 750000m)
                return;

            throw new Exception("Error al Modificar Bienes a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iBienesNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Bienes a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new ExpedientesFinancierosNegocio().Borrar(this.expedienteFinanciero!);
            new ClientesNegocio().Borrar(this.cliente!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadCliente!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilCliente!);
        }
    }
}
