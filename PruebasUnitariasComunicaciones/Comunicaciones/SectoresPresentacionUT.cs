using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class SectoresPresentacionUT
    {
        private ISectoresNegocio? iSectoresNegocio;
        private Sectores? entidad;
        private Departamentos? departamento;
        private Ciudades? ciudad;

        [TestMethod]
        public void Ejecutar()
        {
            this.iSectoresNegocio = new SectoresNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero departamento y ciudad requeridos por el sector.
            this.departamento = new DepartamentosNegocio().Guardar(new Departamentos()
            {
                Nombre = "UT-Departamento-Sector-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "100000"
            });

            this.ciudad = new CiudadesNegocio().Guardar(new Ciudades()
            {
                Nombre = "UT-Ciudad-Sector-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                Poblacion = "50000",
                FechaCreacion = DateTime.Now,
                CodigoPostal = "050001",
                Departamento = this.departamento.Id
            });

            this.entidad = new Sectores()
            {
                Nombre = "UT-Sector-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Ciudad = this.ciudad.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iSectoresNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Sectores a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iSectoresNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Sectores guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Estado = false;
            this.entidad.Nombre = "UT-Sector-Modificado-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iSectoresNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Estado == false)
                return;

            throw new Exception("Error al Modificar Sectores a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iSectoresNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Sectores a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new CiudadesNegocio().Borrar(this.ciudad!);
            new DepartamentosNegocio().Borrar(this.departamento!);
        }
    }
}
