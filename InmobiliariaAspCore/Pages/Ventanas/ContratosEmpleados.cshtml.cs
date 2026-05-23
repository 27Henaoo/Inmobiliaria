using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class ContratosEmpleadosModel : PageModel
    {
        // Para combos

        private IContratosNegocio? iContratosNegocio;
        public List<Contratos> Contratos { get; set; } = new List<Contratos>();

        private IEmpleadosSectoresNegocio? iEmpleadosSectoresNegocio;
        public List<EmpleadosSectores> EmpleadosSectores { get; set; } = new List<EmpleadosSectores>();

        private IContratosEmpleadosNegocio? iContratosEmpleadosNegocio;

        [BindProperty] public List<ContratosEmpleados>? Lista { get; set; }
        [BindProperty] public ContratosEmpleados? ContratoEmpleado { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ContratosEmpleadosModel()
        {
            iContratosEmpleadosNegocio = new ContratosEmpleadosNegocio();
            iContratosNegocio = new ContratosNegocio();
            iEmpleadosSectoresNegocio = new EmpleadosSectoresNegocio();
        }

        private void CargarCombos()
        {
            Contratos = iContratosNegocio!.Consultar();
            EmpleadosSectores = iEmpleadosSectoresNegocio!.Consultar();
        }

        private void ValidarRelacion()
        {
            if (ContratoEmpleado == null)
                return;

            if (ContratoEmpleado.Contrato == 0)
                throw new Exception("Debe seleccionar un contrato.");

            if (ContratoEmpleado.Empleado == 0)
                throw new Exception("Debe seleccionar un empleado.");

            var relaciones = iContratosEmpleadosNegocio!.Consultar();

            var relacionExistente = relaciones.FirstOrDefault(x => x.Contrato == ContratoEmpleado.Contrato &&
                x.Empleado == ContratoEmpleado.Empleado && x.Id != ContratoEmpleado.Id
            );

            if (relacionExistente != null)
                throw new Exception("Este contrato ya tiene asociado ese empleado.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iContratosEmpleadosNegocio == null)
                    return;

                Lista = iContratosEmpleadosNegocio.Consultar();
                CargarCombos();
                ContratoEmpleado = null;
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

            ContratoEmpleado = new ContratosEmpleados()
            {
                FechaCierre = DateTime.Now,
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                ContratoEmpleado = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (ContratoEmpleado == null)
                    return;

                ValidarRelacion();

                if (ContratoEmpleado.Id == 0)
                    ContratoEmpleado = iContratosEmpleadosNegocio!.Guardar(ContratoEmpleado!);
                else
                    ContratoEmpleado = iContratosEmpleadosNegocio!.Modificar(ContratoEmpleado!);

                if (ContratoEmpleado.Id == 0)
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
                if (ContratoEmpleado == null)
                    return;

                ContratoEmpleado = iContratosEmpleadosNegocio!.Borrar(ContratoEmpleado!);

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

                ContratoEmpleado = Lista!.FirstOrDefault(x => x.Id == data);

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