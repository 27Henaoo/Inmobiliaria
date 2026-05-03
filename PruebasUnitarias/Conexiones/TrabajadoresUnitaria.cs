using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class TrabajadoresUnitaria
    {
        private IConexion? iConexion;
        private Trabajadores? entidad;

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
            var lista = iConexion.Trabajadores!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Trabajadores()
            {
                Cedula = "UT-" + DateTime.Now.ToString(),
                Nombre = "Jean Carlos",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "ut" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(1942, 3, 4),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
                Sueldo = 3000000,
                Estado = true,
                Jornada = "Diurna",
            };
            this.iConexion.Trabajadores!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Jornada = "MODIFICADA MIXTA" + DateTime.Now.AddYears(1).ToString();

            var entry = this.iConexion!.Entry<Trabajadores>(this.entidad!);
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

            this.iConexion.Trabajadores!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
