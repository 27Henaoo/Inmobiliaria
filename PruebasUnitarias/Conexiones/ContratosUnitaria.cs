using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
using PruebasUnitarias.Nucleo;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class ContratosUnitaria
    {
        private IConexion? iConexion;
        private Contratos? entidad;

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
            var lista = iConexion.Contratos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Contratos()
            {
                FechaContrato = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddMonths(12),
                Observaciones = "-UTContrato de prueba" + DateTime.Now.ToString(),
                PrecioAcordado = 1800000,
                ArriendoVenta = "Arriendo",
                Cliente = 16,
                Propiedad = 1,
                Comprador = 26,
                JefeSector = 6,
            };
            this.iConexion.Contratos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Observaciones = "Contrato modificado" + DateTime.Now.AddMonths(18);

            var entry = this.iConexion!.Entry<Contratos>(this.entidad!);
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

            this.iConexion.Contratos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
