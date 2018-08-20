using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VetSoft.Presentation.Reportes
{
    public partial class ReporteCitasPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["ChequeoID"] == null)
                Response.Redirect("~/Home/Index");
            var chequeoIDText = Request.QueryString["ChequeoID"].Trim().ToString();

            if (string.IsNullOrEmpty(chequeoIDText))
            {
                var message = "No se encuentra ID";

                ClientScript.RegisterStartupScript(GetType(), "alerte",
                    $"alert({message});"
                    , true);
            }

            try
            {
                using (var db = new Data.VetSoftDBEntities())
                {
                    int.TryParse(chequeoIDText, out int res);
                    var check = db.Chequeo.FirstOrDefault(x => x.ID.Equals(res));

                    if (check == null)
                    {
                        Response.Redirect("~/Home/Index");
                    }
                    var pacmodel = new Models.PacienteSingleModel(check.Paciente);
                    var props = (new Models.PacienteViewModel(check.Paciente)).Propietarios;
                    var propietario = new Models.PropietarioViewModel();
                    if (props.Count > 0)
                        propietario = props
                            .First(x => x.Tipo.Equals((int)Data.TipoPropietario.Propietario_Actual)).Propietario;
                    PacID.Text = pacmodel.ID.ToString();
                    PacName.Text = pacmodel.FullName;
                    PacChip.Text = pacmodel.Microchip_Licencia;
                    PacFechaI.Text = pacmodel.FechaIngreso.ToShortDateString();
                    PacEsp.Text = pacmodel.Raza.Especie.Nombre;
                    PacRaza.Text = pacmodel.Raza.Nombre;
                    PacPeso.Text = check.Peso.ToString();
                    PacGenero.Text = pacmodel.Genero.Value.ToString();
                    PacColor.Text = pacmodel.Color;
                    PacNac.Text = pacmodel.FechaNac.ToShortDateString();
                    fechaLabel.Text = check.Fecha.ToLongDateString();
                    preDiagText.Text = check.Prediagnostico;

                    if (propietario != null)
                    {
                        PropName.Text = propietario.FullName();
                        PropDir.Text = propietario.Direccion;
                       
                        PropTel.Text = propietario.Telefono;
                    }
                    var query = check.Medicacion.ToList()
                        .Select(x =>
                        {
                            var m = x.Medicamento.Nombre;
                            var t = x.Indicacion;
                            var txt = $"Medicamento> {m}: {t}";
                            return new ListItem
                            {
                                Text= txt
                            };
                        }).ToList();
                    IndicacionesList.DataSource = query;
                    
                    IndicacionesList.DataBind();
                    GoBackID.NavigateUrl = $"../../Chequeos/Paciente/{pacmodel.ID}";
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

        }
    }
}