using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class MapaPropiedadesModel : PageModel
    {
        private IPropiedadesNegocio? iPropiedadesNegocio;

        public string PropiedadesJson { get; set; } = "[]";

        public int TotalPropiedadesConUbicacion { get; set; }

        public MapaPropiedadesModel()
        {
            iPropiedadesNegocio = new PropiedadesNegocio();
        }

        public void OnGet()
        {
            CargarMapa();
        }

        private void CargarMapa()
        {
            if (iPropiedadesNegocio == null)
                return;

            var lista = iPropiedadesNegocio.Consultar();

            var datos = lista
                .Where(x => x.Latitud != null && x.Longitud != null)
                .Select(x => new
                {
                    id = x.Id,
                    numeroHabitaciones = x.NumeroHabitaciones,
                    numeroBanos = x.NumeroBanos,
                    estado = x.Estado,
                    valorPropiedad = x.ValorPropiedad,
                    valorArriendo = x.ValorArriendo,
                    direccion = x.Direccion ?? "",
                    latitud = x.Latitud,
                    longitud = x.Longitud,
                    tipoPropiedad = x._TipoPropiedad != null ? x._TipoPropiedad.Nombre : "Sin tipo",
                    cliente = x._Cliente != null ? x._Cliente.Nombre + " " + x._Cliente.Apellido : "Sin cliente"
                })
                .ToList();

            TotalPropiedadesConUbicacion = datos.Count;

            PropiedadesJson = JsonSerializer.Serialize(datos);
        }
    }
}