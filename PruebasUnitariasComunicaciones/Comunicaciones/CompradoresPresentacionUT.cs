using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class CompradoresPresentacionUT
    {
        private ICompradoresNegocio? iCompradoresNegocio;
        private Compradores? entidad;
        private EstadosCiviles? estadoCivilComprador;
        private Nacionalidades? nacionalidadComprador;

        [TestMethod]
        public void Ejecutar()
        {
            this.iCompradoresNegocio = new CompradoresNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilComprador = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-Comprador-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadComprador = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Comprador-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.entidad = new Compradores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "Comprador UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "comprador" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilComprador!.Id,
                Nacionalidad = this.nacionalidadComprador!.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iCompradoresNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar Compradores a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iCompradoresNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad Compradores guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Nombre = "Comprador UT Modificado";
            this.entidad.Correo = "comprador" + Guid.NewGuid().ToString("N") + "@correo.com";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iCompradoresNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Nombre == "Comprador UT Modificado")
                return;

            throw new Exception("Error al Modificar Compradores a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iCompradoresNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar Compradores a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new NacionalidadesNegocio().Borrar(this.nacionalidadComprador!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilComprador!);
        }
    }
}
