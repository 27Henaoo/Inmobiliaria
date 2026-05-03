using LibInmobiliaria.Entidades;
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
    public class PersonasController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Personas.
        private IPersonasNegocio? IPersonasNegocio;

        // Constructor del controlador.
        public PersonasController()
        {
            // Se crea la implementación concreta del negocio.
            this.IPersonasNegocio = new PersonasNegocio();
        }

        // Endpoint para consultar todos los Personas.
        [HttpGet]
        public List<Personas> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IPersonasNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IPersonasNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Personas Guardar(Personas entidad)
        {
            // Validación por si no existe implementación.
            if (this.IPersonasNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IPersonasNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Personas Modificar(Personas entidad)
        {
            // Validación por si no existe implementación.
            if (this.IPersonasNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IPersonasNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Personas Borrar(Personas entidad)
        {
            // Validación por si no existe implementación.
            if (this.IPersonasNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IPersonasNegocio.Borrar(entidad);
        }
    }
}
