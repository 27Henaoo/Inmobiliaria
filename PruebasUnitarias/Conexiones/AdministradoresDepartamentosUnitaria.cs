using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class AdministradoresDepartamentosUnitaria
    {
        private IConexion? iConexion;
        private AdministradoresDepartamentos? entidad;
        private Departamentos? departamento;

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
            var lista = iConexion.AdministradoresDepartamentos!.ToList();
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
                Nombre = "UT-" + DateTime.Now.ToString(),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "90000",
            };
            this.iConexion.Departamentos!.Add(this.departamento);
            this.iConexion.SaveChanges();

            this.entidad = new AdministradoresDepartamentos()
            {
                Cedula = "UT-" + DateTime.Now.ToString(),
                Nombre = "Juan",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "ut" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(1990, 1, 1),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
                Sueldo = 4500000,
                Estado = true,
                Jornada = "Diurna",
                PresupuestoDepartamento = 10000000,
                Departamento = this.departamento!.Id,
            };
            this.iConexion.AdministradoresDepartamentos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.PresupuestoDepartamento = 11000000;

            var entry = this.iConexion!.Entry<AdministradoresDepartamentos>(this.entidad!);
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

            this.iConexion.AdministradoresDepartamentos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
            this.iConexion.Departamentos!.Remove(this.departamento!);
            this.iConexion.SaveChanges();
        }
    }
}
