using LibInmobiliaria.Entidades;
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
    public class ClientesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Clientes.
        private IClientesNegocio? IClientesNegocio;

        // Constructor del controlador.
        public ClientesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IClientesNegocio = new ClientesNegocio();
        }

        // Endpoint para consultar todos los Clientes.
        [HttpGet]
        public List<Clientes> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IClientesNegocio.Consultar();
        }

        // Endpoint para guardar Clientes Nuevos.
        [HttpPost]
        public Clientes Guardar(Clientes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IClientesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar Clientes existentes.
        [HttpPut]
        public Clientes Modificar(Clientes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IClientesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar Clientes Existentes.
        [HttpDelete]
        public Clientes Borrar(Clientes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IClientesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IClientesNegocio.Borrar(entidad);
        }
    }
}
