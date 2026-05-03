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
    public class BienesMueblesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de BienesMuebles.
        private IBienesMueblesNegocio? IBienesMueblesNegocio;

        // Constructor del controlador.
        public BienesMueblesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IBienesMueblesNegocio = new BienesMueblesNegocio();
        }

        // Endpoint para consultar todos los BienesMuebles.
        [HttpGet]
        public List<BienesMuebles> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IBienesMueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IBienesMueblesNegocio.Consultar();
        }

        // Endpoint para guardar BienesMuebles Nuevos.
        [HttpPost]
        public BienesMuebles Guardar(BienesMuebles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesMueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IBienesMueblesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar BienesMuebles existentes.
        [HttpPut]
        public BienesMuebles Modificar(BienesMuebles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesMueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IBienesMueblesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar BienesMuebles Existentes.
        [HttpDelete]
        public BienesMuebles Borrar(BienesMuebles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesMueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IBienesMueblesNegocio.Borrar(entidad);
        }
    }
}
