using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class DireccionesUnitaria
    {
        private IConexion? iConexion;
        private Direcciones? entidad;

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
            var lista = iConexion.Direcciones!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Direcciones()
            {
                TipoVia = "Calle UT",
                Numero = "UT-" + DateTime.Now.ToString(),
                Complemento = "Casa prueba",
                Persona = 1,
            };
            this.iConexion.Direcciones!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Complemento = "Casa modificada" + DateTime.Now.AddYears(1);

            var entry = this.iConexion!.Entry<Direcciones>(this.entidad!);
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

            this.iConexion.Direcciones!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
