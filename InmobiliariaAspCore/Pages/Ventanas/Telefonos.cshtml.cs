using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._2.Sujetos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class TelefonosModel : PageModel
    {
        //Paquete para combos 

        private IPersonasNegocio? iPersonasNegocio;
        public List<Personas> Personas { get; set; } = new List<Personas>();

        private ITelefonosNegocio? iTelefonosNegocio;
        [BindProperty] public List<Telefonos>? Lista { get; set; }
        [BindProperty] public Telefonos? Telefono { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TelefonosModel()
        {
            iTelefonosNegocio = new TelefonosNegocio();
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
                if (iTelefonosNegocio == null)
                    return;
                Lista = iTelefonosNegocio.Consultar();
                CargarCombos();
                Telefono = null;
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
            Telefono = new Telefonos()
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
                Telefono = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Telefono == null)
                    return;
                if (Telefono.Id == 0)
                    Telefono = iTelefonosNegocio!.Guardar(Telefono!);
                else
                    Telefono = iTelefonosNegocio!.Modificar(Telefono!);
                if (Telefono.Id == 0)
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
                if (Telefono == null)
                    return;
                Telefono = iTelefonosNegocio!.Borrar(Telefono!);
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
                Telefono = Lista!.FirstOrDefault(x => x.Id == data);
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

