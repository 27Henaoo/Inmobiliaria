using LibInmobiliaria.Implementaciones.Seguridad;
using LibInmobiliaria.Interfaces.Seguridad;
using LibModelos._5._2LoginRegisterEntidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Seguridad
{
    //Indica que esta clase es un controlador de API
    [ApiController]

    //Define la ruta usando el nombre del controlador y la accion
    [Route("[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        //Variable privada para usar la logica de negocio de Roles
        private IRolesNegocio? IRolesNegocio;

        // Constructor del controlador
        public RolesController()
        {
            //Se crea la implementacion concreta del negocio
            this.IRolesNegocio = new RolesNegocio();
        }

        //Endpoint para consultar todos los Roles
        [HttpGet]
        public List<Roles> Consultar()
        {
            //Validacion por si no existe implementacion
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Consultar del negocio
            return this.IRolesNegocio.Consultar();
        }

        //Endpoint para guardar Roles nuevos
        [HttpPost]
        public Roles Guardar(Roles entidad)
        {
            //Validacion por si no existe implementacion
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Guardar del negocio
            return this.IRolesNegocio.Guardar(entidad);
        }

        //Endpoint para modificar Roles existentes
        [HttpPut]
        public Roles Modificar(Roles entidad)
        {
            //Validacion por si no existe implementacion
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Modificar del negocio
            return this.IRolesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar Roles existentes.
        [HttpDelete]
        public Roles Borrar(Roles entidad)
        {
            //Validacion por si no existe implementacion
            if (this.IRolesNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Borrar del negocio
            return this.IRolesNegocio.Borrar(entidad);
        }
    }
}
