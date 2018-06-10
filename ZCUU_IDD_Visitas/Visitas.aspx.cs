using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WebMatrix.Data;
using System.Configuration;

public partial class Visitas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["No.Empleado"] == null)
        {
            Response.Redirect("login.aspx");
        }
        //if (Session["permisos"].ToString() != "Admin")
        //{
        //    liUsuario.Visible = false;
        //}

        if (Request["seleccionarVisita"] != null)
        {
            Response.Redirect("Visita.aspx?idVisita=" + Request["seleccionarVisita"]);
        }

        if (!IsPostBack)
        {
            RequisDrop.SelectedValue = "todas";
        }
        if (Session["Permisos"].ToString() == "Requi")
        {
            RequisDrop.Visible = false;
        }
        /*En los siguientes GridView se muestran las visitas que sean registrado filtrandolas entre pendientes y no pendientes
         colocando todas las requisiciones si es aprobador */

        //GridView1(PENDIENTES)
        string con = ConfigurationManager.ConnectionStrings["IDDVisitas"].ConnectionString;
        SqlConnection conexion = new SqlConnection(con);
        conexion.Open();

        string que = @"SET LANGUAGE Spanish
    SELECT IdVisita, 
    ([nombreEmple] + ' ' + [apellido1] + ' ' + [apellido2] + ' (' + [No.Empleado_Requi] + ')') AS [Requisitor],
    [Nombre_Visitante] AS [Visitante],
    [Nombre_compania] AS [Compañia],
    ([Ciudad]+', ' +[Estado])AS [Ciudad-Estado],
    [Proposito_Visita] AS [Proposito de la visita],
    CASE WHEN [Aprobado] = 1
    THEN
    'Aprobado'
    ELSE
    'Rechazado'
    END
    AS [Aprobado],
    DATENAME(DD,Fecha_requi)
    + ' de ' 
    + DATENAME(mm, Fecha_requi) 
    + ' de ' 
    + DATENAME(YYYY,Fecha_requi)
    AS [Fecha] 
    FROM [Visitantes] WHERE [Abierto] = 0  ";

        /*Filtro del DropDownList*/
        if (RequisDrop.SelectedValue == "mias")
        {
            que += " AND [No.Empleado_Requi] = '" + Session["No.Empleado"].ToString() + "'";
        }
        if (RequisDrop.SelectedValue == "ajenas")
        {
            que += " AND [No.Empleado_Requi] != '" + Session["No.Empleado"].ToString() + "'";
        }

        /*Cuando se es requisitor se ejecuta esta condicion para que solamente muuestre las requisiciones que el a realizado esto por medio del numero de empleado*/
        if (Session["permisos"].ToString() == "Requi")
        {
            que += " AND [No.Empleado_Requi] = '" + Session["No.Empleado"].ToString() + "'";
        }

        SqlCommand conx = new SqlCommand(que, conexion);
        SqlDataReader slqDR = conx.ExecuteReader();
        GridView1.DataSource = slqDR;
        if (!IsPostBack)
        {
            BoundField columnaSeleccionar = new BoundField();
            columnaSeleccionar.HeaderText = "";
            GridView1.Columns.Add(columnaSeleccionar);
        }
        GridView1.DataBind();
        slqDR.Close();

        slqDR = conx.ExecuteReader();
        var contador0 = 0;
        while (slqDR.Read())
        {
            var celdaSeleccionar = GridView1.Rows[contador0].Cells[0];
            System.Web.UI.HtmlControls.HtmlButton botonSeleccionar = new System.Web.UI.HtmlControls.HtmlButton();
            botonSeleccionar.Attributes.Add("value", slqDR["IdVisita"].ToString());
            botonSeleccionar.Attributes.Add("name", "seleccionarVisita");
            botonSeleccionar.Attributes.Add("class", "fa fa-eye buttonSpecial");
            //botonSeleccionar.InnerHtml = "Ver";
            celdaSeleccionar.Controls.Add(botonSeleccionar);
            contador0++;
        }
        conexion.Close();

        //GridView2 (NO PENDIENTES)
        string conn = ConfigurationManager.ConnectionStrings["IDDVisitas"].ConnectionString;
        SqlConnection conexion1 = new SqlConnection(con);
        conexion.Open();
        string que2 = @" SET LANGUAGE Spanish
