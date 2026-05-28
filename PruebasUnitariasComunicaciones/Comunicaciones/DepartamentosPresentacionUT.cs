using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class DepartamentosPresentacionUT
    {
        private IDepartamentosNegocio? iDepartamentosNegocio;
        private Departamentos? entidad;


        [TestMethod]
        public void Ejecutar()
        {
            this.iDepartamentosNegocio = new DepartamentosNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se prepara la entidad de prueba.
            this.entidad = new Departamentos()
            {
                Nombre = "UT-Departamento-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "100000"
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iDepartamentosNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Departamentos a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iDepartamentosNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Departamentos guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Estado = false;
            this.entidad.Poblacion = "120000";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iDepartamentosNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Estado == false)
                return;

            throw new Exception("Error al Modificar Departamentos a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iDepartamentosNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Departamentos a traves de la API.");

            // No tiene datos relacionados creados dentro de esta prueba.
        }
    }
}
