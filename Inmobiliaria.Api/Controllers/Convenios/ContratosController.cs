using LibInmobiliaria.Implementaciones.Convenios;
using LibInmobiliaria.Entidades;
using LibInmobiliaria.Interfaces.Convenios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Convenios
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class ContratosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Contratos.
        private IContratosNegocio? IContratosNegocio;

        // Constructor del controlador.
        public ContratosController()
        {
            // Se crea la implementación concreta del negocio.
            this.IContratosNegocio = new ContratosNegocio();
        }

        // Endpoint para consultar todos los Contratos.
        [HttpGet]
        public List<Contratos> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IContratosNegocio.Consultar();
        }

        // Endpoint para guardar Contratos Nuevos.
        [HttpPost]
        public Contratos Guardar(Contratos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IContratosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar Contratos existentes.
        [HttpPut]
        public Contratos Modificar(Contratos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IContratosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar Contratos Existentes.
        [HttpDelete]
        public Contratos Borrar(Contratos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IContratosNegocio.Borrar(entidad);
        }
    }
}
