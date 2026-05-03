using LibInmobiliaria.Entidades;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<ActivosFinancieros>? ActivosFinancieros { get; set; }
        public DbSet<AdministradoresDepartamentos>? AdministradoresDepartamentos { get; set; }
        public DbSet<Bienes>? Bienes { get; set; }
        public DbSet<BienesInmuebles>? BienesInmuebles { get; set; }
        public DbSet<BienesMuebles>? BienesMuebles { get; set; }
        public DbSet<Ciudades>? Ciudades { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Codeudores>? Codeudores { get; set; }
        public DbSet<CodeudoresCompradores>? CodeudoresCompradores { get; set; }
        public DbSet<Compradores>? Compradores { get; set; }
        public DbSet<Contratos>? Contratos { get; set; }
        public DbSet<ContratosCodeudores>? ContratosCodeudores { get; set; }
        public DbSet<ContratosEmpleados>? ContratosEmpleados { get; set; }
        public DbSet<Departamentos>? Departamentos { get; set; }
        public DbSet<Direcciones>? Direcciones { get; set; }
        public DbSet<EmpleadosCompradores>? EmpleadosCompradores { get; set; }
        public DbSet<EmpleadosSectores>? EmpleadosSectores { get; set; }
        public DbSet<EstadosCiviles>? EstadosCiviles { get; set; }
        public DbSet<ExpedientesFinancieros>? ExpedientesFinancieros { get; set; }
        public DbSet<ExpedientesLaborales>? ExpedientesLaborales { get; set; }
        public DbSet<JefesSectores>? JefesSectores { get; set; }
        public DbSet<Nacionalidades>? Nacionalidades { get; set; }
        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Propiedades>? Propiedades { get; set; }
        public DbSet<Sectores>? Sectores { get; set; }
        public DbSet<Telefonos>? Telefonos { get; set; }
        public DbSet<TiposContratos>? TiposContratos { get; set; }
        public DbSet<TiposPropiedades>? TiposPropiedades { get; set; }
        public DbSet<Trabajadores>? Trabajadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HERENCIA DE PERSONAS -> TPT
            modelBuilder.Entity<Personas>().UseTptMappingStrategy();
            modelBuilder.Entity<Personas>().ToTable("Personas");

            modelBuilder.Entity<Trabajadores>().ToTable("Trabajadores", t =>
            {
                t.Property(x => x.Id).HasColumnName("Persona");
            });

            modelBuilder.Entity<AdministradoresDepartamentos>().ToTable("AdministradoresDepartamentos", t =>
            {
                t.Property(x => x.Id).HasColumnName("Persona");
            });

            modelBuilder.Entity<JefesSectores>().ToTable("JefesSectores", t =>
            {
                t.Property(x => x.Id).HasColumnName("Persona");
            });

            modelBuilder.Entity<EmpleadosSectores>().ToTable("EmpleadosSectores", t =>
            {
                t.Property(x => x.Id).HasColumnName("Persona");
            });

            modelBuilder.Entity<Clientes>().ToTable("Clientes", t =>
            {
                t.Property(x => x.Id).HasColumnName("Persona");
            });

            modelBuilder.Entity<Codeudores>().ToTable("Codeudores", t =>
            {
                t.Property(x => x.Id).HasColumnName("Persona");
            });

            modelBuilder.Entity<Compradores>().ToTable("Compradores", t =>
            {
                t.Property(x => x.Id).HasColumnName("Persona");
            });

            // HERENCIA DE BIENES -> TPT
            modelBuilder.Entity<Bienes>().UseTptMappingStrategy();
            modelBuilder.Entity<Bienes>().ToTable("Bienes");

            modelBuilder.Entity<BienesMuebles>().ToTable("BienesMuebles", t =>
            {
                t.Property(x => x.Id).HasColumnName("Bien");
            });

            modelBuilder.Entity<BienesInmuebles>().ToTable("BienesInmuebles", t =>
            {
                t.Property(x => x.Id).HasColumnName("Bien");
            });

            // RELACIÓN 1:1 DEPARTAMENTOS <-> ADMINISTRADORESDEPARTAMENTOS
            modelBuilder.Entity<Departamentos>()
                .HasOne(d => d._AdministradorDepartamento)
                .WithOne(a => a._Departamento)
                .HasForeignKey<AdministradoresDepartamentos>(a => a.Departamento)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:1 SECTORES <-> JEFESSECTORES
            modelBuilder.Entity<Sectores>()
                .HasOne(s => s._JefeSector)
                .WithOne(j => j._Sector)
                .HasForeignKey<JefesSectores>(j => j.Sector)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N ADMINISTRADORESDEPARTAMENTOS -> JEFESSECTORES
            modelBuilder.Entity<JefesSectores>()
                .HasOne(j => j._AdministradorDepartamento)
                .WithMany(a => a.JefesSectores)
                .HasForeignKey(j => j.AdministradorDepartamento)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N JEFESSECTORES -> EMPLEADOSSECTORES
            modelBuilder.Entity<EmpleadosSectores>()
                .HasOne(e => e._JefeSector)
                .WithMany(j => j.EmpleadosSectores)
                .HasForeignKey(e => e.JefeSector)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N SECTORES -> EMPLEADOSSECTORES
            modelBuilder.Entity<EmpleadosSectores>()
                .HasOne(e => e._Sector)
                .WithMany(s => s.EmpleadosSectores)
                .HasForeignKey(e => e.Sector)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N TIPOSCONTRATOS -> EMPLEADOSSECTORES
            modelBuilder.Entity<EmpleadosSectores>()
                .HasOne(e => e._TipoContrato)
                .WithMany(t => t.EmpleadosSectores)
                .HasForeignKey(e => e.TipoContrato)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:1 CLIENTES <-> EXPEDIENTESFINANCIEROS
            modelBuilder.Entity<Clientes>()
                .HasOne(c => c._ExpedienteFinanciero)
                .WithOne(e => e._Persona)
                .HasForeignKey<ExpedientesFinancieros>(e => e.Persona)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N EXPEDIENTESFINANCIEROS -> BIENES
            modelBuilder.Entity<Bienes>()
                .HasOne(b => b._ExpedienteFinanciero)
                .WithMany(e => e.Bienes)
                .HasForeignKey(b => b.ExpedienteFinanciero)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N EXPEDIENTESFINANCIEROS -> ACTIVOSFINANCIEROS
            modelBuilder.Entity<ActivosFinancieros>()
                .HasOne(a => a._ExpedienteFinanciero)
                .WithMany(e => e.ActivosFinancieros)
                .HasForeignKey(a => a.ExpedienteFinanciero)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N CLIENTES -> PROPIEDADES
            modelBuilder.Entity<Propiedades>()
                .HasOne(p => p._Cliente)
                .WithMany()
                .HasForeignKey(p => p.Cliente)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N TIPOSPROPIEDADES -> PROPIEDADES
            modelBuilder.Entity<Propiedades>()
                .HasOne(p => p._TipoPropiedad)
                .WithMany(t => t.Propiedades)
                .HasForeignKey(p => p.TipoPropiedad)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N CLIENTES -> CONTRATOS
            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._Cliente)
                .WithMany()
                .HasForeignKey(c => c.Cliente)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N PROPIEDADES -> CONTRATOS
            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._Propiedad)
                .WithMany(p => p.Contratos)
                .HasForeignKey(c => c.Propiedad)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N COMPRADORES -> CONTRATOS
            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._Comprador)
                .WithMany(cmp => cmp.Contratos)
                .HasForeignKey(c => c.Comprador)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N JEFESSECTORES -> CONTRATOS
            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._JefeSector)
                .WithMany(j => j.Contratos)
                .HasForeignKey(c => c.JefeSector)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: EMPLEADOSCOMPRADORES
            modelBuilder.Entity<EmpleadosCompradores>()
                .HasOne(ec => ec._Comprador)
                .WithMany(c => c.EmpleadosCompradores)
                .HasForeignKey(ec => ec.Comprador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmpleadosCompradores>()
                .HasOne(ec => ec._Empleado)
                .WithMany(e => e.EmpleadosCompradores)
                .HasForeignKey(ec => ec.Empleado)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: CONTRATOSEMPLEADOS
            modelBuilder.Entity<ContratosEmpleados>()
                .HasOne(ce => ce._Empleado)
                .WithMany(e => e.ContratosEmpleados)
                .HasForeignKey(ce => ce.Empleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContratosEmpleados>()
                .HasOne(ce => ce._Contrato)
                .WithMany(c => c.ContratosEmpleados)
                .HasForeignKey(ce => ce.Contrato)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: CONTRATOSCODEUDORES
            modelBuilder.Entity<ContratosCodeudores>()
                .HasOne(cc => cc._Codeudor)
                .WithMany(c => c.ContratosCodeudores)
                .HasForeignKey(cc => cc.Codeudor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContratosCodeudores>()
                .HasOne(cc => cc._Contrato)
                .WithMany(c => c.ContratosCodeudores)
                .HasForeignKey(cc => cc.Contrato)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: CODEUDORESCOMPRADORES
            modelBuilder.Entity<CodeudoresCompradores>()
                .HasOne(cc => cc._Comprador)
                .WithMany(c => c.CodeudoresCompradores)
                .HasForeignKey(cc => cc.Comprador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CodeudoresCompradores>()
                .HasOne(cc => cc._Codeudor)
                .WithMany(c => c.CodeudoresCompradores)
                .HasForeignKey(cc => cc.Codeudor)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}

//ANTES DE CORREGIR Con nuevo SQL para mejores relaciones
/*using LibInmobiliaria.Entidades;
using LibInmobiliaria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        // =========================
        // Cadena de conexión
        // =========================
        public string? StringConexion { get; set; }

        // =========================
        // Configuración del motor
        // =========================
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });

            // Dejamos el mismo comportamiento que ya venías usando.
            // Esto ayuda a que el contexto no quede rastreando todo automáticamente.
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        // =========================
        // DBSETS
        // =========================
        public DbSet<ActivosFinancieros>? ActivosFinancieros { get; set; }
        public DbSet<AdministradoresDepartamentos>? AdministradoresDepartamentos { get; set; }
        public DbSet<Bienes>? Bienes { get; set; }
        public DbSet<BienesInmuebles>? BienesInmuebles { get; set; }
        public DbSet<BienesMuebles>? BienesMuebles { get; set; }
        public DbSet<Ciudades>? Ciudades { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Codeudores>? Codeudores { get; set; }
        public DbSet<CodeudoresCompradores>? CodeudoresCompradores { get; set; }
        public DbSet<Compradores>? Compradores { get; set; }
        public DbSet<Contratos>? Contratos { get; set; }
        public DbSet<ContratosCodeudores>? ContratosCodeudores { get; set; }
        public DbSet<ContratosEmpleados>? ContratosEmpleados { get; set; }
        public DbSet<Departamentos>? Departamentos { get; set; }
        public DbSet<Direcciones>? Direcciones { get; set; }
        public DbSet<EmpleadosCompradores>? EmpleadosCompradores { get; set; }
        public DbSet<EmpleadosSectores>? EmpleadosSectores { get; set; }
        public DbSet<EstadosCiviles>? EstadosCiviles { get; set; }
        public DbSet<ExpedientesFinancieros>? ExpedientesFinancieros { get; set; }
        public DbSet<ExpedientesLaborales>? ExpedientesLaborales { get; set; }
        public DbSet<JefesSectores>? JefesSectores { get; set; }
        public DbSet<Nacionalidades>? Nacionalidades { get; set; }
        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Propiedades>? Propiedades { get; set; }
        public DbSet<Sectores>? Sectores { get; set; }
        public DbSet<Telefonos>? Telefonos { get; set; }
        public DbSet<TiposContratos>? TiposContratos { get; set; }
        public DbSet<TiposPropiedades>? TiposPropiedades { get; set; }
        public DbSet<Trabajadores>? Trabajadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HERENCIA DE PERSONAS -> TPT

            //Cada nivel de la herencia tiene su propia tabla.
            modelBuilder.Entity<Personas>().ToTable("Personas");
            modelBuilder.Entity<Trabajadores>().ToTable("Trabajadores");
            modelBuilder.Entity<AdministradoresDepartamentos>().ToTable("AdministradoresDepartamentos");
            modelBuilder.Entity<JefesSectores>().ToTable("JefesSectores");
            modelBuilder.Entity<EmpleadosSectores>().ToTable("EmpleadosSectores");
            modelBuilder.Entity<Clientes>().ToTable("Clientes");
            modelBuilder.Entity<Codeudores>().ToTable("Codeudores");
            modelBuilder.Entity<Compradores>().ToTable("Compradores");

            // En SQL las tablas hijas usan "Persona" como PK/FK.
            // En C# seguimos usando Id heredado de Personas.
            // Aquí le decimos a EF que el Id se llama "Persona" en esas tablas.
            modelBuilder.Entity<Trabajadores>().Property(x => x.Id).HasColumnName("Persona");
            modelBuilder.Entity<AdministradoresDepartamentos>().Property(x => x.Id).HasColumnName("Persona");
            modelBuilder.Entity<JefesSectores>().Property(x => x.Id).HasColumnName("Persona");
            modelBuilder.Entity<EmpleadosSectores>().Property(x => x.Id).HasColumnName("Persona");
            modelBuilder.Entity<Clientes>().Property(x => x.Id).HasColumnName("Persona");
            modelBuilder.Entity<Codeudores>().Property(x => x.Id).HasColumnName("Persona");
            modelBuilder.Entity<Compradores>().Property(x => x.Id).HasColumnName("Persona");

            // HERENCIA DE BIENES -> TPT
            // Personas: Bienes base y dos tablas hijas.
            modelBuilder.Entity<Bienes>().ToTable("Bienes");
            modelBuilder.Entity<BienesMuebles>().ToTable("BienesMuebles");
            modelBuilder.Entity<BienesInmuebles>().ToTable("BienesInmuebles");

            // En SQL las tablas hijas usan "Bien" como PK/FK.
            // En C# seguimos usando Id heredado de Bienes.
            modelBuilder.Entity<BienesMuebles>().Property(x => x.Id).HasColumnName("Bien");
            modelBuilder.Entity<BienesInmuebles>().Property(x => x.Id).HasColumnName("Bien");

            // RELACIÓN 1:1 DEPARTAMENTOS <-> ADMINISTRADORESDEPARTAMENTOS
          
            modelBuilder.Entity<Departamentos>()
                .HasOne(d => d._AdministradorDepartamento)
                .WithOne(a => a._Departamento)
                .HasForeignKey<AdministradoresDepartamentos>(a => a.Departamento)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:1 SECTORES <-> JEFESSECTORES

            modelBuilder.Entity<Sectores>()
                .HasOne(s => s._JefeSector)
                .WithOne(j => j._Sector)
                .HasForeignKey<JefesSectores>(j => j.Sector)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N ADMINISTRADORESDEPARTAMENTOS -> JEFESSECTORES

            modelBuilder.Entity<JefesSectores>()
                .HasOne(j => j._AdministradorDepartamento)
                .WithMany(a => a.JefesSectores)
                .HasForeignKey(j => j.AdministradorDepartamento)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N JEFESSECTORES -> EMPLEADOSSECTORES

            modelBuilder.Entity<EmpleadosSectores>()
                .HasOne(e => e._JefeSector)
                .WithMany(j => j.EmpleadosSectores)
                .HasForeignKey(e => e.JefeSector)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N SECTORES -> EMPLEADOSSECTORES

            modelBuilder.Entity<EmpleadosSectores>()
                .HasOne(e => e._Sector)
                .WithMany(s => s.EmpleadosSectores)
                .HasForeignKey(e => e.Sector)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N TIPOSCONTRATOS -> EMPLEADOSSECTORES

            modelBuilder.Entity<EmpleadosSectores>()
                .HasOne(e => e._TipoContrato)
                .WithMany(t => t.EmpleadosSectores)
                .HasForeignKey(e => e.TipoContrato)
                .OnDelete(DeleteBehavior.Restrict);


            // RELACIÓN 1:1 CLIENTES <-> EXPEDIENTESFINANCIEROS

            // En el SQL, ExpedientesFinancieros apunta a Clientes.
            modelBuilder.Entity<Clientes>()
                .HasOne(c => c._ExpedienteFinanciero)
                .WithOne(e => e._Persona)
                .HasForeignKey<ExpedientesFinancieros>(e => e.Persona)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N EXPEDIENTESFINANCIEROS -> BIENES

            modelBuilder.Entity<Bienes>()
                .HasOne(b => b._ExpedienteFinanciero)
                .WithMany(e => e.Bienes)
                .HasForeignKey(b => b.ExpedienteFinanciero)
                .OnDelete(DeleteBehavior.Restrict);


            // RELACIÓN 1:N EXPEDIENTESFINANCIEROS -> ACTIVOSFINANCIEROS

            modelBuilder.Entity<ActivosFinancieros>()
                .HasOne(a => a._ExpedienteFinanciero)
                .WithMany(e => e.ActivosFinancieros)
                .HasForeignKey(a => a.ExpedienteFinanciero)
                .OnDelete(DeleteBehavior.Restrict);


            // RELACIÓN 1:N CLIENTES -> PROPIEDADES

            modelBuilder.Entity<Propiedades>()
                .HasOne(p => p._Cliente)
                .WithMany()
                .HasForeignKey(p => p.Cliente)
                .OnDelete(DeleteBehavior.Restrict);


            // RELACIÓN 1:N TIPOSPROPIEDADES -> PROPIEDADES

            modelBuilder.Entity<Propiedades>()
                .HasOne(p => p._TipoPropiedad)
                .WithMany(t => t.Propiedades)
                .HasForeignKey(p => p.TipoPropiedad)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N CLIENTES -> CONTRATOS

            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._Cliente)
                .WithMany()
                .HasForeignKey(c => c.Cliente)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N PROPIEDADES -> CONTRATOS

            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._Propiedad)
                .WithMany(p => p.Contratos)
                .HasForeignKey(c => c.Propiedad)
                .OnDelete(DeleteBehavior.Restrict);


            // RELACIÓN 1:N COMPRADORES -> CONTRATOS

            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._Comprador)
                .WithMany(cmp => cmp.Contratos)
                .HasForeignKey(c => c.Comprador)
                .OnDelete(DeleteBehavior.Restrict);

            // RELACIÓN 1:N JEFESSECTORES -> CONTRATOS

            modelBuilder.Entity<Contratos>()
                .HasOne(c => c._JefeSector)
                .WithMany(j => j.Contratos)
                .HasForeignKey(c => c.JefeSector)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: EMPLEADOSCOMPRADORES

            modelBuilder.Entity<EmpleadosCompradores>()
                .HasOne(ec => ec._Comprador)
                .WithMany(c => c.EmpleadosCompradores)
                .HasForeignKey(ec => ec.Comprador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmpleadosCompradores>()
                .HasOne(ec => ec._Empleado)
                .WithMany(e => e.EmpleadosCompradores)
                .HasForeignKey(ec => ec.Empleado)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: CONTRATOSEMPLEADOS

            modelBuilder.Entity<ContratosEmpleados>()
                .HasOne(ce => ce._Empleado)
                .WithMany(e => e.ContratosEmpleados)
                .HasForeignKey(ce => ce.Empleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContratosEmpleados>()
                .HasOne(ce => ce._Contrato)
                .WithMany(c => c.ContratosEmpleados)
                .HasForeignKey(ce => ce.Contrato)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: CONTRATOSCODEUDORES

            modelBuilder.Entity<ContratosCodeudores>()
                .HasOne(cc => cc._Codeudor)
                .WithMany(c => c.ContratosCodeudores)
                .HasForeignKey(cc => cc.Codeudor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContratosCodeudores>()
                .HasOne(cc => cc._Contrato)
                .WithMany(c => c.ContratosCodeudores)
                .HasForeignKey(cc => cc.Contrato)
                .OnDelete(DeleteBehavior.Restrict);

            // TABLA PUENTE: CODEUDORESCOMPRADORES

            modelBuilder.Entity<CodeudoresCompradores>()
                .HasOne(cc => cc._Comprador)
                .WithMany(c => c.CodeudoresCompradores)
                .HasForeignKey(cc => cc.Comprador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CodeudoresCompradores>()
                .HasOne(cc => cc._Codeudor)
                .WithMany(c => c.CodeudoresCompradores)
                .HasForeignKey(cc => cc.Codeudor)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}*/