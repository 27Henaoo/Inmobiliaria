using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class PropiedadesModel : PageModel
    {
        // Para combos

        private IClientesNegocio? iClientesNegocio;
        public List<Clientes> Clientes { get; set; } = new List<Clientes>();

        private ITiposPropiedadesNegocio? iTiposPropiedadesNegocio;
        public List<TiposPropiedades> TiposPropiedades { get; set; } = new List<TiposPropiedades>();

        private IPropiedadesNegocio? iPropiedadesNegocio;

        [BindProperty] public List<Propiedades>? Lista { get; set; }
        [BindProperty] public Propiedades? Propiedad { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PropiedadesModel()
        {
            iPropiedadesNegocio = new PropiedadesNegocio();
            iClientesNegocio = new ClientesNegocio();
            iTiposPropiedadesNegocio = new TiposPropiedadesNegocio();
        }

        private void CargarCombos()
        {
            Clientes = iClientesNegocio!.Consultar();
            TiposPropiedades = iTiposPropiedadesNegocio!.Consultar();
        }

        private void ValidarCombos()
        {
            if (Propiedad == null)
                return;

            if (Propiedad.Cliente == 0)
                throw new Exception("Debe seleccionar un cliente.");

            if (Propiedad.TipoPropiedad == 0)
                throw new Exception("Debe seleccionar un tipo de propiedad.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iPropiedadesNegocio == null)
                    return;

                Lista = iPropiedadesNegocio.Consultar();
                CargarCombos();
                Propiedad = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            CargarCombos();

            Propiedad = new Propiedades()
            {
                AnioConstruccion = DateTime.Now,
                Estado = "Disponible"
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                Propiedad = Lista!.FirstOrDefault(x => x.Id == data);

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Propiedad == null)
                    return;

                ValidarCombos();

                if (Propiedad.Id == 0)
                    Propiedad = iPropiedadesNegocio!.Guardar(Propiedad!);
                else
                    Propiedad = iPropiedadesNegocio!.Modificar(Propiedad!);

                if (Propiedad.Id == 0)
                    return;

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Propiedad == null)
                    return;

                Propiedad = iPropiedadesNegocio!.Borrar(Propiedad!);

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();

                Propiedad = Lista!.FirstOrDefault(x => x.Id == data);

                Lista = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }

        //Para generar PDF y Excel de los datos
        public IActionResult OnPostBtExportarExcel()
        {
            try
            {
                if (iPropiedadesNegocio == null)
                    return Page();

                var lista = iPropiedadesNegocio.Consultar();

                var sb = new StringBuilder();

                sb.AppendLine("NumeroHabitaciones;NumeroBanos;Patio;Entradas;Pisos;AnioConstruccion;ValorPropiedad;ValorArriendo;Estado;TipoPropiedad;Cliente");

                foreach (var elemento in lista)
                {
                    var tipoPropiedad = "";

                    if (elemento._TipoPropiedad != null)
                        tipoPropiedad = elemento._TipoPropiedad.Nombre;

                    var cliente = "";

                    if (elemento._Cliente != null)
                        cliente = elemento._Cliente.Nombre + " " + elemento._Cliente.Apellido;

                    sb.AppendLine(
                        elemento.NumeroHabitaciones + ";" +
                        elemento.NumeroBanos + ";" +
                        elemento.Patio + ";" +
                        elemento.Entradas + ";" +
                        elemento.Pisos + ";" +
                        elemento.AnioConstruccion.ToString("dd/MM/yyyy") + ";" +
                        elemento.ValorPropiedad.ToString("N0") + ";" +
                        elemento.ValorArriendo.ToString("N0") + ";" +
                        LimpiarCsv(elemento.Estado) + ";" +
                        LimpiarCsv(tipoPropiedad) + ";" +
                        LimpiarCsv(cliente)
                    );
                }

                var bytes = Encoding.UTF8.GetPreamble()
                    .Concat(Encoding.UTF8.GetBytes(sb.ToString()))
                    .ToArray();

                return File(bytes, "text/csv", "Propiedades.csv");
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
                OnPostBtRefrescar();
                return Page();
            }
        }

        private string LimpiarCsv(string? texto)
        {
            if (texto == null)
                return "";

            return texto
                .Replace(";", ",")
                .Replace("\r", " ")
                .Replace("\n", " ");
        }

    }

}