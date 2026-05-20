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
    public class AdministradoresDepartamentosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de AdministradoresDepartamentos.
        private IAdministradoresDepartamentosNegocio? IAdministradoresDepartamentosNegocio;

        // Constructor del controlador.
        public AdministradoresDepartamentosController()
        {
            // Se crea la implementación concreta del negocio.
            this.IAdministradoresDepartamentosNegocio = new AdministradoresDepartamentosNegocio();
        }

        // Endpoint para consultar todos los AdministradoresDepartamentos.
        [HttpGet]
        public List<AdministradoresDepartamentos> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IAdministradoresDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IAdministradoresDepartamentosNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public AdministradoresDepartamentos Guardar(AdministradoresDepartamentos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IAdministradoresDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IAdministradoresDepartamentosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IAdministradoresDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IAdministradoresDepartamentosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public AdministradoresDepartamentos Borrar(AdministradoresDepartamentos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IAdministradoresDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IAdministradoresDepartamentosNegocio.Borrar(entidad);
        }
    }
}
