using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Implementaciones.Comercial;
using LibInmobiliaria.Interfaces.Comercial;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Comercial
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class EmpleadosCompradoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de EmpleadosCompradores.
        private IEmpleadosCompradoresNegocio? IEmpleadosCompradoresNegocio;

        // Constructor del controlador.
        public EmpleadosCompradoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.IEmpleadosCompradoresNegocio = new EmpleadosCompradoresNegocio();
        }

        // Endpoint para consultar todos los EmpleadosCompradores.
        [HttpGet]
        public List<EmpleadosCompradores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IEmpleadosCompradoresNegocio.Consultar();
        }

        // Endpoint para guardar EmpleadosCompradores Nuevos.
        [HttpPost]
        public EmpleadosCompradores Guardar(EmpleadosCompradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IEmpleadosCompradoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar EmpleadosCompradores existentes.
        [HttpPut]
        public EmpleadosCompradores Modificar(EmpleadosCompradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IEmpleadosCompradoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar EmpleadosCompradores Existentes.
        [HttpDelete]
        public EmpleadosCompradores Borrar(EmpleadosCompradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEmpleadosCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IEmpleadosCompradoresNegocio.Borrar(entidad);
        }
    }
}
