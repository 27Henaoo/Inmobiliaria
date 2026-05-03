using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class BienesUnitaria
    {
        private IConexion? iConexion;
        private Bienes? entidad;

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
            var lista = iConexion.Bienes!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Bienes()
            {
                Nombre = "UT-" + DateTime.Now.ToString(),
                Descripcion = "Bien de prueba",
                FechaAdquisicion = DateTime.Now,
                PrecioCompra = 100000,
                ValorActual = 120000,
                ExpedienteFinanciero = 1,
            };
            this.iConexion.Bienes!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Descripcion = "Cambio -UT" + DateTime.Now.AddMonths(12);

            var entry = this.iConexion!.Entry<Bienes>(this.entidad!);
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

            this.iConexion.Bienes!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
