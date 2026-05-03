using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones.Patrimonio;
using LibInmobiliaria.Interfaces.Patrimonio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Patrimonio
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class BienesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Bienes.
        private IBienesNegocio? IBienesNegocio;

        // Constructor del controlador.
        public BienesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IBienesNegocio = new BienesNegocio();
        }

        // Endpoint para consultar todos los Bienes.
        [HttpGet]
        public List<Bienes> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IBienesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IBienesNegocio.Consultar();
        }

        // Endpoint para guardar Bienes Nuevos.
        [HttpPost]
        public Bienes Guardar(Bienes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IBienesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar Bienes existentes.
        [HttpPut]
        public Bienes Modificar(Bienes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IBienesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar Bienes Existentes.
        [HttpDelete]
        public Bienes Borrar(Bienes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IBienesNegocio.Borrar(entidad);
        }
    }
}
