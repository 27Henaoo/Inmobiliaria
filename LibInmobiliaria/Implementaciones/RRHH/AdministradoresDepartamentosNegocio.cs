using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.RRHH;
using Microsoft.EntityFrameworkCore;


namespace LibInmobiliaria.Implementaciones.RRHH
{
    public class AdministradoresDepartamentosNegocio : IAdministradoresDepartamentosNegocio
    {
        private string ObtenerDatosAdministradoresDepartamentos(AdministradoresDepartamentos entidad)
        {
            return $"Id: {entidad.Id}, " +
                   $"Cedula: {entidad.Cedula}, " +
                   $"Nombre: {entidad.Nombre}, " +
                   $"Apellido: {entidad.Apellido}, " +
                   $"Genero: {entidad.Genero}, " +
                   $"Correo: {entidad.Correo}, " +
                   $"FechaNacimiento: {entidad.FechaNacimiento}, " +
                   $"FechaRegistro: {entidad.FechaRegistro}, " +
                   $"EstadoCivil: {entidad.EstadoCivil}, " +
                   $"Nacionalidad: {entidad.Nacionalidad}, " +
                   $"_EstadoCivil: {entidad._EstadoCivil}, " +
                   $"_Nacionalidad: {entidad._Nacionalidad}, " +
                   $"Telefonos: {entidad.Telefonos}, " +
                   $"Direcciones: {entidad.Direcciones}, " +
                   $"ExpedientesLaborales: {entidad.ExpedientesLaborales}, " +
                   $"Sueldo: {entidad.Sueldo}, " +
                   $"Estado: {entidad.Estado}, " +
                   $"Jornada: {entidad.Jornada}, " +
                   $"PresupuestoDepartamento: {entidad.PresupuestoDepartamento}, " +
                   $"Departamento: {entidad.Departamento}, " +
                   $"_Departamento: {entidad._Departamento}, " +
                   $"JefesSectores: {entidad.JefesSectores}, ";
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
                Tabla = "AdministradoresDepartamentos",
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

        // Método para consultar todos los AdministradoresDepartamentos.
        public List<AdministradoresDepartamentos> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Se consultan los registros de AdministradoresDepartamentos en forma de lista.
                var lista = this.iConexion.AdministradoresDepartamentos!.Include(x => x._EstadoCivil).Include(x => x._Nacionalidad).Include(x => x.Telefonos).Include(x => x.Direcciones)
                       .Include(x => x.ExpedientesLaborales).Include(x => x._Departamento).Include(x => x.JefesSectores!).ThenInclude(x => x._Sector).ToList();

                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Se consultaron los registros de AdministradoresDepartamentos",
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
                    descripcion: "Fallo al consultar los registros de AdministradoresDepartamentos",
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

        // Método para guardar un avión nuevo.
        public AdministradoresDepartamentos Guardar(AdministradoresDepartamentos entidad)
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

                // Se agrega la entidad al conjunto de AdministradoresDepartamentos.
                this.iConexion.AdministradoresDepartamentos!.Add(entidad);

                AgregarHistorico(
                    accion: "Guardar",
                    registroId: null,
                    descripcion: "Se guardo un nuevo registro de AdministradoresDepartamentos",
                    cambios: "Se creo un nuevo registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosAdministradoresDepartamentos(entidad),
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
                    descripcion: "Fallo al guardar un registro de AdministradoresDepartamentos",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosAdministradoresDepartamentos(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para modificar un avión existente.
        public AdministradoresDepartamentos Modificar(AdministradoresDepartamentos entidad)
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

                var anterior = this.iConexion.AdministradoresDepartamentos!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de AdministradoresDepartamentos no existe");

                string valorAnterior = ObtenerDatosAdministradoresDepartamentos(anterior);
                string valorNuevo = ObtenerDatosAdministradoresDepartamentos(entidad);

                // Se obtiene la entrada de Entity Framework para la entidad recibida.
                var entry = this.iConexion.Entry<AdministradoresDepartamentos>(entidad);

                // Se marca la entidad como modificada.
                entry.State = EntityState.Modified;

                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Se modifico un registro de AdministradoresDepartamentos",
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
                    descripcion: "Fallo al modificar un registro de AdministradoresDepartamentos",
                    cambios: "No se pudo modificar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosAdministradoresDepartamentos(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para borrar un avión existente.
        public AdministradoresDepartamentos Borrar(AdministradoresDepartamentos entidad)
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

                var anterior = this.iConexion.AdministradoresDepartamentos!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de AdministradoresDepartamentos no existe");

                string valorAnterior = ObtenerDatosAdministradoresDepartamentos(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.AdministradoresDepartamentos!.Remove(entidad);

                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Se borro un registro de AdministradoresDepartamentos",
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
                    descripcion: "Fallo al borrar un registro de AdministradoresDepartamentos",
                    cambios: "No se pudo eliminar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosAdministradoresDepartamentos(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
    }
}
