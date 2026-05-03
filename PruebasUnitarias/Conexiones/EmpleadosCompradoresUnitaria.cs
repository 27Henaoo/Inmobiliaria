using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
        public class EmpleadosCompradoresUnitaria
        {
            private IConexion? iConexion;
            private EmpleadosCompradores? entidad;
    
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
                var lista = iConexion.EmpleadosCompradores!.ToList();
                if (lista.Count > 0)
                    return;
                throw new Exception("");
            }
    
            private void Guardar()
            {
                this.iConexion = new Conexion();
                this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
    
                this.entidad = new EmpleadosCompradores()
                {
                    FechaAsesoramiento = DateTime.Now,
                    Comprador = 26,
                    Empleado = 11,
                };
                this.iConexion.EmpleadosCompradores!.Add(this.entidad!);
                this.iConexion.SaveChanges();
    
                if (this.entidad.Id != 0)
                    return;
                throw new Exception("");
            }
    
            private void Modificar()
            {
                this.iConexion = new Conexion();
                this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
    
                this.entidad!.Comprador = 27;
    
                var entry = this.iConexion!.Entry<EmpleadosCompradores>(this.entidad!);
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
    
                this.iConexion.EmpleadosCompradores!.Remove(this.entidad!);
                this.iConexion.SaveChanges();
            }
        }
}
