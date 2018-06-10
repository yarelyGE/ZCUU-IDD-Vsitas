using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebMatrix.Data;

public partial class js_home : System.Web.UI.Page
{
    private static string idEmpleado;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["No.Empleado"] == null)
        {
            Response.Redirect("login.aspx");
        }

      if (Session["Permisos"].ToString() != "Admin" &&  
          Session["Permisos"].ToString() != "Requi")
        {
            Response.Redirect("login.aspx"); 
        }

        idEmpleado = Session["No.Empleado"].ToString();
        var nombre = ""; 
        var apellido1 = "";
        var apellido2 = "";

        var serYG = new ServiceYG.servicioRHSoapClient(); 
        DataTable tableEx = serYG.infoEnfermeria(idEmpleado);//Obtiene los datos por medio del IdEmpleado
        if (tableEx.Rows.Count > 0) //Se obtiene columnas 
        {
            DataRow[] filas = tableEx.Select();
            foreach(var x in filas)
            {
                nombre = x["nombre"].ToString();
                apellido1 = x["apellido1"].ToString();
                apellido2 = x["apellido2"].ToString();
            }
        }
    }
    


    protected void enviar_Click(object sender, EventArgs e)
    {
        var dbC = Database.Open("IDDVisitas");

        var telefono = NTelefono.Text;
        var fechaReq = FechaRequi.Text;
        var nombreVis = NombreVisitante.Text;
        var estat = Estatus.Text;
        bool identificV = false; //indentificacionVerif
        if (IdentificacionVerif.Checked)
        {
            identificV = true;
        }
        var nombreCom = NombreCompania.Text;
        var ciud = Ciudad.Text;
        var esta = Estado.Text;
        var fechaIn = FechaIniciV.Text;
        var fechFin = FechaFinalV.Text;
        //bool AT = false; //AbiertoTerminado
        //if (AbiertoTerminado.Checked)
        //{
        //    AT = true;
        //}
        var proposi = Proposito.Text;
        var Visitado = NombreVisitado.Text;
        bool admini = false; //Administrativo
        if (Administrativo.Checked)
        {
            admini = true;
        }
        bool inge = false;//Ingenieria
        if (Ingenieria.Checked)
        {
            inge = true;
        }
        bool fabri = false;//Fabrica
        if (Fabrica.Checked)
        {
            fabri = true;
        }
        var ottro = Otro.Text;
        bool res = false;//si
        if (si.Checked)
        {
            res = true;
        }
        var noLice = NoLicencia.Text;
        var expi = expiracion.Text;

        dbC.Execute(@"INSERT INTO Visitantes 
            ([No.Empleado_Requi], 
            [No.Telefono], 
            [Fecha_requi], 
            [Nombre_Visitante], 
            [Estatus_ciudadania], 
            [Identificacion_verificada], 
            [Nombre_compania], 
            [Ciudad],
            [Estado], 
            [Fecha_inicio_Visita], 
            [Fecha_final_Visita], 
            [Abierto], 
            [Proposito_Visita], 
            [Empleados_Visitados], 
            [Administrativo], 
            [Ingenieria], 
            [Fabrica], 
            [Otro], 
            [Exportara_informacion],
            [No.Licencia_control_exportaciones], 
            [Fecha_expiracion_licencia])
            VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20)",idEmpleado,telefono, fechaReq, nombreVis, estat, identificV,
            nombreCom, ciud, esta, fechaIn, fechFin, true, proposi, Visitado, admini, inge, fabri, ottro, res, noLice, expi);

        /*bool prueba = false;
          if(si.Checked)
          {
              prueba = true;
          }*/

        Response.Redirect("consultaAprobador.aspx");
    }

    protected void si_CheckedChanged(object sender, EventArgs e)
    {
        if (si.Checked)
        {
            panelSi.Visible = true;
        }
        else
        {
            panelSi.Visible = false;
        }
    }
}