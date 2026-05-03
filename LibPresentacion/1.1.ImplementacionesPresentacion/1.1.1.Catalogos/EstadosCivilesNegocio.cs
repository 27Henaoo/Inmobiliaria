using LibInmobiliaria.Entidades;
using LibPresentacion._1._2.InterfacesPresentacion;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Newtonsoft.Json;

namespace LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos
{
    public class EstadosCivilesNegocio : IEstadosCivilesNegocio
    {
        private IComunicaciones? iComunicaciones;

        public List<EstadosCiviles> Consultar()
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/EstadosCiviles/Consultar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<EstadosCiviles>();

            return JsonConvert.DeserializeObject<List<EstadosCiviles>>(
                respuesta["Valor"].ToString()!)!;
        }

        public EstadosCiviles Guardar(EstadosCiviles entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iComunicaciones = new Comunicaciones();

            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/EstadosCiviles/Guardar";

            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new EstadosCiviles();

            return JsonConvert.DeserializeObject<EstadosCiviles>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
