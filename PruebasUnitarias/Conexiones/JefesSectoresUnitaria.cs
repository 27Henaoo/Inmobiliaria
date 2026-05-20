using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class JefesSectoresUnitaria
    {
        private IConexion? iConexion;
        private JefesSectores? entidad;
        private Departamentos? departamento;
        private Ciudades? ciudad;
        private Sectores? sector;
        private AdministradoresDepartamentos? administrador;

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
            var lista = iConexion.JefesSectores!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.departamento = new Departamentos()
            {
                Nombre = "UT-DEP-" + DateTime.Now.ToString(),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "80000",
            };
            this.iConexion.Departamentos!.Add(this.departamento);
            this.iConexion.SaveChanges();

            this.ciudad = new Ciudades()
            {
                Nombre = "UT-CIU-" + DateTime.Now.ToString(),
                Estado = true,
                Poblacion = "60000",
                FechaCreacion = DateTime.Now,
                CodigoPostal = "700001",
                Departamento = this.departamento!.Id,
            };
            this.iConexion.Ciudades!.Add(this.ciudad);
            this.iConexion.SaveChanges();

            this.sector = new Sectores()
            {
                Nombre = "UT-SEC-" + DateTime.Now.ToString(),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Ciudad = this.ciudad!.Id,
            };
            this.iConexion.Sectores!.Add(this.sector);
            this.iConexion.SaveChanges();

            this.administrador = new AdministradoresDepartamentos()
            {
                Cedula = "UTA-" + DateTime.Now.ToString(),
                Nombre = "Admin",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "Admin" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(1988, 1, 1),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
                Sueldo = 5000000,
                Estado = true,
                Jornada = "Diurna",
                PresupuestoDepartamento = 13000000,
                Departamento = this.departamento!.Id,
            };
            this.iConexion.AdministradoresDepartamentos!.Add(this.administrador);
            this.iConexion.SaveChanges();

            this.entidad = new JefesSectores()
            {
                Cedula = "UT-" + DateTime.Now.ToString(),
                Nombre = "Richard",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "UT JEFESECTORES" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(2000, 1, 3),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
                Sueldo = 4200000,
                Estado = true,
                Jornada = "Mixta",
                PresupuestoSector = 7000000,
                AdministradorDepartamento = this.administrador!.Id,
                Sector = this.sector!.Id,
            };
            this.iConexion.JefesSectores!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.PresupuestoSector = 99999999;

            var entry = this.iConexion!.Entry<JefesSectores>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.iConexion.JefesSectores!.Remove(this.entidad!);
            this.iConexion.SaveChanges();

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
            this.iConexion.AdministradoresDepartamentos!.Remove(this.administrador!);
            this.iConexion.SaveChanges();

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
            this.iConexion.Sectores!.Remove(this.sector!);
            this.iConexion.SaveChanges();

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
            this.iConexion.Ciudades!.Remove(this.ciudad!);
            this.iConexion.SaveChanges();

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
            this.iConexion.Departamentos!.Remove(this.departamento!);
            this.iConexion.SaveChanges();
        }
    }
}
