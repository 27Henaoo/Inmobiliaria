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
    public class ContratosCodeudoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de ContratosCodeudores.
        private IContratosCodeudoresNegocio? IContratosCodeudoresNegocio;

        // Constructor del controlador.
        public ContratosCodeudoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.IContratosCodeudoresNegocio = new ContratosCodeudoresNegocio();
        }

        // Endpoint para consultar todos los ContratosCodeudores.
        [HttpGet]
        public List<ContratosCodeudores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IContratosCodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IContratosCodeudoresNegocio.Consultar();
        }

        // Endpoint para guardar ContratosCodeudores Nuevos.
        [HttpPost]
        public ContratosCodeudores Guardar(ContratosCodeudores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosCodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IContratosCodeudoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar ContratosCodeudores existentes.
        [HttpPut]
        public ContratosCodeudores Modificar(ContratosCodeudores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosCodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IContratosCodeudoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar ContratosCodeudores Existentes.
        [HttpDelete]
        public ContratosCodeudores Borrar(ContratosCodeudores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosCodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IContratosCodeudoresNegocio.Borrar(entidad);
        }
    }
}
