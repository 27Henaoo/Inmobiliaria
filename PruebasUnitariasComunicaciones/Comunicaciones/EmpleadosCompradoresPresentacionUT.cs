using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class EmpleadosCompradoresPresentacionUT
    {
        private IEmpleadosCompradoresNegocio? iEmpleadosCompradoresNegocio;
        private EmpleadosCompradores? entidad;
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
        private EstadosCiviles? estadoCivilEmpleado;
        private Nacionalidades? nacionalidadEmpleado;
        private JefesSectores? jefeSector;
        private TiposContratos? tipoContrato;
        private EmpleadosSectores? empleado;

        [TestMethod]
        public void Ejecutar()
        {
            this.iEmpleadosCompradoresNegocio = new EmpleadosCompradoresNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
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
                Nombre = "JefeEmpleado UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "jefeempleado" + Guid.NewGuid().ToString("N") + "@correo.com",
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

            this.tipoContrato = new TiposContratosNegocio().Guardar(new TiposContratos()
            {
                Nombre = "UT-TipoContrato-Empleado-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilEmpleado = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-Empleado-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadEmpleado = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Empleado-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.empleado = new EmpleadosSectoresNegocio().Guardar(new EmpleadosSectores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "EmpleadoComprador UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "empleadocomprador" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilEmpleado!.Id,
                Nacionalidad = this.nacionalidadEmpleado!.Id,
                Sueldo = 2200000m,
                Estado = true,
                Jornada = "Diurna",
                JefeSector = this.jefeSector.Id,
                TipoContrato = this.tipoContrato.Id,
                Sector = this.sector.Id
            });

            this.entidad = new EmpleadosCompradores()
            {
                FechaAsesoramiento = DateTime.Now,
                Comprador = this.comprador.Id,
                Empleado = this.empleado.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iEmpleadosCompradoresNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar EmpleadosCompradores a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iEmpleadosCompradoresNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad EmpleadosCompradores guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.FechaAsesoramiento = DateTime.Now.AddDays(1);

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iEmpleadosCompradoresNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Id == this.entidad!.Id)
                return;

            throw new Exception("Error al Modificar EmpleadosCompradores a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iEmpleadosCompradoresNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar EmpleadosCompradores a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new EmpleadosSectoresNegocio().Borrar(this.empleado!);
            new TiposContratosNegocio().Borrar(this.tipoContrato!);
            new JefesSectoresNegocio().Borrar(this.jefeSector!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadEmpleado!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilEmpleado!);
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
        }
    }
}
