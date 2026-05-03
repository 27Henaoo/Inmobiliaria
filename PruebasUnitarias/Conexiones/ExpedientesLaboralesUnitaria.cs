using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class ExpedientesLaboralesUnitaria
    {
        private IConexion? iConexion;
        private ExpedientesLaborales? entidad;

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
            var lista = iConexion.ExpedientesLaborales!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new ExpedientesLaborales()
            {
                NombreEmpresa = "Empresa UT",
                Cargo = "Analista",
                SalarioPagado = 2000000,
                FechaIngreso = new DateTime(2022, 3, 4),
                FechaEgreso = new DateTime(2023, 5, 6),
                Desempeno = "Bueno",
                MotivoSalida = "Prueba",
                Persona = 1,
            };
            this.iConexion.ExpedientesLaborales!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Cargo = "Coordinador" + DateTime.Now.AddYears(1).ToString();

            var entry = this.iConexion!.Entry<ExpedientesLaborales>(this.entidad!);
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

            this.iConexion.ExpedientesLaborales!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
