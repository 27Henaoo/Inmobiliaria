using LibInmobiliaria.Implementaciones.Seguridad;
using LibInmobiliaria.Interfaces.Seguridad;
using LibModelos._5._2LoginRegisterEntidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Api.Controllers.Seguridad
{
    //Indica que esta clase es un controlador de API
    [ApiController]

    //Define la ruta usando el nombre del controlador y la accion
    [Route("[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        //Variable privada para usar la logica de negocio de Usuarios
        private IUsuariosNegocio? IUsuariosNegocio;

        //Constructor del controlador
        public UsuariosController()
        {
            //Se crea la implementacion concreta del negocio
            this.IUsuariosNegocio = new UsuariosNegocio();
        }

        //Endpoint para consultar todos los Usuarios
        [HttpGet]
        public List<Usuarios> Consultar()
        {
            //Validacion por si no existe implementacion
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Consultar del negocio
            return this.IUsuariosNegocio.Consultar();
        }

        //Endpoint para guardar Usuarios nuevos
        //Aqui ClaveHash se usa temporalmente para recibir la clave normal
        [HttpPost]
        public Usuarios Guardar(Usuarios entidad)
        {
            //Validacion por si no existe implementacion
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Guardar del negocio
            return this.IUsuariosNegocio.Guardar(entidad);
        }

        //Endpoint para modificar Usuarios existentes
        [HttpPut]
        public Usuarios Modificar(Usuarios entidad)
        {
            //Validacion por si no existe implementacion
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Modificar del negocio
            return this.IUsuariosNegocio.Modificar(entidad);
        }

        //Endpoint para borrar Usuarios existentes
        [HttpDelete]
        public Usuarios Borrar(Usuarios entidad)
        {
            //Validacion por si no existe implementacion.
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");

            //Se llama al metodo Borrar del negocio.
            return this.IUsuariosNegocio.Borrar(entidad);
        }

        //Endpoint para validar login
        //El correo llega en entidad.Correo
        //La clave llega en entidad.ClaveHash
        [HttpPost]
        public Usuarios? ValidarLogin(Usuarios entidad)
        {
            //Validacion por si no existe implementacion
            if (this.IUsuariosNegocio == null)
                throw new Exception("No implementado");

            //Se valida el login usando correo y clave
            return this.IUsuariosNegocio.ValidarLogin(
                entidad.Correo,
                entidad.ClaveHash
            );
        }
    }
}
