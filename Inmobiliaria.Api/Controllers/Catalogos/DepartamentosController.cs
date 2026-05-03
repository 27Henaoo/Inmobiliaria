using LibInmobiliaria.Entidades;
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
    public class DepartamentosController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Departamentos.
        private IDepartamentosNegocio? IDepartamentosNegocio;

        // Constructor del controlador.
        public DepartamentosController()
        {
            // Se crea la implementación concreta del negocio.
            this.IDepartamentosNegocio = new DepartamentosNegocio();
        }

        // Endpoint para consultar todos los Departamentos.
        [HttpGet]
        public List<Departamentos> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IDepartamentosNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Departamentos Guardar(Departamentos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IDepartamentosNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Departamentos Modificar(Departamentos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IDepartamentosNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Departamentos Borrar(Departamentos entidad)
        {
            // Validación por si no existe implementación.
            if (this.IDepartamentosNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IDepartamentosNegocio.Borrar(entidad);
        }
    }
}
