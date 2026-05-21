using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._2.Sujetos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class DireccionesModel : PageModel
    {
        private IPersonasNegocio? iPersonasNegocio;
        public List<Personas> Personas { get; set; } = new List<Personas>();

        private IDireccionesNegocio? iDireccionesNegocio;
        [BindProperty] public List<Direcciones>? Lista { get; set; }
        [BindProperty] public Direcciones? Direccion { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DireccionesModel()
        {
            iDireccionesNegocio = new DireccionesNegocio();
            iPersonasNegocio = new PersonasNegocio();
        }

        private void CargarCombos()
        {
            Personas = iPersonasNegocio!.Consultar();

        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iDireccionesNegocio == null)
                    return;
                Lista = iDireccionesNegocio.Consultar();
                CargarCombos();
                Direccion = null;
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
            Direccion = new Direcciones()
            {
                //FechaCreacion = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Direccion = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Direccion == null)
                    return;
                if (Direccion.Id == 0)
                    Direccion = iDireccionesNegocio!.Guardar(Direccion!);
                else
                    Direccion = iDireccionesNegocio!.Modificar(Direccion!);
                if (Direccion.Id == 0)
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
                if (Direccion == null)
                    return;
                Direccion = iDireccionesNegocio!.Borrar(Direccion!);
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
                Direccion = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}
