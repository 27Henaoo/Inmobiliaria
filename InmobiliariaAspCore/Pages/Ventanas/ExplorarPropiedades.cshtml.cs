using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class ExplorarPropiedadesModel : PageModel
    {
        private IPropiedadesNegocio? iPropiedadesNegocio;

        public List<Propiedades> Lista { get; set; } = new List<Propiedades>();

        public string PropiedadesJson { get; set; } = "[]";

        public int TotalPropiedades { get; set; }

        public int TotalPropiedadesConUbicacion { get; set; }

        public int TotalDisponibles { get; set; }

        public ExplorarPropiedadesModel()
        {
            iPropiedadesNegocio = new PropiedadesNegocio();
        }

        public void OnGet()
        {
            CargarPropiedades();
        }

        private void CargarPropiedades()
        {
            if (iPropiedadesNegocio == null)
                return;

            Lista = iPropiedadesNegocio.Consultar();

            TotalPropiedades = Lista.Count;

            TotalPropiedadesConUbicacion = Lista
                .Count(x => x.Latitud != null && x.Longitud != null);

            TotalDisponibles = Lista
                .Count(x => x.Estado != null &&
                            x.Estado.ToLower().Contains("disponible"));

            var datos = Lista
                .Where(x => x.Latitud != null && x.Longitud != null)
                .Select(x => new
                {
                    id = x.Id,
                    numeroHabitaciones = x.NumeroHabitaciones,
                    numeroBanos = x.NumeroBanos,
                    pisos = x.Pisos,
                    entradas = x.Entradas,
                    estado = x.Estado,
                    valorPropiedad = x.ValorPropiedad,
                    valorArriendo = x.ValorArriendo,
                    direccion = x.Direccion ?? "",
                    latitud = x.Latitud,
                    longitud = x.Longitud,
                    imagen = x.Imagen ?? "",
                    tipoPropiedad = x._TipoPropiedad != null ? x._TipoPropiedad.Nombre : "Sin tipo"
                })
                .ToList();

            PropiedadesJson = JsonSerializer.Serialize(datos);
        }
    }
}