using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
using PruebasUnitarias.Nucleo;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class BienesInmueblesUnitaria
    {
        private IConexion? iConexion;
        private BienesInmuebles? entidad;

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
            var lista = iConexion.BienesInmuebles!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new BienesInmuebles()
            {
                Nombre = "UT-" + DateTime.Now.ToString(),
                Descripcion = "Bien inmueble prueba",
                FechaAdquisicion = DateTime.Now,
                PrecioCompra = 500000,
                ValorActual = 700000,
                ExpedienteFinanciero = 1,
                MetrosCuadrados = 80,
                Direccion = "UT Direccion",
                EstadoConservacion = "Bueno",
                Estrato = "3",
                NumeroHabitaciones = "3",
                NumeroBanos = "2",
                CodigoCUC = "CUC-UT",
                EncargosDeudas = "Ninguna",
            };
            this.iConexion.BienesInmuebles!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.EstadoConservacion = "Excelente";

            var entry = this.iConexion!.Entry<BienesInmuebles>(this.entidad!);
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

            this.iConexion.BienesInmuebles!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
