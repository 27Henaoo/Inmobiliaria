using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class EmpleadosSectoresPresentacionUT
    {
        private IEmpleadosSectoresNegocio? iEmpleadosSectoresNegocio;
        private EmpleadosSectores? entidad;
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

        [TestMethod]
        public void Ejecutar()
        {
            this.iEmpleadosSectoresNegocio = new EmpleadosSectoresNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
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

            this.entidad = new EmpleadosSectores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "Empleado UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "empleado" + Guid.NewGuid().ToString("N") + "@correo.com",
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
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iEmpleadosSectoresNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar EmpleadosSectores a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iEmpleadosSectoresNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad EmpleadosSectores guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Jornada = "Mixta";
            this.entidad.Sueldo = 2300000m;

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iEmpleadosSectoresNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Jornada == "Mixta")
                return;

            throw new Exception("Error al Modificar EmpleadosSectores a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iEmpleadosSectoresNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar EmpleadosSectores a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
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
        }
    }
}
