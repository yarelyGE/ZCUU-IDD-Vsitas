using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.Url.LocalPath == "/login.aspx")
        {
            liNuevaVisita.Visible = false;
            liUsuarios.Visible = false;
            liSalir.Visible = false;
        }

        if (Session["Permisos"] != null)
        {
            if (Session["Permisos"].ToString() != "Admin")
            {
                liUsuarios.Visible = false;
            }
        }
    }        
}
