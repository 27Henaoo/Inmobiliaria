using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Convenios;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Convenios
{
    public class PropiedadesNegocio : IPropiedadesNegocio
    {
        private string ObtenerDatosPropiedades(Propiedades entidad)
        {
            return $"Id: {entidad.Id}, " +
                   $"NumeroHabitaciones: {entidad.NumeroHabitaciones}, " +
                   $"NumeroBanos: {entidad.NumeroBanos}, " +
                   $"Patio: {entidad.Patio}, " +
                   $"Entradas: {entidad.Entradas}, " +
                   $"Pisos: {entidad.Pisos}, " +
                   $"AnioConstruccion: {entidad.AnioConstruccion}, " +
                   $"ValorPropiedad: {entidad.ValorPropiedad}, " +
                   $"ValorArriendo: {entidad.ValorArriendo}, " +
                   $"Estado: {entidad.Estado}, " +
                   $"Direccion: {entidad.Direccion}, " +
                   $"Latitud: {entidad.Latitud}, " +
                   $"Longitud: {entidad.Longitud}, " +
                   $"Imagen: {entidad.Imagen}, " +
                   $"Cliente: {entidad.Cliente}, " +
                   $"TipoPropiedad: {entidad.TipoPropiedad}, " +
                   $"_Cliente: {entidad._Cliente}, " +
                   $"_TipoPropiedad: {entidad._TipoPropiedad}, " +
                   $"Contratos: {entidad.Contratos}, ";
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
                Tabla = "Propiedades",
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
        }



        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        public List<Propiedades> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            try
            {
                // Se consultan los registros de Propiedades en forma de lista.
                var lista = this.iConexion.Propiedades!.Include(x => x._Cliente!).ThenInclude(x => x._EstadoCivil).Include(x => x._Cliente!)
                                 .ThenInclude(x => x._Nacionalidad).Include(x => x._Cliente!).ThenInclude(x => x.Telefonos).Include(x => x._Cliente!)
                                 .ThenInclude(x => x.Direcciones).Include(x => x._Cliente!).ThenInclude(x => x.ExpedientesLaborales).Include(x => x._TipoPropiedad)
                                 .Include(x => x.Contratos!).ThenInclude(x => x._Comprador).Include(x => x.Contratos!).ThenInclude(x => x._JefeSector).ToList();

                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Se consultaron los registros de Propiedades",
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
                    descripcion: "Fallo al consultar los registros de Propiedades",
                    cambios: "No se pudo consultar la lista",
                    valorAnterior: "Error al Consultar",
                    valorNuevo: "Error al Consultar",
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para guardar Propiedades nuevos.
        public Propiedades Guardar(Propiedades entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            try
            {
                // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
                if (entidad.Id != 0)
                    throw new Exception("Ya se guardo");

                // Se agrega la entidad al conjunto de Propiedades.
                this.iConexion.Propiedades!.Add(entidad);

                AgregarHistorico(
                    accion: "Guardar",
                    registroId: null,
                    descripcion: "Se guardo un nuevo registro de Propiedades",
                    cambios: "Se creo un nuevo registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosPropiedades(entidad),
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
                    descripcion: "Fallo al guardar un registro de Propiedades",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosPropiedades(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para modificar Propiedades existentes.
        public Propiedades Modificar(Propiedades entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            try
            {
                // Si el Id es 0, no se puede modificar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede modificar");

                var anterior = this.iConexion.Propiedades!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de Propiedades no existe");

                string valorAnterior = ObtenerDatosPropiedades(anterior);
                string valorNuevo = ObtenerDatosPropiedades(entidad);

                // Se obtiene la entrada de Entity Framework para la entidad recibida.
                var entry = this.iConexion.Entry<Propiedades>(entidad);

                // Se marca la entidad como modificada.
                entry.State = EntityState.Modified;

                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Se modifico un registro de Propiedades",
                    cambios: "Se cambio la informacion del registro",
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
                    descripcion: "Fallo al modificar un registro de Propiedades",
                    cambios: "No se pudo modificar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosPropiedades(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
        // Método para borrar Propiedades existentes.
        public Propiedades Borrar(Propiedades entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            try
            {
                // Si el Id es 0, no se puede borrar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede borrar");

                var anterior = this.iConexion.Propiedades!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de Propiedades no existe");

                string valorAnterior = ObtenerDatosPropiedades(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.Propiedades!.Remove(entidad);

                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Se borro un registro de Propiedades",
                    cambios: "Se elimino el registro",
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
                    descripcion: "Fallo al borrar un registro de Propiedades",
                    cambios: "No se pudo eliminar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosPropiedades(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
    }
}