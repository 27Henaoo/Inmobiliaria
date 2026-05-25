
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Seguridad;
using LibModelos._5._2LoginRegisterEntidades;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Seguridad
{
    public class RolesNegocio : IRolesNegocio
    {
        private IConexion? iConexion;

        public List<Roles> Consultar()
        {
            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            return this.iConexion.Roles!.ToList();
        }

        public Roles Guardar(Roles entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Roles!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        public Roles Modificar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entry = this.iConexion.Entry<Roles>(entidad);

            entry.State = EntityState.Modified;

            this.iConexion.SaveChanges();

            return entidad;
        }

        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Roles!.Remove(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }
    }
}
