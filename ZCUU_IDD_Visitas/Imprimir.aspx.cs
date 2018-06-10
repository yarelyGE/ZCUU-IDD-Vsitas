using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Imprimir : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void descargar_Click(object sender, EventArgs e)
    {
        string visita = Request.QueryString["IdVisita"];
        Funciones.PDF(visita);
    }

    protected void Cancelar2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Visitas.aspx");
    }
}