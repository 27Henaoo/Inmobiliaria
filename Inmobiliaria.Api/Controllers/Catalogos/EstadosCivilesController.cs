using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Implementaciones.Catalogos;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Catalogo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Catalogos
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class EstadosCivilesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de EstadosCiviles.
        private IEstadosCivilesNegocio? IEstadosCivilesNegocio;

        // Constructor del controlador.
        public EstadosCivilesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IEstadosCivilesNegocio = new EstadosCivilesNegocio();
        }

        // Endpoint para consultar todos los EstadosCiviles.
        [HttpGet]
        public List<EstadosCiviles> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IEstadosCivilesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IEstadosCivilesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public EstadosCiviles Guardar(EstadosCiviles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEstadosCivilesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IEstadosCivilesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public EstadosCiviles Modificar(EstadosCiviles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEstadosCivilesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IEstadosCivilesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public EstadosCiviles Borrar(EstadosCiviles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEstadosCivilesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IEstadosCivilesNegocio.Borrar(entidad);
        }
    }
}
