using LibInmobiliaria.Entidades;
using LibInmobiliaria.Implementaciones;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Convenios;
using Microsoft.EntityFrameworkCore;

public class ContratosCodeudoresNegocio : IContratosCodeudoresNegocio
{

    // Variable privada para manejar la conexión a la base de datos.
    private IConexion? iConexion;

    public List<ContratosCodeudores> Consultar()
    {
        // Se crea una nueva conexión.
        this.iConexion = new Conexion();

        // Se asigna la cadena de conexión.
        this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

        // Se consultan todos los registros de ContratosCodeudores y se devuelven en forma de lista.
        return this.iConexion.ContratosCodeudores!.ToList();
    }

    // Método para guardar ContratosCodeudores nuevos.
    public ContratosCodeudores Guardar(ContratosCodeudores entidad)
    {
        // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
        if (entidad.Id != 0)
            throw new Exception("Ya se guardo");

        // Se crea una nueva conexión.
        this.iConexion = new Conexion();

        // Se asigna la cadena de conexión.
        this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

        // Se agrega la entidad al conjunto de ContratosCodeudores.
        this.iConexion.ContratosCodeudores!.Add(entidad);

        // Se guardan los cambios en la base de datos.
        this.iConexion.SaveChanges();

        // Se devuelve la entidad guardada.
        return entidad;
    }

    // Método para modificar ContratosCodeudores existentes.
    public ContratosCodeudores Modificar(ContratosCodeudores entidad)
    {
        // Si el Id es 0, no se puede modificar porque no existe en base de datos.
        if (entidad.Id == 0)
            throw new Exception("No se puede modificar");

        // Se crea una nueva conexión.
        this.iConexion = new Conexion();

        // Se asigna la cadena de conexión.
        this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

        // Se obtiene la entrada de Entity Framework para la entidad recibida.
        var entry = this.iConexion.Entry<ContratosCodeudores>(entidad);

        // Se marca la entidad como modificada.
        entry.State = EntityState.Modified;

        // Se guardan los cambios en la base de datos.
        this.iConexion.SaveChanges();

        // Se devuelve la entidad modificada.
        return entidad;
    }
    // Método para borrar ContratosCodeudores existentes.
    public ContratosCodeudores Borrar(ContratosCodeudores entidad)
    {
        // Si el Id es 0, no se puede borrar porque no existe en base de datos.
        if (entidad.Id == 0)
            throw new Exception("No se puede borrar");

        // Se crea una nueva conexión.
        this.iConexion = new Conexion();

        // Se asigna la cadena de conexión.
        this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

        // Se marca la entidad para eliminarla.
        this.iConexion.ContratosCodeudores!.Remove(entidad);

        // Se guardan los cambios en la base de datos.
        this.iConexion.SaveChanges();

        // Se devuelve la entidad borrada.
        return entidad;
    }
}