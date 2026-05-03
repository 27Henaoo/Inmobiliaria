using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones.Financiero;
using LibInmobiliaria.Interfaces.Financiero;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Financiero
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class ExpedientesFinancierosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de ExpedientesFinancieros.
        private IExpedientesFinancierosNegocio? IExpedientesFinancierosNegocio;

        // Constructor del controlador.
        public ExpedientesFinancierosController()
        {
            // Se crea la implementación concreta del negocio.
            this.IExpedientesFinancierosNegocio = new ExpedientesFinancierosNegocio();
        }

        // Endpoint para consultar todos los ExpedientesFinancieros.
        [HttpGet]
        public List<ExpedientesFinancieros> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IExpedientesFinancierosNegocio.Consultar();
        }

        // Endpoint para guardar ExpedientesFinancieros Nuevos.
        [HttpPost]
        public ExpedientesFinancieros Guardar(ExpedientesFinancieros entidad)
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IExpedientesFinancierosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar ExpedientesFinancieros existentes.
        [HttpPut]
        public ExpedientesFinancieros Modificar(ExpedientesFinancieros entidad)
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IExpedientesFinancierosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar ExpedientesFinancieros Existentes.
        [HttpDelete]
        public ExpedientesFinancieros Borrar(ExpedientesFinancieros entidad)
        {
            // Validación por si no existe implementación.
            if (this.IExpedientesFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IExpedientesFinancierosNegocio.Borrar(entidad);
        }
    }

}
