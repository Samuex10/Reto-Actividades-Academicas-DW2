using System;
using System.Collections.Generic;
using UniCampus.Entities;
using UniCampus.Web.Presenters;
using UniCampus.Web.Views;

namespace UniCampus.Web
{
    /// <summary>Vista (MVP): solo muestra datos y delega las acciones al Presenter.</summary>
    public partial class Actividades : System.Web.UI.Page, IActividadesView
    {
        private ActividadesPresenter _presenter;

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new ActividadesPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) _presenter.Cargar();
        }

        protected void btnGuardar_Click(object sender, EventArgs e) { _presenter.Guardar(); }

        protected void btnLimpiar_Click(object sender, EventArgs e) { LimpiarFormulario(); }

        // ----- IActividadesView -----
        public string Titulo { get { return txtTitulo.Text; } }
        public string Descripcion { get { return txtDescripcion.Text; } }
        public string FechaInicio { get { return txtFechaInicio.Text; } }
        public string FechaFin { get { return txtFechaFin.Text; } }
        public string Lugar { get { return txtLugar.Text; } }
        public string Cupos { get { return txtCupos.Text; } }
        public string Estado { get { return ddlEstado.SelectedValue; } }

        public void MostrarActividades(IList<ActividadDto> actividades)
        {
            gvActividades.DataSource = actividades;
            gvActividades.DataBind();
        }

        public void MostrarMensaje(string mensaje, bool esError)
        {
            // Codificar siempre el texto antes de mostrarlo (prevención de XSS).
            lblMensaje.Text = Server.HtmlEncode(mensaje).Replace(Environment.NewLine, "<br />");
            lblMensaje.CssClass = "mensaje " + (esError ? "error" : "ok");
        }

        public void LimpiarFormulario()
        {
            txtTitulo.Text = txtDescripcion.Text = txtFechaInicio.Text =
                txtFechaFin.Text = txtLugar.Text = txtCupos.Text = string.Empty;
            ddlEstado.SelectedIndex = 0;
        }
    }
}
