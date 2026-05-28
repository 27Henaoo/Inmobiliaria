using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._5.Financiero;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class ExpedientesFinancierosPresentacionUT
    {
        private IExpedientesFinancierosNegocio? iExpedientesFinancierosNegocio;
        private ExpedientesFinancieros? entidad;
        private EstadosCiviles? estadoCivilCliente;
        private Nacionalidades? nacionalidadCliente;
        private Clientes? cliente;

        [TestMethod]
        public void Ejecutar()
        {
            this.iExpedientesFinancierosNegocio = new ExpedientesFinancierosNegocio();

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

            this.entidad = new ExpedientesFinancieros()
            {
                Persona = this.cliente.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iExpedientesFinancierosNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar ExpedientesFinancieros a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iExpedientesFinancierosNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad ExpedientesFinancieros guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            // ExpedientesFinancieros solo tiene la FK Persona como dato editable simple.
            this.entidad!.Persona = this.cliente!.Id;

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iExpedientesFinancierosNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Persona == this.cliente!.Id)
                return;

            throw new Exception("Error al Modificar ExpedientesFinancieros a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iExpedientesFinancierosNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar ExpedientesFinancieros a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new ClientesNegocio().Borrar(this.cliente!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadCliente!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilCliente!);
        }
    }
}
