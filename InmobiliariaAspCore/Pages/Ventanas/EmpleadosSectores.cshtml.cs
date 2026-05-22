using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class EmpleadosSectoresModel : PageModel
    {
        //Para combos

        private IEstadosCivilesNegocio? iEstadosCivilesNegocio;
        public List<EstadosCiviles> EstadosCiviles { get; set; } = new List<EstadosCiviles>();

        private INacionalidadesNegocio? iNacionalidadesNegocio;
        public List<Nacionalidades> Nacionalidades { get; set; } = new List<Nacionalidades>();

        private ISectoresNegocio? iSectoresNegocio;
        public List<Sectores> Sectores { get; set; } = new List<Sectores>();

        private ITiposContratosNegocio? iTiposContratosNegocio;
        public List<TiposContratos> TiposContratos { get; set; } = new List<TiposContratos>();

        private IJefesSectoresNegocio? iJefesSectoresNegocio;
        public List<JefesSectores> JefesSectores { get; set; } = new List<JefesSectores>();

        private IEmpleadosSectoresNegocio? iEmpleadosSectoresNegocio;

        [BindProperty] public List<EmpleadosSectores>? Lista { get; set; }
        [BindProperty] public EmpleadosSectores? EmpleadoSector { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public EmpleadosSectoresModel()
        {
            iEmpleadosSectoresNegocio = new EmpleadosSectoresNegocio();
            iEstadosCivilesNegocio = new EstadosCivilesNegocio();
            iNacionalidadesNegocio = new NacionalidadesNegocio();
            iSectoresNegocio = new SectoresNegocio();
            iTiposContratosNegocio = new TiposContratosNegocio();
            iJefesSectoresNegocio = new JefesSectoresNegocio();
        }

        private void CargarCombos()
        {
            EstadosCiviles = iEstadosCivilesNegocio!.Consultar();
            Nacionalidades = iNacionalidadesNegocio!.Consultar();
            Sectores = iSectoresNegocio!.Consultar();
            TiposContratos = iTiposContratosNegocio!.Consultar();
            JefesSectores = iJefesSectoresNegocio!.Consultar();
        }

        //Validar primero que el jefe pertenezca al mismo sector seleccionado
        private void ValidarJefeSector()
        {
            if (EmpleadoSector == null)
                return;

            if (EmpleadoSector.Sector == 0)
                throw new Exception("Debe seleccionar un sector.");

            if (EmpleadoSector.JefeSector == 0)
                throw new Exception("Debe seleccionar un jefe de sector.");

            if (EmpleadoSector.TipoContrato == 0)
                throw new Exception("Debe seleccionar un tipo de contrato.");

            var jefes = iJefesSectoresNegocio!.Consultar();

            var jefe = jefes.FirstOrDefault(x => x.Id == EmpleadoSector.JefeSector);

            if (jefe == null)
                throw new Exception("El jefe de sector seleccionado no existe.");

            if (jefe.Sector != EmpleadoSector.Sector)
                throw new Exception("El jefe seleccionado no pertenece al sector seleccionado.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iEmpleadosSectoresNegocio == null)
                    return;

                Lista = iEmpleadosSectoresNegocio.Consultar();
                CargarCombos();
                EmpleadoSector = null;
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

            EmpleadoSector = new EmpleadosSectores()
            {
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                EmpleadoSector = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (EmpleadoSector == null)
                    return;

                ValidarJefeSector();

                if (EmpleadoSector.Id == 0)
                    EmpleadoSector = iEmpleadosSectoresNegocio!.Guardar(EmpleadoSector!);
                else
                    EmpleadoSector = iEmpleadosSectoresNegocio!.Modificar(EmpleadoSector!);

                if (EmpleadoSector.Id == 0)
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
                if (EmpleadoSector == null)
                    return;

                EmpleadoSector = iEmpleadosSectoresNegocio!.Borrar(EmpleadoSector!);

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

                EmpleadoSector = Lista!.FirstOrDefault(x => x.Id == data);

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