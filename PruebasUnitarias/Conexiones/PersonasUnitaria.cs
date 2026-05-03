using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
using PruebasUnitarias.Nucleo;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class PersonasUnitaria
    {
        private IConexion? iConexion;
        private Personas? entidad;

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
            var lista = iConexion.Personas!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Personas()
            {
                Cedula = "UT-" + DateTime.Now.ToString(),
                Nombre = "Juan",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "UT Personas" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(2011, 5, 6),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
            };
            this.iConexion.Personas!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Nombre = "Juan Modificado" + DateTime.Now.ToString();

            var entry = this.iConexion!.Entry<Personas>(this.entidad!);
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

            this.iConexion.Personas!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
