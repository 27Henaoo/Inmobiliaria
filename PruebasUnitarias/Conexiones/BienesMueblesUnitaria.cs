using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class BienesMueblesUnitaria
    {
        private IConexion? iConexion;
        private BienesMuebles? entidad;

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
            var lista = iConexion.BienesMuebles!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new BienesMuebles()
            {
                Nombre = "UT-" + DateTime.Now.ToString(),
                Descripcion = "Bien mueble UT",
                FechaAdquisicion = DateTime.Now,
                PrecioCompra = 50000,
                ValorActual = 70000,
                ExpedienteFinanciero = 1,
                Tipo = "Tecnologico",
                Modelo = "UT-Modelo",
                UbicacionActual = "Bodega",
                Garantia = "1 ano",
                Estado = "Bueno",
            };
            this.iConexion.BienesMuebles!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Estado = "Regular";

            var entry = this.iConexion!.Entry<BienesMuebles>(this.entidad!);
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

            this.iConexion.BienesMuebles!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
