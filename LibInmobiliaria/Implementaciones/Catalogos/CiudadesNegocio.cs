
using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Catalogo;
using Microsoft.EntityFrameworkCore;


namespace LibInmobiliaria.Implementaciones.Catalogos
{
    public class CiudadesNegocio : ICiudadesNegocio
    {
        private string ObtenerDatosCiudad(Ciudades entidad) //Convierte un Galaxiaito a puro texto papi!
        {
            return $"Id: {entidad.Id}, " +
                   $"Nombre: {entidad.Nombre}, " +
                   $"Estado: {entidad.Estado}, " +
                   $"Poblacion: {entidad.Poblacion}, " +
                   $"FechaCreacion: {entidad.FechaCreacion}, " +
                   $"CodigoPostal: {entidad.CodigoPostal}, " +
                   $"Departamento: {entidad._Departamento}, "+
                   $"Sectores: {entidad._Sectores}, ";
        }

        private void AgregarHistorico(
            string accion,
            int? registroId,
            string descripcion,
            string? cambios,
            string? valorAnterior,
            string? valorNuevo,
            bool exitoso,
            string? error)
        {
            this.iConexion!.Historicos!.Add(new Historicos()
            {
                Usuario = "ADMIN",
                Tabla = "Ciudad",
                Accion = accion,
                RegistroId = registroId,
                Descripcion = descripcion,
                Cambios = cambios,
                ValorAnterior = valorAnterior,
                ValorNuevo = valorNuevo,
                Origen = "Inmobiliaria.Api",
                Exitoso = exitoso,
                Error = error,
                Fecha = DateTime.Now
            });
        } // reemplaza el this.iConexion.Historicos.Add(new Historicos() { ... }) que usabamos;

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        // Método para consultar todos los Ciudades.
        public List<Ciudades> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                //Se consultan los registros de Ciudades en forma de lista
                var lista = this.iConexion.Ciudades!.ToList();
                AgregarHistorico(
                  accion: "Consultar",
                  registroId: null,
                  descripcion: "Se consultaron los Ciudad",
                  cambios: "No se modificaron datos",
                  valorAnterior: null,
                  valorNuevo: $"Cantidad de registros consultados: {lista.Count}",
                  exitoso: true,
                  error: "N/A"
                  );

                this.iConexion.SaveChanges();
                return lista;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Fallo al consultar los Ciudades",
                    cambios: "No se pudo consultar la lista",
                    valorAnterior: "Erro al Consultar",
                    valorNuevo: "Error al Consultar",
                    exitoso: false,
                    error: ex.Message
                    );

                this.iConexion.SaveChanges();
                throw;
            }

        }

        // Método para guardar new Ciudades.
        public Ciudades Guardar(Ciudades entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
                if (entidad.Id != 0)
                    throw new Exception("Ya se guardo");
                // Se agrega la entidad al conjunto de Ciudades.
                this.iConexion.Ciudades!.Add(entidad);

                AgregarHistorico(
                     accion: "Guardar",
                     registroId: null,
                     descripcion: "Se guardo un nueva Ciudad",
                     cambios: "Se creo un nuevo registro",
                     valorAnterior: null,
                     valorNuevo: ObtenerDatosCiudad(entidad),
                     exitoso: true,
                     error: "N/A"
                 );

                // Se guardan los cambios en la base de datos.
                this.iConexion.SaveChanges();
                // Se devuelve la entidad guardada.
                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                   accion: "Guardar",
                   registroId: entidad.Id,
                   descripcion: "Fallo al guardar una Ciudad",
                   cambios: "No se pudo crear el registro",
                   valorAnterior: null,
                   valorNuevo: ObtenerDatosCiudad(entidad),
                   exitoso: false,
                   error: ex.Message
               );

                throw;
            }   
        }

        // Método para modificar un avión existente.
        public Ciudades Modificar(Ciudades entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");
            try
            {
                // Si el Id es 0, no se puede modificar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede modificar");
                var anterior = this.iConexion.Ciudades!.FirstOrDefault(x => x.Id == entidad.Id);//Buscamos en la tabla Ciudad primer Ciudad donde
                                                                                                //Id sea igual al Id que se va a modificar.
                if (anterior == null)
                    throw new Exception("La Ciudad No existe");
                string valorAnterior = ObtenerDatosCiudad(anterior); //Convertimos para pasarle el original
                string valorNuevo = ObtenerDatosCiudad(entidad);//Enviamos lo que se modifico
                                                                // Se obtiene la entrada de Entity Framework para la entidad recibida.

                var entry = this.iConexion.Entry<Ciudades>(entidad);
                // Se marca la entidad como modificada.
                entry.State = EntityState.Modified;
                AgregarHistorico(
                  accion: "Modificar",
                  registroId: entidad.Id,
                  descripcion: "Se modifica una Ciudad",
                  cambios: "Se cambio la informacion de la Ciudad",
                  valorAnterior: valorAnterior,
                  valorNuevo: valorNuevo,
                  exitoso: true,
                  error: "N/A"
                   );

                // Se guardan los cambios en la base de datos.
                this.iConexion.SaveChanges();

                // Se devuelve la entidad modificada.
                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                  accion: "Modificar",
                  registroId: entidad.Id,
                  descripcion: "Fallo al modificar una Ciudad",
                  cambios: "No se pudo modificar el registro",
                  valorAnterior: null,
                  valorNuevo: ObtenerDatosCiudad(entidad),
                  exitoso: false,
                  error: ex.Message
                  );

                this.iConexion.SaveChanges();
                throw;
            }   

        }

        // Método para borrar un avión existente.
        public Ciudades Borrar(Ciudades entidad)
        {

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Si el Id es 0, no se puede borrar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede borrar");
                var anterior = this.iConexion.Ciudades!.FirstOrDefault(x => x.Id == entidad.Id);//Buscamos en la tabla Ciudad primer Ciudad donde
                                                                                                //Id sea igual al Id que se va a modificar.
                if (anterior == null)
                    throw new Exception("La Ciudad No existe");
                string valorAnterior = ObtenerDatosCiudad(anterior); //Convertimos para pasarle el original
                                                                     // Se marca la entidad para eliminarla.
                this.iConexion.Ciudades!.Remove(entidad);

                AgregarHistorico(
                 accion: "Borrar",
                 registroId: entidad.Id,
                 descripcion: "Se borro una Piscina",
                 cambios: "Se eliminó el registro",
                 valorAnterior: valorAnterior,
                 valorNuevo: null,
                 exitoso: true,
                 error: "N/A"
              );
                // Se guardan los cambios en la base de datos.
                this.iConexion.SaveChanges();

                // Se devuelve la entidad borrada.
                return entidad;

            }
            catch (Exception ex)
            {
                AgregarHistorico(
                accion: "Borrar",
                registroId: entidad.Id,
                descripcion: "Fallo al borrar una Ciudad",
                cambios: "No se pudo eliminar el registro",
                valorAnterior: null,
                valorNuevo: ObtenerDatosCiudad(entidad),
                exitoso: false,
                error: ex.Message
                );

                this.iConexion.SaveChanges();

                throw;
            }
            
            
        }
    }
}