SELECT IdVisita,
([nombreEmple] + ' ' + [apellido1] + ' ' + [apellido2] + ' (' + [No.Empleado_Requi] + ')') AS [Requisitor],
[Nombre_Visitante] AS [Visitante], [Nombre_compania] AS [Compañia], 
([Ciudad]+', ' +[Estado])AS [Ciudad-Estado],
[Proposito_Visita] AS [Proposito de la visita],
DATENAME(DD,Fecha_requi)
+ ' de ' 
+ DATENAME(mm, Fecha_requi) 
+ ' de ' 
+ DATENAME(YYYY,Fecha_requi)
AS [Fecha] FROM [Visitantes] WHERE [Abierto] = 1 ";

        /*Filtro del DropDownList*/
        if (RequisDrop.SelectedValue == "mias")
        {
            que2 += " AND [No.Empleado_Requi] = '" + Session["No.Empleado"].ToString() + "'";
        }
        if (RequisDrop.SelectedValue == "ajenas")
        {
            que2 += " AND [No.Empleado_Requi] != '" + Session["No.Empleado"].ToString() + "'";
        }

        /*Cuando se es requisitor se ejecuta esta condicion para que solamente muuestre las requisiciones que el a realizado esto por medio del numero de empleado*/
        if (Session["permisos"].ToString() == "Requi")
        {
            que2 += " AND [No.Empleado_Requi] = '" + Session["No.Empleado"].ToString() + "'";
        }
        SqlCommand con1 = new SqlCommand(que2, conexion);
        SqlDataReader slqDR1 = con1.ExecuteReader();
        GridView2.DataSource = slqDR1;
        if (!IsPostBack)
        {
            BoundField columnaSeleccionar = new BoundField();
            columnaSeleccionar.HeaderText = "";
            GridView2.Columns.Add(columnaSeleccionar);
        }
        GridView2.DataBind();
        slqDR1.Close();

        slqDR1 = con1.ExecuteReader();
        var contadorNp0 = 0;
        while (slqDR1.Read())
        {
            var celdaSeleccionar = GridView2.Rows[contadorNp0].Cells[0];
            System.Web.UI.HtmlControls.HtmlButton botonSeleccionar = new System.Web.UI.HtmlControls.HtmlButton();
            botonSeleccionar.Attributes.Add("value", slqDR1["IdVisita"].ToString());
            botonSeleccionar.Attributes.Add("name", "seleccionarVisita");
            botonSeleccionar.Attributes.Add("class", "fa fa-eye buttonSpecial");
            //botonSeleccionar.InnerHtml = "Ver";
            celdaSeleccionar.Controls.Add(botonSeleccionar);
            contadorNp0++;
        }

        conexion.Close();

    }
    /*Cuando se da clicik al boton nueva requisicion envia al formulario para relizar un nuevo registro*/
    protected void NuevaRequi_Click(object sender, EventArgs e)
    {
        Response.Redirect("NuevaVisita.aspx");
    }

    /*Estos tres eventos correponden a los botones pendientes y no pendientes, cuando se seleccione uno se muestra ese panel y se oculta el otro*/
    protected void Nopend_Click(object sender, EventArgs e)
    {
        divP.Visible = false;
        divNP.Visible = true;
    }

    protected void Pend_Click(object sender, EventArgs e)
    {
        divNP.Visible = false;
        divP.Visible = true;
    }

    protected void todo_Click(object sender, EventArgs e)
    {
        divNP.Visible = true;
        divP.Visible = true;
    }

}