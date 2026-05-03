using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Entidades;
using LibInmobiliaria.Interfaces;

Console.WriteLine("Bienvenido a Inmobiliaria La Recocha ITM");

Console.WriteLine("CONSOLE MAIN");

Console.WriteLine("Cargando Conexion a Base de Datos");
IConexion conexion = new Conexion();
conexion.StringConexion = "Server=JUANES\\DEV;Integrated Security=True;TrustServerCertificate=True;Database=db_inmobiliaria;";

var lista_activosFinancieros = conexion.ActivosFinancieros!.ToList();
var lista_administradoresDepartamentos = conexion.AdministradoresDepartamentos!.ToList();
var lista_bienes = conexion.Bienes!.ToList();
var lista_bienesInmuebles = conexion.BienesInmuebles!.ToList();
var lista_bienesMuebles = conexion.BienesMuebles!.ToList();
var lista_ciudades = conexion.Ciudades!.ToList();
var lista_clientes = conexion.Clientes!.ToList();
var lista_codeudores = conexion.Codeudores!.ToList();
var lista_codeudoresCompradores = conexion.CodeudoresCompradores!.ToList();
var lista_compradores = conexion.Compradores!.ToList();
var lista_contratos = conexion.Contratos!.ToList();
var lista_contratosCodeudores = conexion.ContratosCodeudores!.ToList();
var lista_contratosEmpleados = conexion.ContratosEmpleados!.ToList();
var lista_departamentos = conexion.Departamentos!.ToList();
var lista_direcciones = conexion.Direcciones!.ToList();
var lista_empleadosCompradores = conexion.EmpleadosCompradores!.ToList();
var lista_empleadosSectores = conexion.EmpleadosSectores!.ToList();
var lista_estadosCiviles = conexion.EstadosCiviles!.ToList();
var lista_expedientesFinancieros = conexion.ExpedientesFinancieros!.ToList();
var lista_expedientesLaborales = conexion.ExpedientesLaborales!.ToList();
var lista_JefesSectores = conexion.JefesSectores!.ToList();
var lista_Nacionalidades = conexion.Nacionalidades!.ToList();
var lista_personas = conexion.Personas!.ToList();
var lista_propiedades = conexion.Propiedades!.ToList();
var lista_sectores = conexion.Sectores!.ToList();
var lista_telefonos = conexion.Telefonos!.ToList();
var lista_tiposContratos = conexion.TiposContratos!.ToList();
var lista_tiposPropiedades = conexion.TiposPropiedades!.ToList();
var lista_trabajadores = conexion.Trabajadores!.ToList();


Console.WriteLine("Conexion a Base de Datos Existosa");
