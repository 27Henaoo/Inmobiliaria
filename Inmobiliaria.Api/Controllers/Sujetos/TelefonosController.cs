using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Implementaciones.Sujetos;
using LibInmobiliaria.Interfaces.Sujetos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Sujetos
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class TelefonosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Telefonos.
        private ITelefonosNegocio? ITelefonosNegocio;

        // Constructor del controlador.
        public TelefonosController()
        {
            // Se crea la implementación concreta del negocio.
            this.ITelefonosNegocio = new TelefonosNegocio();
        }

        // Endpoint para consultar todos los Telefonos.
        [HttpGet]
        public List<Telefonos> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ITelefonosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ITelefonosNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Telefonos Guardar(Telefonos entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITelefonosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ITelefonosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Telefonos Modificar(Telefonos entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITelefonosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ITelefonosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Telefonos Borrar(Telefonos entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITelefonosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ITelefonosNegocio.Borrar(entidad);
        }
    }
}
