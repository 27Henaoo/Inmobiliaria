using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class NacionalidadesPresentacionUT
    {
        private INacionalidadesNegocio? iNacionalidadesNegocio;
        private Nacionalidades? entidad;


        [TestMethod]
        public void Ejecutar()
        {
            this.iNacionalidadesNegocio = new NacionalidadesNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se prepara la entidad de prueba.
            this.entidad = new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iNacionalidadesNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Nacionalidades a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iNacionalidadesNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Nacionalidades guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Nombre = "UT-Nacionalidad-Modificada-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iNacionalidadesNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Nombre == this.entidad!.Nombre)
                return;

            throw new Exception("Error al Modificar Nacionalidades a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iNacionalidadesNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Nacionalidades a traves de la API.");

            // No tiene datos relacionados creados dentro de esta prueba.
        }
    }
}
