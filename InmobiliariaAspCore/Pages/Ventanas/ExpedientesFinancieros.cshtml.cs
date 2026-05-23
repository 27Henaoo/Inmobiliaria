using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._5.Financiero;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class ExpedientesFinancierosModel : PageModel
    {
        // Para combos

        private IClientesNegocio? iClientesNegocio;
        public List<Clientes> Clientes { get; set; } = new List<Clientes>();

        private IExpedientesFinancierosNegocio? iExpedientesFinancierosNegocio;

        [BindProperty] public List<ExpedientesFinancieros>? Lista { get; set; }
        [BindProperty] public ExpedientesFinancieros? ExpedienteFinanciero { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ExpedientesFinancierosModel()
        {
            iExpedientesFinancierosNegocio = new ExpedientesFinancierosNegocio();
            iClientesNegocio = new ClientesNegocio();
        }

        private void CargarCombos()
        {
            Clientes = iClientesNegocio!.Consultar();
        }

        //Valida que un cliente no tenga mas de un expediente financiero.
        private void ValidarClienteExpediente()
        {
            if (ExpedienteFinanciero == null)
                return;

            if (ExpedienteFinanciero.Persona == 0)
                throw new Exception("Debe seleccionar un cliente.");

            var expedientes = iExpedientesFinancierosNegocio!.Consultar();

            var expedienteExistente = expedientes.FirstOrDefault(x => x.Persona == ExpedienteFinanciero.Persona &&
                x.Id != ExpedienteFinanciero.Id
            );

            if (expedienteExistente != null)
                throw new Exception("Este cliente ya tiene un expediente financiero asignado.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iExpedientesFinancierosNegocio == null)
                    return;

                Lista = iExpedientesFinancierosNegocio.Consultar();
                CargarCombos();
                ExpedienteFinanciero = null;
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

            ExpedienteFinanciero = new ExpedientesFinancieros();

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                ExpedienteFinanciero = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (ExpedienteFinanciero == null)
                    return;

                ValidarClienteExpediente();

                if (ExpedienteFinanciero.Id == 0)
                    ExpedienteFinanciero = iExpedientesFinancierosNegocio!.Guardar(ExpedienteFinanciero!);
                else
                    ExpedienteFinanciero = iExpedientesFinancierosNegocio!.Modificar(ExpedienteFinanciero!);

                if (ExpedienteFinanciero.Id == 0)
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
                if (ExpedienteFinanciero == null)
                    return;

                ExpedienteFinanciero = iExpedientesFinancierosNegocio!.Borrar(ExpedienteFinanciero!);

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

                ExpedienteFinanciero = Lista!.FirstOrDefault(x => x.Id == data);

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