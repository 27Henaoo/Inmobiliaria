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
    public class TiposPropiedadesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de TiposPropiedades.
        private ITiposPropiedadesNegocio? ITiposPropiedadesNegocio;

        // Constructor del controlador.
        public TiposPropiedadesController()
        {
            // Se crea la implementación concreta del negocio.
            this.ITiposPropiedadesNegocio = new TiposPropiedadesNegocio();
        }

        // Endpoint para consultar todos los TiposPropiedades.
        [HttpGet]
        public List<TiposPropiedades> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ITiposPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ITiposPropiedadesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public TiposPropiedades Guardar(TiposPropiedades entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITiposPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ITiposPropiedadesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public TiposPropiedades Modificar(TiposPropiedades entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITiposPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ITiposPropiedadesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public TiposPropiedades Borrar(TiposPropiedades entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITiposPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ITiposPropiedadesNegocio.Borrar(entidad);
        }
    }
}
