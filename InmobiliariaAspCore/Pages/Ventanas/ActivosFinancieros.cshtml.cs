using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._5.Financiero;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class ActivosFinancierosModel : PageModel
    {
        //Para combos
        private IExpedientesFinancierosNegocio? iExpedientesFinancierosNegocio;
        public List<ExpedientesFinancieros> ExpedientesFinancieros { get; set; } = new List<ExpedientesFinancieros>();

        private IActivosFinancierosNegocio? iActivosFinancierosNegocio;

        [BindProperty] public List<ActivosFinancieros>? Lista { get; set; }
        [BindProperty] public ActivosFinancieros? ActivoFinanciero { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ActivosFinancierosModel()
        {
            iActivosFinancierosNegocio = new ActivosFinancierosNegocio();
            iExpedientesFinancierosNegocio = new ExpedientesFinancierosNegocio();
        }

        private void CargarCombos()
        {
            ExpedientesFinancieros = iExpedientesFinancierosNegocio!.Consultar();
        }

        private void ValidarExpediente()
        {
            if (ActivoFinanciero == null)
                return;

            if (ActivoFinanciero.ExpedienteFinanciero == 0)
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
                if (iActivosFinancierosNegocio == null)
                    return;

                Lista = iActivosFinancierosNegocio.Consultar();
                CargarCombos();
                ActivoFinanciero = null;
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

            ActivoFinanciero = new ActivosFinancieros();

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                ActivoFinanciero = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (ActivoFinanciero == null)
                    return;

                ValidarExpediente();

                if (ActivoFinanciero.Id == 0)
                    ActivoFinanciero = iActivosFinancierosNegocio!.Guardar(ActivoFinanciero!);
                else
                    ActivoFinanciero = iActivosFinancierosNegocio!.Modificar(ActivoFinanciero!);

                if (ActivoFinanciero.Id == 0)
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
                if (ActivoFinanciero == null)
                    return;

                ActivoFinanciero = iActivosFinancierosNegocio!.Borrar(ActivoFinanciero!);

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

                ActivoFinanciero = Lista!.FirstOrDefault(x => x.Id == data);

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