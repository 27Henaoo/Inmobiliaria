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
    public class CodeudoresCompradoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de CodeudoresCompradores.
        private ICodeudoresCompradoresNegocio? ICodeudoresCompradoresNegocio;

        // Constructor del controlador.
        public CodeudoresCompradoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.ICodeudoresCompradoresNegocio = new CodeudoresCompradoresNegocio();
        }

        // Endpoint para consultar todos los CodeudoresCompradores.
        [HttpGet]
        public List<CodeudoresCompradores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ICodeudoresCompradoresNegocio.Consultar();
        }

        // Endpoint para guardar CodeudoresCompradores Nuevos.
        [HttpPost]
        public CodeudoresCompradores Guardar(CodeudoresCompradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ICodeudoresCompradoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar CodeudoresCompradores existentes.
        [HttpPut]
        public CodeudoresCompradores Modificar(CodeudoresCompradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ICodeudoresCompradoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar CodeudoresCompradores Existentes.
        [HttpDelete]
        public CodeudoresCompradores Borrar(CodeudoresCompradores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICodeudoresCompradoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ICodeudoresCompradoresNegocio.Borrar(entidad);
        }
    }
}
