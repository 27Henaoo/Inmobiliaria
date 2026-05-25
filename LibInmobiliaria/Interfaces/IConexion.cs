using LibModelos._5._1ModelosComunes;
using LibModelos._5._2LoginRegisterEntidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibInmobiliaria.Interfaces
{
    public interface IConexion
    {
       
        public string? StringConexion { get; set; }

        // Entidades principales
        
        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Trabajadores>? Trabajadores { get; set; }
        public DbSet<AdministradoresDepartamentos>? AdministradoresDepartamentos { get; set; }
        public DbSet<JefesSectores>? JefesSectores { get; set; }
        public DbSet<EmpleadosSectores>? EmpleadosSectores { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Codeudores>? Codeudores { get; set; }
        public DbSet<Compradores>? Compradores { get; set; }

       
        // Catálogos / tablas maestras o tablas padres
        
        public DbSet<EstadosCiviles>? EstadosCiviles { get; set; }
        public DbSet<Nacionalidades>? Nacionalidades { get; set; }
        public DbSet<TiposContratos>? TiposContratos { get; set; }
        public DbSet<TiposPropiedades>? TiposPropiedades { get; set; }
        public DbSet<Departamentos>? Departamentos { get; set; }
        public DbSet<Ciudades>? Ciudades { get; set; }
        public DbSet<Sectores>? Sectores { get; set; }

        
        // Dependen de personas
      
        public DbSet<Telefonos>? Telefonos { get; set; }
        public DbSet<Direcciones>? Direcciones { get; set; }
        public DbSet<ExpedientesLaborales>? ExpedientesLaborales { get; set; }
        public DbSet<ExpedientesFinancieros>? ExpedientesFinancieros { get; set; }

        // Bienes y activos
        
        public DbSet<Bienes>? Bienes { get; set; }
        public DbSet<BienesMuebles>? BienesMuebles { get; set; }
        public DbSet<BienesInmuebles>? BienesInmuebles { get; set; }
        public DbSet<ActivosFinancieros>? ActivosFinancieros { get; set; }

        
        // Negocios principales
       
        public DbSet<Propiedades>? Propiedades { get; set; }
        public DbSet<Contratos>? Contratos { get; set; }

        
        // Tablas puente
     
        public DbSet<EmpleadosCompradores>? EmpleadosCompradores { get; set; }
        public DbSet<ContratosEmpleados>? ContratosEmpleados { get; set; }
        public DbSet<ContratosCodeudores>? ContratosCodeudores { get; set; }
        public DbSet<CodeudoresCompradores>? CodeudoresCompradores { get; set; }

        //Tabla Historicos
        public DbSet<Historicos>? Historicos{ get; set; }

        //PARA LOGIN Y REGISTRO CON ROLES

        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }

        // Métodos heredados de DbContext

        public int SaveChanges();
        //public EntityEntry Entry(object entity);
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class; 
    }
}