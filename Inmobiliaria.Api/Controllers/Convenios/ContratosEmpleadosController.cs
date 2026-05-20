using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Implementaciones.Convenios;
using LibInmobiliaria.Interfaces.Convenios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Convenios
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class ContratosEmpleadosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de ContratosEmpleados.
        private IContratosEmpleadosNegocio? IContratosEmpleadosNegocio;

        // Constructor del controlador.
        public ContratosEmpleadosController()
        {
            // Se crea la implementación concreta del negocio.
            this.IContratosEmpleadosNegocio = new ContratosEmpleadosNegocio();
        }

        // Endpoint para consultar todos los ContratosEmpleados.
        [HttpGet]
        public List<ContratosEmpleados> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IContratosEmpleadosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IContratosEmpleadosNegocio.Consultar();
        }

        // Endpoint para guardar ContratosEmpleados Nuevos.
        [HttpPost]
        public ContratosEmpleados Guardar(ContratosEmpleados entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosEmpleadosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IContratosEmpleadosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar ContratosEmpleados existentes.
        [HttpPut]
        public ContratosEmpleados Modificar(ContratosEmpleados entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosEmpleadosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IContratosEmpleadosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar ContratosEmpleados Existentes.
        [HttpDelete]
        public ContratosEmpleados Borrar(ContratosEmpleados entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosEmpleadosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IContratosEmpleadosNegocio.Borrar(entidad);
        }
    }
}
