using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._2.InterfacesPresentacion;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using Newtonsoft.Json;


namespace LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial
{
    public class CodeudoresNegocio : ICodeudoresNegocio
    {
        private IComunicaciones? iComunicaciones;

        // Este método consulta y devuelve todos los Codeudores.
        public List<Codeudores> Consultar()
        {
            // Se crea el diccionario con los datos de la petición.
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Codeudores/Consultar";
            datos["Metodo"] = "GET";

            // Se ejecuta la comunicación.
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            // Si no viene valor, se devuelve una lista vacía.
            if (!respuesta.ContainsKey("Valor"))
                return new List<Codeudores>();

            // Se deserializa la respuesta y se devuelve.
            return JsonConvert.DeserializeObject<List<Codeudores>>(
                respuesta["Valor"].ToString()!)!;
        }

        // Este método guarda un Arbol nuevo.
        public Codeudores Guardar(Codeudores entidad)
        {
            // Si el Id es distinto de 0, significa que ya fue guardado.
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // Se crea el diccionario con los datos de la petición.
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Codeudores/Guardar";
            datos["Metodo"] = "POST";
            datos["Entidad"] = entidad;

            // Se ejecuta la comunicación.
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            // Si no viene valor, se devuelve una entidad vacía.
            if (!respuesta.ContainsKey("Valor"))
                return new Codeudores();

            // Se deserializa la respuesta y se devuelve.
            return JsonConvert.DeserializeObject<Codeudores>(
                respuesta["Valor"].ToString()!)!;
        }

        // Este método modifica un Arbol existente.
        public Codeudores Modificar(Codeudores entidad)
        {
            // Si el Id es 0, no se puede modificar.
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            // Se crea el diccionario con los datos de la petición.
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Codeudores/Modificar";
            datos["Metodo"] = "PUT";
            datos["Entidad"] = entidad;

            // Se ejecuta la comunicación.
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            // Si no viene valor, se devuelve una entidad vacía.
            if (!respuesta.ContainsKey("Valor"))
                return new Codeudores();

            // Se deserializa la respuesta y se devuelve.
            return JsonConvert.DeserializeObject<Codeudores>(
                respuesta["Valor"].ToString()!)!;
        }

        // Este método borra un Arbol existente.
        public Codeudores Borrar(Codeudores entidad)
        {
            // Si el Id es 0, no se puede borrar.
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            // Se crea el diccionario con los datos de la petición.
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7173/Codeudores/Borrar";
            datos["Metodo"] = "DELETE";
            datos["Entidad"] = entidad;

            // Se ejecuta la comunicación.
            this.iComunicaciones = new Comunicaciones();
            var task = this.iComunicaciones.Ejecutar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            // Si no viene valor, se devuelve una entidad vacía.
            if (!respuesta.ContainsKey("Valor"))
                return new Codeudores();

            // Se deserializa la respuesta y se devuelve.
            return JsonConvert.DeserializeObject<Codeudores>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
