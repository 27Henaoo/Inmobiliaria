using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class ContratosPresentacionUT
    {
        private IContratosNegocio? iContratosNegocio;
        private Contratos? entidad;
        private EstadosCiviles? estadoCivilCliente;
        private Nacionalidades? nacionalidadCliente;
        private Clientes? cliente;
        private TiposPropiedades? tipoPropiedad;
        private Propiedades? propiedad;
        private EstadosCiviles? estadoCivilComprador;
        private Nacionalidades? nacionalidadComprador;
        private Compradores? comprador;
        private EstadosCiviles? estadoCivilJefe;
        private Nacionalidades? nacionalidadJefe;
        private EstadosCiviles? estadoCivilAdmin;
        private Nacionalidades? nacionalidadAdmin;
        private Departamentos? departamentoAdmin;
        private AdministradoresDepartamentos? administrador;
        private Departamentos? departamentoSector;
        private Ciudades? ciudadSector;
        private Sectores? sector;
        private JefesSectores? jefeSector;

        [TestMethod]
        public void Ejecutar()
        {
            this.iContratosNegocio = new ContratosNegocio();

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
                Nombre = "UT-TipoPropiedad-Contrato-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.propiedad = new PropiedadesNegocio().Guardar(new Propiedades()
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
            });

            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilComprador = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-CompradorContrato-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadComprador = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-CompradorContrato-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.comprador = new CompradoresNegocio().Guardar(new Compradores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "CompradorContrato UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "compradorcontrato" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilComprador!.Id,
                Nacionalidad = this.nacionalidadComprador!.Id
            });

            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilAdmin = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-AdminJefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadAdmin = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-AdminJefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.departamentoAdmin = new DepartamentosNegocio().Guardar(new Departamentos()
            {
                Nombre = "UT-Departamento-AdminJefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "100000"
            });

            this.administrador = new AdministradoresDepartamentosNegocio().Guardar(new AdministradoresDepartamentos()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "AdminJefe UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "adminjefe" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilAdmin!.Id,
                Nacionalidad = this.nacionalidadAdmin!.Id,
                Sueldo = 4000000m,
                Estado = true,
                Jornada = "Diurna",
                PresupuestoDepartamento = 9000000m,
                Departamento = this.departamentoAdmin.Id
            });

            this.departamentoSector = new DepartamentosNegocio().Guardar(new Departamentos()
            {
                Nombre = "UT-Departamento-SectorJefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "100000"
            });

            this.ciudadSector = new CiudadesNegocio().Guardar(new Ciudades()
            {
                Nombre = "UT-Ciudad-SectorJefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                Poblacion = "50000",
                FechaCreacion = DateTime.Now,
                CodigoPostal = "050001",
                Departamento = this.departamentoSector.Id
            });

            this.sector = new SectoresNegocio().Guardar(new Sectores()
            {
                Nombre = "UT-Sector-Jefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Ciudad = this.ciudadSector.Id
            });

            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilJefe = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-Jefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadJefe = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Jefe-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.jefeSector = new JefesSectoresNegocio().Guardar(new JefesSectores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "JefeContrato UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "jefecontrato" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilJefe!.Id,
                Nacionalidad = this.nacionalidadJefe!.Id,
                Sueldo = 3500000m,
                Estado = true,
                Jornada = "Diurna",
                PresupuestoSector = 5000000m,
                AdministradorDepartamento = this.administrador.Id,
                Sector = this.sector.Id
            });

            this.entidad = new Contratos()
            {
                FechaContrato = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddYears(1),
                Observaciones = "Contrato UT",
                PrecioAcordado = 250000000m,
                ArriendoVenta = "Venta",
                Cliente = this.cliente.Id,
                Propiedad = this.propiedad.Id,
                Comprador = this.comprador.Id,
                JefeSector = this.jefeSector.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iContratosNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Contratos a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iContratosNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Contratos guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Observaciones = "Contrato UT Modificado";
            this.entidad.PrecioAcordado = 260000000m;

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iContratosNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Observaciones == "Contrato UT Modificado")
                return;

            throw new Exception("Error al Modificar Contratos a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iContratosNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Contratos a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new PropiedadesNegocio().Borrar(this.propiedad!);
            new TiposPropiedadesNegocio().Borrar(this.tipoPropiedad!);
            new JefesSectoresNegocio().Borrar(this.jefeSector!);
            new SectoresNegocio().Borrar(this.sector!);
            new CiudadesNegocio().Borrar(this.ciudadSector!);
            new DepartamentosNegocio().Borrar(this.departamentoSector!);
            new AdministradoresDepartamentosNegocio().Borrar(this.administrador!);
            new DepartamentosNegocio().Borrar(this.departamentoAdmin!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadJefe!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilJefe!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadAdmin!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilAdmin!);
            new CompradoresNegocio().Borrar(this.comprador!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadComprador!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilComprador!);
            new ClientesNegocio().Borrar(this.cliente!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadCliente!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilCliente!);
        }
    }
}
