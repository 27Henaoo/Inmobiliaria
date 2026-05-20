using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace PruebasUnitarias.Repositorios
{

    [TestClass]
    public class CodeudoresCompradoresUnitaria
    {
        private IConexion? iConexion;
        private CodeudoresCompradores? entidad;

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
            var lista = iConexion.CodeudoresCompradores!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad = new CodeudoresCompradores()
            {
                FechaUnion = DateTime.Now,
                Relacion = "Hermano" + DateTime.Now.ToString(),
                Comprador = 26,
                Codeudor = 21,
            };
            this.iConexion.CodeudoresCompradores!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.Id != 0)
                return;
            throw new Exception("");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            this.entidad!.Relacion = "Amigo" + DateTime.Now.AddMonths(12).ToString();

            var entry = this.iConexion!.Entry<CodeudoresCompradores>(this.entidad!);
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

            this.iConexion.CodeudoresCompradores!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
