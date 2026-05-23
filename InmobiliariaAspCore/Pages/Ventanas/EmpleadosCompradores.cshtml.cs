using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class EmpleadosCompradoresModel : PageModel
    {
        // Para combos

        private ICompradoresNegocio? iCompradoresNegocio;
        public List<Compradores> Compradores { get; set; } = new List<Compradores>();

        private IEmpleadosSectoresNegocio? iEmpleadosSectoresNegocio;
        public List<EmpleadosSectores> EmpleadosSectores { get; set; } = new List<EmpleadosSectores>();

        private IEmpleadosCompradoresNegocio? iEmpleadosCompradoresNegocio;

        [BindProperty] public List<EmpleadosCompradores>? Lista { get; set; }
        [BindProperty] public EmpleadosCompradores? EmpleadoComprador { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public EmpleadosCompradoresModel()
        {
            iEmpleadosCompradoresNegocio = new EmpleadosCompradoresNegocio();
            iCompradoresNegocio = new CompradoresNegocio();
            iEmpleadosSectoresNegocio = new EmpleadosSectoresNegocio();
        }

        private void CargarCombos()
        {
            Compradores = iCompradoresNegocio!.Consultar();
            EmpleadosSectores = iEmpleadosSectoresNegocio!.Consultar();
        }

        //Validacion de la relacion para que no explote
        private void ValidarRelacion()
        {
            if (EmpleadoComprador == null)
                return;

            if (EmpleadoComprador.Comprador == 0)
                throw new Exception("Debe seleccionar un comprador.");

            if (EmpleadoComprador.Empleado == 0)
                throw new Exception("Debe seleccionar un empleado.");

            var relaciones = iEmpleadosCompradoresNegocio!.Consultar();

            var relacionExistente = relaciones.FirstOrDefault(x => x.Comprador == EmpleadoComprador.Comprador &&
                x.Empleado == EmpleadoComprador.Empleado && x.Id != EmpleadoComprador.Id
            );

            if (relacionExistente != null)
                throw new Exception("Este comprador ya tiene asociado ese empleado.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iEmpleadosCompradoresNegocio == null)
                    return;

                Lista = iEmpleadosCompradoresNegocio.Consultar();
                CargarCombos();
                EmpleadoComprador = null;
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

            EmpleadoComprador = new EmpleadosCompradores()
            {
                FechaAsesoramiento = DateTime.Now
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                EmpleadoComprador = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (EmpleadoComprador == null)
                    return;

                ValidarRelacion();

                if (EmpleadoComprador.Id == 0)
                    EmpleadoComprador = iEmpleadosCompradoresNegocio!.Guardar(EmpleadoComprador!);
                else
                    EmpleadoComprador = iEmpleadosCompradoresNegocio!.Modificar(EmpleadoComprador!);

                if (EmpleadoComprador.Id == 0)
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
                if (EmpleadoComprador == null)
                    return;

                EmpleadoComprador = iEmpleadosCompradoresNegocio!.Borrar(EmpleadoComprador!);

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

                EmpleadoComprador = Lista!.FirstOrDefault(x => x.Id == data);

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