using LibModelos._5._1ModelosComunes;
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
    public class ExpedientesLaboralesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de ExpedientesLaborales.
        private IExpedientesLaboralesNegocio? IExpedientesLaboralesNegocio;

        // Constructor del controlador.
        public ExpedientesLaboralesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IExpedientesLaboralesNegocio = new ExpedientesLaboralesNegocio();
        }

        // Endpoint para consultar todos los ExpedientesLaborales.
        [HttpGet]
        public List<ExpedientesLaborales> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesLaboralesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IExpedientesLaboralesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public ExpedientesLaborales Guardar(ExpedientesLaborales entidad)
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesLaboralesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IExpedientesLaboralesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public ExpedientesLaborales Modificar(ExpedientesLaborales entidad)
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesLaboralesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IExpedientesLaboralesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public ExpedientesLaborales Borrar(ExpedientesLaborales entidad)
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesLaboralesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IExpedientesLaboralesNegocio.Borrar(entidad);
        }
    }
}
