using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones.RRHH;
using LibInmobiliaria.Interfaces.RRHH;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.RRHH
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class EmpleadosSectoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de EmpleadosSectores.
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresNegocio;

        // Constructor del controlador.
        public EmpleadosSectoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.IEmpleadosSectoresNegocio = new EmpleadosSectoresNegocio();
        }

        // Endpoint para consultar todos los EmpleadosSectores.
        [HttpGet]
        public List<EmpleadosSectores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IEmpleadosSectoresNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public EmpleadosSectores Guardar(EmpleadosSectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IEmpleadosSectoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public EmpleadosSectores Modificar(EmpleadosSectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IEmpleadosSectoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public EmpleadosSectores Borrar(EmpleadosSectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IEmpleadosSectoresNegocio.Borrar(entidad);
        }
    }
}
