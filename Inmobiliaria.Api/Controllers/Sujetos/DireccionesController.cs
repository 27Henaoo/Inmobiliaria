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
    public class DireccionesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Direcciones.
        private IDireccionesNegocio? IDireccionesNegocio;

        // Constructor del controlador.
        public DireccionesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IDireccionesNegocio = new DireccionesNegocio();
        }

        // Endpoint para consultar todos los Direcciones.
        [HttpGet]
        public List<Direcciones> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IDireccionesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IDireccionesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Direcciones Guardar(Direcciones entidad)
        {
            // Validación por si no existe implementación.
            if (this.IDireccionesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IDireccionesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Direcciones Modificar(Direcciones entidad)
        {
            // Validación por si no existe implementación.
            if (this.IDireccionesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IDireccionesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Direcciones Borrar(Direcciones entidad)
        {
            // Validación por si no existe implementación.
            if (this.IDireccionesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IDireccionesNegocio.Borrar(entidad);
        }
    }
}
