using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
using PruebasUnitarias.Nucleo;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class PropiedadesUnitaria
    {
        private IConexion? iConexion;
        private Propiedades? entidad;

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
            var lista = iConexion.Propiedades!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Propiedades()
            {
                NumeroHabitaciones = 2,
                NumeroBanos = 1,
                Patio = true,
                Entradas = 1,
                Pisos = 1,
                AnioConstruccion = new DateTime(2023, 4, 5),
                ValorPropiedad = 300000000,
                ValorArriendo = 1500000,
                Estado = "Disponible",
                Cliente = 16,
                TipoPropiedad = 1,
            };
            this.iConexion.Propiedades!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Estado = "Reservada";

            var entry = this.iConexion!.Entry<Propiedades>(this.entidad!);
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

            this.iConexion.Propiedades!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
