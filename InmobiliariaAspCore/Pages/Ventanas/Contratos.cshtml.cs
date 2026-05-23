using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class ContratosModel : PageModel
    {
        //Para combos

        private IClientesNegocio? iClientesNegocio;
        public List<Clientes> Clientes { get; set; } = new List<Clientes>();

        private ICompradoresNegocio? iCompradoresNegocio;
        public List<Compradores> Compradores { get; set; } = new List<Compradores>();

        private IPropiedadesNegocio? iPropiedadesNegocio;
        public List<Propiedades> Propiedades { get; set; } = new List<Propiedades>();

        private IJefesSectoresNegocio? iJefesSectoresNegocio;
        public List<JefesSectores> JefesSectores { get; set; } = new List<JefesSectores>();

        private IContratosNegocio? iContratosNegocio;

        [BindProperty] public List<Contratos>? Lista { get; set; }
        [BindProperty] public Contratos? Contrato { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ContratosModel()
        {
            iContratosNegocio = new ContratosNegocio();
            iClientesNegocio = new ClientesNegocio();
            iCompradoresNegocio = new CompradoresNegocio();
            iPropiedadesNegocio = new PropiedadesNegocio();
            iJefesSectoresNegocio = new JefesSectoresNegocio();
        }

        private void CargarCombos()
        {
            Clientes = iClientesNegocio!.Consultar();
            Compradores = iCompradoresNegocio!.Consultar();
            Propiedades = iPropiedadesNegocio!.Consultar();
            JefesSectores = iJefesSectoresNegocio!.Consultar();
        }

        private void ValidarCombos()
        {
            if (Contrato == null)
                return;

            if (Contrato.Cliente == 0)
                throw new Exception("Debe seleccionar un cliente.");

            if (Contrato.Propiedad == 0)
                throw new Exception("Debe seleccionar una propiedad.");

            if (Contrato.Comprador == 0)
                throw new Exception("Debe seleccionar un comprador.");

            if (Contrato.JefeSector == 0)
                throw new Exception("Debe seleccionar un jefe de sector.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iContratosNegocio == null)
                    return;

                Lista = iContratosNegocio.Consultar();
                CargarCombos();
                Contrato = null;
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

            Contrato = new Contratos()
            {
                FechaContrato = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                Contrato = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (Contrato == null)
                    return;

                ValidarCombos();

                if (Contrato.Id == 0)
                    Contrato = iContratosNegocio!.Guardar(Contrato!);
                else
                    Contrato = iContratosNegocio!.Modificar(Contrato!);

                if (Contrato.Id == 0)
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
                if (Contrato == null)
                    return;

                Contrato = iContratosNegocio!.Borrar(Contrato!);

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

                Contrato = Lista!.FirstOrDefault(x => x.Id == data);

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