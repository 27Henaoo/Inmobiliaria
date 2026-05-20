using LibInmobiliaria.Implementaciones.Convenios;
using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces.Convenios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Convenios
{
    // Indica que esta clase es un controlador de API.
    [ApiController]

    // Define la ruta usando el nombre del controlador y la acción.
    [Route("[controller]/[action]")]
    public class PropiedadesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Propiedades.
        private IPropiedadesNegocio? IPropiedadesNegocio;

        // Constructor del controlador.
        public PropiedadesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IPropiedadesNegocio = new PropiedadesNegocio();
        }

        // Endpoint para consultar todos los Propiedades.
        [HttpGet]
        public List<Propiedades> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IPropiedadesNegocio.Consultar();
        }

        // Endpoint para guardar Propiedades Nuevos.
        [HttpPost]
        public Propiedades Guardar(Propiedades entidad)
        {
            // Validación por si no existe implementación.
            if (this.IPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IPropiedadesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar Propiedades existentes.
        [HttpPut]
        public Propiedades Modificar(Propiedades entidad)
        {
            // Validación por si no existe implementación.
            if (this.IPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IPropiedadesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar Propiedades Existentes.
        [HttpDelete]
        public Propiedades Borrar(Propiedades entidad)
        {
            // Validación por si no existe implementación.
            if (this.IPropiedadesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IPropiedadesNegocio.Borrar(entidad);
        }
    }
}
