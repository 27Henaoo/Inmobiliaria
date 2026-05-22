using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class JefesSectoresModel : PageModel
    {
        // Para los combos

        private IEstadosCivilesNegocio? iEstadosCivilesNegocio;
        public List<EstadosCiviles> EstadosCiviles { get; set; } = new List<EstadosCiviles>();

        private INacionalidadesNegocio? iNacionalidadesNegocio;
        public List<Nacionalidades> Nacionalidades { get; set; } = new List<Nacionalidades>();

        private IAdministradoresDepartamentosNegocio? iAdministradoresDepartamentosNegocio;
        public List<AdministradoresDepartamentos> AdministradoresDepartamentos { get; set; } = new List<AdministradoresDepartamentos>();

        private ISectoresNegocio? iSectoresNegocio;
        public List<Sectores> Sectores { get; set; } = new List<Sectores>();

        private IJefesSectoresNegocio? iJefesSectoresNegocio;

        [BindProperty] public List<JefesSectores>? Lista { get; set; }
        [BindProperty] public JefesSectores? JefeSector { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public JefesSectoresModel()
        {
            iJefesSectoresNegocio = new JefesSectoresNegocio();
            iEstadosCivilesNegocio = new EstadosCivilesNegocio();
            iNacionalidadesNegocio = new NacionalidadesNegocio();
            iAdministradoresDepartamentosNegocio = new AdministradoresDepartamentosNegocio();
            iSectoresNegocio = new SectoresNegocio();
        }

        private void CargarCombos()
        {
            EstadosCiviles = iEstadosCivilesNegocio!.Consultar();
            Nacionalidades = iNacionalidadesNegocio!.Consultar();
            AdministradoresDepartamentos = iAdministradoresDepartamentosNegocio!.Consultar();
            Sectores = iSectoresNegocio!.Consultar();
        }

        //Valida que un sector no tenga mas de un jefe
        private void ValidarSectorJefe()
        {
            if (JefeSector == null)
                return;

            if (JefeSector.Sector == 0)
                throw new Exception("Debe seleccionar un sector.");

            var jefes = iJefesSectoresNegocio!.Consultar();

            var jefeExistente = jefes.FirstOrDefault(x =>
                x.Sector == JefeSector.Sector &&
                x.Id != JefeSector.Id
            );

            if (jefeExistente != null)
                throw new Exception("Este sector ya tiene un jefe asignado.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iJefesSectoresNegocio == null)
                    return;

                Lista = iJefesSectoresNegocio.Consultar();
                CargarCombos();
                JefeSector = null;
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

            JefeSector = new JefesSectores()
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

                JefeSector = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (JefeSector == null)
                    return;

                //Validacion 1:1 entre Sector y JefeSector
                ValidarSectorJefe();

                if (JefeSector.Id == 0)
                    JefeSector = iJefesSectoresNegocio!.Guardar(JefeSector!);
                else
                    JefeSector = iJefesSectoresNegocio!.Modificar(JefeSector!);

                if (JefeSector.Id == 0)
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
                if (JefeSector == null)
                    return;

                JefeSector = iJefesSectoresNegocio!.Borrar(JefeSector!);

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

                JefeSector = Lista!.FirstOrDefault(x => x.Id == data);

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