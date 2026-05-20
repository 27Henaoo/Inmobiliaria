using LibModelos._5._1ModelosComunes;
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
    public class CompradoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Compradores.
        private ICompradoresNegocio? ICompradoresNegocio;

        // Constructor del controlador.
        public CompradoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.ICompradoresNegocio = new CompradoresNegocio();
        }

        // Endpoint para consultar todos los Compradores.
        [HttpGet]
        public List<Compradores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ICompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ICompradoresNegocio.Consultar();
        }

        // Endpoint para guardar Compradores Nuevos.
        [HttpPost]
        public Compradores Guardar(Compradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ICompradoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar Compradores existentes.
        [HttpPut]
        public Compradores Modificar(Compradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ICompradoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar Compradores Existentes.
        [HttpDelete]
        public Compradores Borrar(Compradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ICompradoresNegocio.Borrar(entidad);
        }
    }
}
