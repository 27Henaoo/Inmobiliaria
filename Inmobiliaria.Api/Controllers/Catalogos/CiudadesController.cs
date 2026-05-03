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
    public class CiudadesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Ciudades.
        private ICiudadesNegocio? ICiudadesNegocio;

        // Constructor del controlador.
        public CiudadesController()
        {
            // Se crea la implementación concreta del negocio.
            this.ICiudadesNegocio = new CiudadesNegocio();
        }

        // Endpoint para consultar todos los Ciudades.
        [HttpGet]
        public List<Ciudades> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ICiudadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ICiudadesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Ciudades Guardar(Ciudades entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICiudadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ICiudadesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Ciudades Modificar(Ciudades entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICiudadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ICiudadesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Ciudades Borrar(Ciudades entidad)
        {
            // Validación por si no existe implementación.
            if (this.ICiudadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ICiudadesNegocio.Borrar(entidad);
        }
    }
}
