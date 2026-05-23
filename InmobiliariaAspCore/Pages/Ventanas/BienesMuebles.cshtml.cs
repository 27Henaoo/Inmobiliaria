using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._5.Financiero;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._6.Patrimonio;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._6.Patrimonio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class BienesMueblesModel : PageModel
    {
        // Para combos

        private IExpedientesFinancierosNegocio? iExpedientesFinancierosNegocio;
        public List<ExpedientesFinancieros> ExpedientesFinancieros { get; set; } = new List<ExpedientesFinancieros>();

        private IBienesMueblesNegocio? iBienesMueblesNegocio;

        [BindProperty] public List<BienesMuebles>? Lista { get; set; }
        [BindProperty] public BienesMuebles? BienMueble { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public BienesMueblesModel()
        {
            iBienesMueblesNegocio = new BienesMueblesNegocio();
            iExpedientesFinancierosNegocio = new ExpedientesFinancierosNegocio();
        }

        private void CargarCombos()
        {
            ExpedientesFinancieros = iExpedientesFinancierosNegocio!.Consultar();
        }

        private void ValidarExpediente()
        {
            if (BienMueble == null)
                return;

            if (BienMueble.ExpedienteFinanciero == 0)
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
                if (iBienesMueblesNegocio == null)
                    return;

                Lista = iBienesMueblesNegocio.Consultar();
                CargarCombos();
                BienMueble = null;
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

            BienMueble = new BienesMuebles()
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

                BienMueble = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (BienMueble == null)
                    return;

                ValidarExpediente();

                if (BienMueble.Id == 0)
                    BienMueble = iBienesMueblesNegocio!.Guardar(BienMueble!);
                else
                    BienMueble = iBienesMueblesNegocio!.Modificar(BienMueble!);

                if (BienMueble.Id == 0)
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
                if (BienMueble == null)
                    return;

                BienMueble = iBienesMueblesNegocio!.Borrar(BienMueble!);

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

                BienMueble = Lista!.FirstOrDefault(x => x.Id == data);

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