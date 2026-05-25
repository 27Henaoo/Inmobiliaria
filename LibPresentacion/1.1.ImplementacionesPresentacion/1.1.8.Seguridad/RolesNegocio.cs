
using LibModelos._5._2LoginRegisterEntidades;
using LibPresentacion._1._2.InterfacesPresentacion;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad;
using Newtonsoft.Json;

namespace LibPresentacion._1._1.ImplementacionesPresentacion._1._1._8.Seguridad
{
    public class RolesNegocio : IRolesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<Roles> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Roles/Consultar";
            datos["Metodo"] = "GET";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Roles>();

            return JsonConvert.DeserializeObject<List<Roles>>(
                respuesta["Valor"].ToString()!)!;
        }

        public Roles Guardar(Roles entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Roles/Guardar";
            datos["Metodo"] = "POST";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Roles();

            return JsonConvert.DeserializeObject<Roles>(
                respuesta["Valor"].ToString()!)!;
        }

        public Roles Modificar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Roles/Modificar";
            datos["Metodo"] = "PUT";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Roles();

            return JsonConvert.DeserializeObject<Roles>(
                respuesta["Valor"].ToString()!)!;
        }

        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Roles/Borrar";
            datos["Metodo"] = "DELETE";
            datos["Entidad"] = entidad;

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();

            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Roles();

            return JsonConvert.DeserializeObject<Roles>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
