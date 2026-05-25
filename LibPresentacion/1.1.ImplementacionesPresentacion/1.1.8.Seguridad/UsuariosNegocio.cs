
using LibModelos._5._2LoginRegisterEntidades;
using LibPresentacion._1._2.InterfacesPresentacion;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad;
using Newtonsoft.Json;

namespace LibPresentacion._1._1.ImplementacionesPresentacion._1._1._8.Seguridad
{
    public class UsuariosNegocio : IUsuariosNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Usuarios> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Usuarios/Consultar";
            datos["Metodo"] = "GET";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Usuarios>();

            return JsonConvert.DeserializeObject<List<Usuarios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Usuarios Guardar(Usuarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Usuarios/Guardar";
            datos["Metodo"] = "POST";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Usuarios();

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Usuarios Modificar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Usuarios/Modificar";
            datos["Metodo"] = "PUT";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Usuarios();

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Usuarios/Borrar";
            datos["Metodo"] = "DELETE";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Usuarios();

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Usuarios ValidarLogin(Usuarios entidad)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Usuarios/ValidarLogin";
            datos["Metodo"] = "POST";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Usuarios();

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
