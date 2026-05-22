using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class AdministradoresDepartamentosModel : PageModel
    {
        //Para los combos

        private IEstadosCivilesNegocio? iEstadosCivilesNegocio;
        public List<EstadosCiviles> EstadosCiviles { get; set; } = new List<EstadosCiviles>();

        private INacionalidadesNegocio? iNacionalidadesNegocio;
        public List<Nacionalidades> Nacionalidades { get; set; } = new List<Nacionalidades>();

        private IDepartamentosNegocio? iDepartamentosNegocio;
        public List<Departamentos> Departamentos { get; set; } = new List<Departamentos>();

        private IAdministradoresDepartamentosNegocio? iAdministradoresDepartamentosNegocio;

        [BindProperty] public List<AdministradoresDepartamentos>? Lista { get; set; }
        [BindProperty] public AdministradoresDepartamentos? AdministradorDepartamento { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public AdministradoresDepartamentosModel()
        {
            iAdministradoresDepartamentosNegocio = new AdministradoresDepartamentosNegocio();
            iEstadosCivilesNegocio = new EstadosCivilesNegocio();
            iNacionalidadesNegocio = new NacionalidadesNegocio();
            iDepartamentosNegocio = new DepartamentosNegocio();
        }

        private void CargarCombos()
        {
            EstadosCiviles = iEstadosCivilesNegocio!.Consultar();
            Nacionalidades = iNacionalidadesNegocio!.Consultar();
            Departamentos = iDepartamentosNegocio!.Consultar();
        }

        //Valida que un departamento no tenga mas de un administrador
        private void ValidarDepartamentoAdministrador()
        {
            //Si no hay admin no hace nada
            if (AdministradorDepartamento == null)
                return;

            //Si no selecciono departamento msotramos error
            if (AdministradorDepartamento.Departamento == 0)
                throw new Exception("Debe seleccionar un departamento.");

            //Consultamos los administradores ya guardados
            var administradores = iAdministradoresDepartamentosNegocio!.Consultar();

            //Buscamos si ya existe otro admin con el mismo departamento
            var administradorExistente = administradores.FirstOrDefault(x => x.Departamento == AdministradorDepartamento.Departamento &&
                x.Id != AdministradorDepartamento.Id
            );

            //Si encuentra otro no dejamos guardar
            if (administradorExistente != null)
                throw new Exception("Este departamento ya tiene un administrador asignado.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iAdministradoresDepartamentosNegocio == null)
                    return;
                Lista = iAdministradoresDepartamentosNegocio.Consultar();
                CargarCombos();
                AdministradorDepartamento = null;
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
            AdministradorDepartamento = new AdministradoresDepartamentos()
            {
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                AdministradorDepartamento = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (AdministradorDepartamento == null)
                    return;

                //Validamos relación 1:1 entre Departamento y AdministradorDepartamento llamando al metodo que se creo antmente
                ValidarDepartamentoAdministrador();

                if (AdministradorDepartamento.Id == 0)
                    AdministradorDepartamento = iAdministradoresDepartamentosNegocio!.Guardar(AdministradorDepartamento!);
                else
                    AdministradorDepartamento = iAdministradoresDepartamentosNegocio!.Modificar(AdministradorDepartamento!);
                if (AdministradorDepartamento.Id == 0)
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
                if (AdministradorDepartamento == null)
                    return;
                AdministradorDepartamento = iAdministradoresDepartamentosNegocio!.Borrar(AdministradorDepartamento!);
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
                AdministradorDepartamento = Lista!.FirstOrDefault(x => x.Id == data);
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
