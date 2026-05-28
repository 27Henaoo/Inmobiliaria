using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class CiudadesPresentacionUT
    {
        private ICiudadesNegocio? iCiudadesNegocio;
        private Ciudades? entidad;
        private Departamentos? departamento;

        [TestMethod]
        public void Ejecutar()
        {
            this.iCiudadesNegocio = new CiudadesNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crea primero el departamento requerido por la ciudad.
            this.departamento = new DepartamentosNegocio().Guardar(new Departamentos()
            {
                Nombre = "UT-Departamento-Ciudad-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "100000"
            });

            this.entidad = new Ciudades()
            {
                Nombre = "UT-Ciudad-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                Poblacion = "50000",
                FechaCreacion = DateTime.Now,
                CodigoPostal = "050001",
                Departamento = this.departamento.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iCiudadesNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Ciudades a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iCiudadesNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Ciudades guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Estado = false;
            this.entidad.CodigoPostal = "050002";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iCiudadesNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.CodigoPostal == "050002")
                return;

            throw new Exception("Error al Modificar Ciudades a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iCiudadesNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Ciudades a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new DepartamentosNegocio().Borrar(this.departamento!);
        }
    }
}
