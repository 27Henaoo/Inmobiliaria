using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._5.Financiero;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._6.Patrimonio;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._6.Patrimonio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class BienesModel : PageModel
    {
        // Para combos

        private IExpedientesFinancierosNegocio? iExpedientesFinancierosNegocio;
        public List<ExpedientesFinancieros> ExpedientesFinancieros { get; set; } = new List<ExpedientesFinancieros>();

        private IBienesNegocio? iBienesNegocio;

        [BindProperty] public List<Bienes>? Lista { get; set; }
        [BindProperty] public Bienes? Bien { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public BienesModel()
        {
            iBienesNegocio = new BienesNegocio();
            iExpedientesFinancierosNegocio = new ExpedientesFinancierosNegocio();
        }

        private void CargarCombos()
        {
            ExpedientesFinancieros = iExpedientesFinancierosNegocio!.Consultar();
        }

        private void ValidarExpediente()
        {
            if (Bien == null)
                return;

            if (Bien.ExpedienteFinanciero == 0)
                throw new Exception("Debe seleccionar un expediente financiero.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iBienesNegocio == null)
                    return;

                Lista = iBienesNegocio.Consultar();
                CargarCombos();
                Bien = null;
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

            Bien = new Bienes()
            {
                FechaAdquisicion = DateTime.Now
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                Bien = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (Bien == null)
                    return;

                ValidarExpediente();

                if (Bien.Id == 0)
                    Bien = iBienesNegocio!.Guardar(Bien!);
                else
                    Bien = iBienesNegocio!.Modificar(Bien!);

                if (Bien.Id == 0)
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
                if (Bien == null)
                    return;

                Bien = iBienesNegocio!.Borrar(Bien!);

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

                Bien = Lista!.FirstOrDefault(x => x.Id == data);

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
    }
}