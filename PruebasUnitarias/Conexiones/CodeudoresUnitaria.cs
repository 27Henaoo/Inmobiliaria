using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class CodeudoresUnitaria
    {
        private IConexion? iConexion;
        private Codeudores? entidad;

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
            var lista = iConexion.Codeudores!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Codeudores()
            {
                Cedula = "UT-" + DateTime.Now.ToString(),
                Nombre = "Abelardo",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "UT CODEUDORES" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(1990, 1, 1),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
            };
            this.iConexion.Codeudores!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Nombre = "Codeudor Modificado";

            var entry = this.iConexion!.Entry<Codeudores>(this.entidad!);
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

            this.iConexion.Codeudores!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
