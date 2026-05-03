using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
using PruebasUnitarias.Nucleo;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class ClientesUnitaria
    {
        private IConexion? iConexion;
        private Clientes? entidad;

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
            var lista = iConexion.Clientes!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new Clientes()
            {
                Cedula = "UT-" + DateTime.Now.ToString(""),
                Nombre = "Sebas",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "ut" + DateTime.Now.AddYears(1).ToString() + "@correo.com",
                FechaNacimiento = new DateTime(2004, 4, 19),
                FechaRegistro = DateTime.Now,
                EstadoCivil = 1,
                Nacionalidad = 1,
                PorcentajeComision = 3,
                CantidadContratos = 1,
                PrioridadCliente = "Alta",
                MotivoVenta = "Prueba",
            };
            this.iConexion.Clientes!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.CantidadContratos = 100;

            var entry = this.iConexion!.Entry<Clientes>(this.entidad!);
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

            this.iConexion.Clientes!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
