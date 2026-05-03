using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones.RRHH;
using LibInmobiliaria.Interfaces.RRHH;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.RRHH
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class JefesSectoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de JefesSectores.
        private IJefesSectoresNegocio? IJefesSectoresNegocio;

        // Constructor del controlador.
        public JefesSectoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.IJefesSectoresNegocio = new JefesSectoresNegocio();
        }

        // Endpoint para consultar todos los JefesSectores.
        [HttpGet]
        public List<JefesSectores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IJefesSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IJefesSectoresNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public JefesSectores Guardar(JefesSectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IJefesSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IJefesSectoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public JefesSectores Modificar(JefesSectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IJefesSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IJefesSectoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public JefesSectores Borrar(JefesSectores entidad)
        {
            // Validación por si no existe implementación.
            if (this.IJefesSectoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IJefesSectoresNegocio.Borrar(entidad);
        }
    }
}
