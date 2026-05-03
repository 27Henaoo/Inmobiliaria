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
    public class TrabajadoresController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Trabajadores.
        private ITrabajadoresNegocio? ITrabajadoresNegocio;

        // Constructor del controlador.
        public TrabajadoresController()
        {
            // Se crea la implementación concreta del negocio.
            this.ITrabajadoresNegocio = new TrabajadoresNegocio();
        }

        // Endpoint para consultar todos los Trabajadores.
        [HttpGet]
        public List<Trabajadores> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ITrabajadoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ITrabajadoresNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Trabajadores Guardar(Trabajadores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITrabajadoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ITrabajadoresNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Trabajadores Modificar(Trabajadores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITrabajadoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ITrabajadoresNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Trabajadores Borrar(Trabajadores entidad)
        {
            // Validación por si no existe implementación.
            if (this.ITrabajadoresNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ITrabajadoresNegocio.Borrar(entidad);
        }
    }
}
