using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
using PruebasUnitarias.Nucleo;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class EmpleadosSectoresUnitaria
    {
        private IConexion? iConexion;
        private EmpleadosSectores? entidad;

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
            var lista = iConexion.EmpleadosSectores!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new EmpleadosSectores()
            {
                Cedula = "UT-" + DateTime.Now.ToString(),
                Nombre = "Carlos",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "UT" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(2010, 11, 12),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
                Sueldo = 2500000,
                Estado = true,
                Jornada = "Diurna",
                JefeSector = 6,
                TipoContrato = 1,
                Sector = 1,
            };
            this.iConexion.EmpleadosSectores!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Jornada = "Nocturna" + DateTime.Now.AddYears(1).ToString();

            var entry = this.iConexion!.Entry<EmpleadosSectores>(this.entidad!);
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

            this.iConexion.EmpleadosSectores!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
