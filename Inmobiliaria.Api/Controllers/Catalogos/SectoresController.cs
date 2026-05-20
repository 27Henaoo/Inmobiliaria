using LibModelos._5._1ModelosComunes;
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
    public class SectoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Sectores.
        private ISectoresNegocio? ISectoresNegocio;

        // Constructor del controlador.
        public SectoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.ISectoresNegocio = new SectoresNegocio();
        }

        // Endpoint para consultar todos los Sectores.
        [HttpGet]
        public List<Sectores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ISectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ISectoresNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Sectores Guardar(Sectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ISectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ISectoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Sectores Modificar(Sectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ISectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ISectoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Sectores Borrar(Sectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ISectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ISectoresNegocio.Borrar(entidad);
        }
    }
}
