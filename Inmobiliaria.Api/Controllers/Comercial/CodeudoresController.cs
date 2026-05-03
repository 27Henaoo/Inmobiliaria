using LibInmobiliaria.Entidades;
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
    public class CodeudoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Codeudores.
        private ICodeudoresNegocio? ICodeudoresNegocio;

        // Constructor del controlador.
        public CodeudoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.ICodeudoresNegocio = new CodeudoresNegocio();
        }

        // Endpoint para consultar todos los Codeudores.
        [HttpGet]
        public List<Codeudores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ICodeudoresNegocio.Consultar();
        }

        // Endpoint para guardar Codeudores Nuevos.
        [HttpPost]
        public Codeudores Guardar(Codeudores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ICodeudoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar Codeudores existentes.
        [HttpPut]
        public Codeudores Modificar(Codeudores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ICodeudoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar Codeudores Existentes.
        [HttpDelete]
        public Codeudores Borrar(Codeudores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ICodeudoresNegocio.Borrar(entidad);
        }
    }
}
