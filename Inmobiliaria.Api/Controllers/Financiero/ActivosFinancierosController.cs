using LibModelos._5._1ModelosComunes;
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
    public class ActivosFinancierosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de ActivosFinancieros.
        private IActivosFinancierosNegocio? IActivosFinancierosNegocio;

        // Constructor del controlador.
        public ActivosFinancierosController()
        {
            // Se crea la implementación concreta del negocio.
            this.IActivosFinancierosNegocio = new ActivosFinancierosNegocio();
        }

        // Endpoint para consultar todos los ActivosFinancieros.
        [HttpGet]
        public List<ActivosFinancieros> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IActivosFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IActivosFinancierosNegocio.Consultar();
        }

        // Endpoint para guardar ActivosFinancieros Nuevos.
        [HttpPost]
        public ActivosFinancieros Guardar(ActivosFinancieros entidad)
        {
            // Validación por si no existe implementación.
            if (this.IActivosFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IActivosFinancierosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar ActivosFinancieros existentes.
        [HttpPut]
        public ActivosFinancieros Modificar(ActivosFinancieros entidad)
        {
            // Validación por si no existe implementación.
            if (this.IActivosFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IActivosFinancierosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar ActivosFinancieros Existentes.
        [HttpDelete]
        public ActivosFinancieros Borrar(ActivosFinancieros entidad)
        {
            // Validación por si no existe implementación.
            if (this.IActivosFinancierosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IActivosFinancierosNegocio.Borrar(entidad);
        }
    }

}
