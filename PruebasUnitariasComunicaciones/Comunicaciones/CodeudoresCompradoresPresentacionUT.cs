using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class CodeudoresCompradoresPresentacionUT
    {
        private ICodeudoresCompradoresNegocio? iCodeudoresCompradoresNegocio;
        private CodeudoresCompradores? entidad;
        private EstadosCiviles? estadoCivilComprador;
        private Nacionalidades? nacionalidadComprador;
        private Compradores? comprador;
        private EstadosCiviles? estadoCivilCodeudor;
        private Nacionalidades? nacionalidadCodeudor;
        private Codeudores? codeudor;

        [TestMethod]
        public void Ejecutar()
        {
            this.iCodeudoresCompradoresNegocio = new CodeudoresCompradoresNegocio();

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
                Nombre = "UT-EstadoCivil-CompradorContrato-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadComprador = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-CompradorContrato-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.comprador = new CompradoresNegocio().Guardar(new Compradores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "CompradorContrato UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "compradorcontrato" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilComprador!.Id,
                Nacionalidad = this.nacionalidadComprador!.Id
            });

            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilCodeudor = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-CodeudorComprador-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadCodeudor = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-CodeudorComprador-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.codeudor = new CodeudoresNegocio().Guardar(new Codeudores()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "CodeudorComprador UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "codeudorcomprador" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilCodeudor!.Id,
                Nacionalidad = this.nacionalidadCodeudor!.Id
            });

            this.entidad = new CodeudoresCompradores()
            {
                FechaUnion = DateTime.Now,
                Relacion = "Familiar",
                Comprador = this.comprador.Id,
                Codeudor = this.codeudor.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iCodeudoresCompradoresNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar CodeudoresCompradores a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iCodeudoresCompradoresNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad CodeudoresCompradores guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.Relacion = "Laboral";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iCodeudoresCompradoresNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.Relacion == "Laboral")
                return;

            throw new Exception("Error al Modificar CodeudoresCompradores a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iCodeudoresCompradoresNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar CodeudoresCompradores a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new CodeudoresNegocio().Borrar(this.codeudor!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadCodeudor!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilCodeudor!);
            new CompradoresNegocio().Borrar(this.comprador!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadComprador!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilComprador!);
        }
    }
}
