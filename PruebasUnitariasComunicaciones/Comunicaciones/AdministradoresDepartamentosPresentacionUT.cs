using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;

namespace PruebasUnitariasComunicaciones
{
    [TestClass]
    public class AdministradoresDepartamentosPresentacionUT
    {
        private IAdministradoresDepartamentosNegocio? iAdministradoresDepartamentosNegocio;
        private AdministradoresDepartamentos? entidad;
        private EstadosCiviles? estadoCivilAdmin;
        private Nacionalidades? nacionalidadAdmin;
        private Departamentos? departamentoAdmin;

        [TestMethod]
        public void Ejecutar()
        {
            this.iAdministradoresDepartamentosNegocio = new AdministradoresDepartamentosNegocio();

            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Guardar()
        {
            // Se crean primero los catalogos requeridos por la persona.
            this.estadoCivilAdmin = new EstadosCivilesNegocio().Guardar(new EstadosCiviles()
            {
                Nombre = "UT-EstadoCivil-Admin-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.nacionalidadAdmin = new NacionalidadesNegocio().Guardar(new Nacionalidades()
            {
                Nombre = "UT-Nacionalidad-Admin-" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
            });

            this.departamentoAdmin = new DepartamentosNegocio().Guardar(new Departamentos()
            {
                Nombre = "UT-Departamento-Admin-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Estado = true,
                FechaCreacion = DateTime.Now,
                Poblacion = "100000"
            });

            this.entidad = new AdministradoresDepartamentos()
            {
                Cedula = "1" + DateTime.Now.Ticks.ToString(),
                Nombre = "Admin UT",
                Apellido = "Prueba",
                Genero = 'M',
                Correo = "admin" + Guid.NewGuid().ToString("N") + "@correo.com",
                FechaNacimiento = new DateTime(1998, 4, 10),
                FechaRegistro = DateTime.Now,
                EstadoCivil = this.estadoCivilAdmin!.Id,
                Nacionalidad = this.nacionalidadAdmin!.Id,
                Sueldo = 4000000m,
                Estado = true,
                Jornada = "Diurna",
                PresupuestoDepartamento = 9000000m,
                Departamento = this.departamentoAdmin.Id
            };

            // Se guarda por LibPresentacion, que internamente llama Comunicaciones y la API.
            this.entidad = this.iAdministradoresDepartamentosNegocio!.Guardar(this.entidad!);

            if (this.entidad != null && this.entidad.Id != 0)
                return;

            throw new Exception("Error al Guardar AdministradoresDepartamentos a traves de la API.");
        }

        private void Consultar()
        {
            // Se consulta por API y se valida que exista el registro guardado.
            var lista = this.iAdministradoresDepartamentosNegocio!.Consultar();
            var existeEntidad = lista.Any(x => x.Id == this.entidad!.Id);

            if (existeEntidad)
                return;

            throw new Exception("La entidad AdministradoresDepartamentos guardada no se encontro al Consultar la API.");
        }

        private void Modificar()
        {
            this.entidad!.PresupuestoDepartamento = 9500000m;
            this.entidad.Jornada = "Mixta";

            // Se modifica por API y se valida el dato cambiado.
            var resultadoModificar = this.iAdministradoresDepartamentosNegocio!.Modificar(this.entidad!);

            if (resultadoModificar != null && resultadoModificar.PresupuestoDepartamento == 9500000m)
                return;

            throw new Exception("Error al Modificar AdministradoresDepartamentos a traves de la API.");
        }

        private void Borrar()
        {
            // Se borra primero la entidad principal.
            var resultadoBorrar = this.iAdministradoresDepartamentosNegocio!.Borrar(this.entidad!);

            if (resultadoBorrar == null || resultadoBorrar.Id == 0)
                throw new Exception("Error al Borrar AdministradoresDepartamentos a traves de la API.");

            // Luego se borran los datos relacionados creados para esta prueba.
            new DepartamentosNegocio().Borrar(this.departamentoAdmin!);
            new NacionalidadesNegocio().Borrar(this.nacionalidadAdmin!);
            new EstadosCivilesNegocio().Borrar(this.estadoCivilAdmin!);
        }
    }
}
