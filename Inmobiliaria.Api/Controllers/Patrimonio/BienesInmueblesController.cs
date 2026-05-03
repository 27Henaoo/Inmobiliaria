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
    public class BienesInmueblesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de BienesInmuebles.
        private IBienesInmueblesNegocio? IBienesInmueblesNegocio;

        // Constructor del controlador.
        public BienesInmueblesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IBienesInmueblesNegocio = new BienesInmueblesNegocio();
        }

        // Endpoint para consultar todos los BienesInmuebles.
        [HttpGet]
        public List<BienesInmuebles> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IBienesInmueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IBienesInmueblesNegocio.Consultar();
        }

        // Endpoint para guardar BienesInmuebles Nuevos.
        [HttpPost]
        public BienesInmuebles Guardar(BienesInmuebles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesInmueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IBienesInmueblesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar BienesInmuebles existentes.
        [HttpPut]
        public BienesInmuebles Modificar(BienesInmuebles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesInmueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IBienesInmueblesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar BienesInmuebles Existentes.
        [HttpDelete]
        public BienesInmuebles Borrar(BienesInmuebles entidad)
        {
            // Validación por si no existe implementación.
            if (this.IBienesInmueblesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IBienesInmueblesNegocio.Borrar(entidad);
        }
    }
}
