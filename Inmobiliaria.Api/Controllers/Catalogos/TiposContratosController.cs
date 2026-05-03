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
    public class TiposContratosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de TiposContratos.
        private ITiposContratosNegocio? ITiposContratosNegocio;

        // Constructor del controlador.
        public TiposContratosController()
        {
            // Se crea la implementación concreta del negocio.
            this.ITiposContratosNegocio = new TiposContratosNegocio();
        }

        // Endpoint para consultar todos los TiposContratos.
        [HttpGet]
        public List<TiposContratos> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ITiposContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ITiposContratosNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public TiposContratos Guardar(TiposContratos entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITiposContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ITiposContratosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public TiposContratos Modificar(TiposContratos entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITiposContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ITiposContratosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public TiposContratos Borrar(TiposContratos entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITiposContratosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ITiposContratosNegocio.Borrar(entidad);
        }
    }
}
