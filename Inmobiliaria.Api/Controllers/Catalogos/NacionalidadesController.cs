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
    public class NacionalidadesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Nacionalidades.
        private INacionalidadesNegocio? INacionalidadesNegocio;

        // Constructor del controlador.
        public NacionalidadesController()
        {
            // Se crea la implementación concreta del negocio.
            this.INacionalidadesNegocio = new NacionalidadesNegocio();
        }

        // Endpoint para consultar todos los Nacionalidades.
        [HttpGet]
        public List<Nacionalidades> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.INacionalidadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.INacionalidadesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Nacionalidades Guardar(Nacionalidades entidad)
        {
            // Validación por si no existe implementación.
            if (this.INacionalidadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.INacionalidadesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Nacionalidades Modificar(Nacionalidades entidad)
        {
            // Validación por si no existe implementación.
            if (this.INacionalidadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.INacionalidadesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Nacionalidades Borrar(Nacionalidades entidad)
        {
            // Validación por si no existe implementación.
            if (this.INacionalidadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.INacionalidadesNegocio.Borrar(entidad);
        }
    }
}
