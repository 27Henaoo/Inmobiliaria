using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class ExpedientesFinancierosUnitaria
    {
        private IConexion? iConexion;
        private ExpedientesFinancieros? entidad;
        private Clientes? cliente;

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
            var lista = iConexion.ExpedientesFinancieros!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.cliente = new Clientes()
            {
                Cedula = "UTC-" + DateTime.Now.ToString(),
                Nombre = "Cliente",
                Apellido = "Prueba",
                Genero = 'F',
                Correo = "clienteUT" + DateTime.Now.ToString() + "@correo.com",
                FechaNacimiento = new DateTime(2000, 1, 2),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
                PorcentajeComision = 2,
                CantidadContratos = 1,
                PrioridadCliente = "Media",
                MotivoVenta = "Prueba",
            };
            this.iConexion.Clientes!.Add(this.cliente);
            this.iConexion.SaveChanges();

            this.entidad = new ExpedientesFinancieros()
            {
                Persona = this.cliente!.Id,
            };
            this.iConexion.ExpedientesFinancieros!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Persona = this.cliente!.Id;

            var entry = this.iConexion!.Entry<ExpedientesFinancieros>(this.entidad!);
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

            this.iConexion.ExpedientesFinancieros!.Remove(this.entidad!);
            this.iConexion.SaveChanges();

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");
            this.iConexion.Clientes!.Remove(this.cliente!);
            this.iConexion.SaveChanges();
        }
    }
}
