using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using WebMatrix.Data;

public partial class Visita : System.Web.UI.Page
{
    private static string idEmpleadoReq;
    private static string nombreReq;
    private static string apellido1Req;
    private static string apellido2Req;

    private static string idEmpleadoAp;
    private static string nombreAp;
    private static string apellido1Ap;
    private static string apellido2Ap;

    protected void Page_Load(object sender, EventArgs e)
    {
        var idVisita = Request["idVisita"];

        if (idVisita == null)
        {
            Response.Redirect("Visitas.aspx");
        }

        if (Session["No.Empleado"] == null)
        {
            Response.Redirect("login.aspx");
        }

        /*OBTENEMOS LOS DATOS DEL REQUISITOR*/
        /*Servicio para obtener el nombre por medio del numero de empleado que este registrado en la base de datos */
        var cgp = Database.Open("IDDVisitas");
        var bdgp = cgp.QuerySingle(@" SELECT [No.Empleado_Requi],nombreEmple,apellido1,apellido2 FROM [Visitantes] WHERE IdVisita = @0", idVisita);
        idEmpleadoReq = bdgp["No.Empleado_Requi"];
        nombreReq = bdgp["nombreEmple"];
        apellido1Req = bdgp["apellido1"];
        apellido2Req = bdgp["apellido2"];
        var abiertoSQL = cgp.QueryValue(@" SELECT Abierto
FROM [Visitantes]
WHERE IdVisita = @0", idVisita);
        nfZLS.InnerHtml = nombreReq + " ";
        nfZLS.InnerHtml += apellido1Req + " ";
        nfZLS.InnerHtml += apellido2Req + " ";
        nfZLS.InnerHtml += "(" + idEmpleadoReq + ")";

        /*OBTENEMOS LOS DATOS DEL APROBADOR*/
        /*Debemos checar si es pendiente o no, si es pendiente se saca de la sesion, si no se saca de la base de datos*/
        var pen = cgp.QueryValue(@" SELECT [Abierto] FROM [Visitantes] WHERE IdVisita = @0", idVisita);
        if (pen)
        {
            idEmpleadoAp = Session["No.Empleado"].ToString();
            nombreAp = "";
            apellido1Ap = "";
            apellido2Ap = "";
            var serYG = new ServiceYG.servicioRHSoapClient();
            DataTable tableEx = serYG.infoEnfermeria(idEmpleadoAp);//Obtiene los datos por medio del IdEmpleado
            if (tableEx.Rows.Count > 0)                         //Se obtiene columnas 
            {
                DataRow[] filas = tableEx.Select();
                foreach (var x in filas)
                {
                    nombreAp = x["nombre"].ToString();
                    apellido1Ap = x["apellido1"].ToString();
                    apellido2Ap = x["apellido2"].ToString();
                }
            }
        }
        else
        {
            var Nopen = cgp.QuerySingle(@" SELECT [No.Empleado_Aprobador], [NombreAprobador], [Apellido1Apro], [Apellido2Apro] FROM [Visitantes] WHERE IdVisita = @0 ", idVisita);
            idEmpleadoAp = Nopen["No.Empleado_Aprobador"];
            nombreAp = Nopen["NombreAprobador"];
            apellido1Ap = Nopen["Apellido1Apro"];
            apellido2Ap = Nopen["Apellido2Apro"];
        }

        //Label edicion
        NomApro.InnerHtml = nombreAp + " ";
        NomApro.InnerHtml += apellido1Ap + " ";
        NomApro.InnerHtml += apellido2Ap + " ";
        NomApro.InnerHtml += "(" + idEmpleadoAp + ")";

        //Label consulta
        NombreAprob.InnerHtml = nombreAp + " ";
        NombreAprob.InnerHtml += apellido1Ap + " ";
        NombreAprob.InnerHtml += apellido2Ap + " ";
        NombreAprob.InnerHtml += "(" + idEmpleadoAp + ")";

        AprobadorIngresar.Visible = false;
        AprobarConsulta.Visible = true;

        if (abiertoSQL)
        {
            if (Session["permisos"].ToString() == "Admin" ||
                (Session["permisos"].ToString() == "Aprobador" &&
                Session["No.Empleado"].ToString() != idEmpleadoReq)
                )
            {
                AprobadorIngresar.Visible = true;
                AprobarConsulta.Visible = false;
                Rechazar.Visible = true;
                Aceptar.Visible = true;
                editar.Visible = true;               
            }
            else
            {
                AsignarAprobar.Visible = true;
                NombreAprob.Visible = false;
            }

        }

        /*****SELECT para traer los datos de la visita sengun sea la seleccionada (esto por meido del IdVisita)*****/
        var cg = Database.Open("IDDVisitas");

        var bdg = cg.QuerySingle(@"SELECT [IdVisita]
      ,[No.Empleado_Requi]
      ,[nombreEmple]
      ,[apellido1]
      ,[apellido2]
      ,[No.Telefono]
      ,[Fecha_requi]
      ,[TipoVisita]
      ,[Nombre_Visitante]
      ,[Estatus_ciudadania]
      ,[Identificacion_verificada]
      ,[Nombre_compania]
      ,[Ciudad]
      ,[Estado]
      ,[Fecha_inicio_Visita]
      ,[Fecha_final_Visita]
      ,[Abierto]
      ,[Proposito_Visita]
      ,[Empleados_Visitados]
      ,[Administrativo]
      ,[Ingenieria]
      ,[Fabrica]
      ,[Otro]
      ,[Exportara_informacion]
      ,COALESCE([No.Licencia_control_exportaciones],'') AS [No.Licencia_control_exportaciones]
      ,COALESCE([Fecha_expiracion_licencia],'') AS [Fecha_expiracion_licencia]
      ,[No.Empleado_Aprobador]
      ,[NombreAprobador]
      ,[Apellido1Apro]
      ,[Apellido2Apro]
      ,[Fecha_investigacion]
      ,[Aprobado]
      ,[Acompanante_requerido]
      ,[Vendedor_contratista]
      ,[Extranjero]
      ,[Otra_credencial]
      ,[Comentarios]
FROM [Visitantes] 
WHERE IdVisita = @0", idVisita);

        if (!IsPostBack)
        {
            NumeroTE.Text = bdg["No.Telefono"];
            NombreVisi.Text = bdg["Nombre_Visitante"];
            status.Text = bdg["Estatus_ciudadania"];
            Check_Identifi.Checked = bdg["Identificacion_verificada"];
            companiaEdit.Text = bdg["Nombre_compania"];
            ci.Text = bdg["Ciudad"];
            es.Text = bdg["Estado"];
            fechVisi.Text = bdg["Fecha_inicio_Visita"].ToString();
            propo.Text = bdg["Proposito_Visita"];
            visitadoEdit.Text = bdg["Empleados_Visitados"];
        }

        NuTelefono.InnerHtml = bdg["No.Telefono"];
        fecha1.InnerHtml = Funciones.returnString(bdg["Fecha_requi"]);
        nombre_visitante.InnerHtml = bdg["Nombre_Visitante"];
        estatus.InnerHtml = bdg["Estatus_ciudadania"];

        if (bdg["Identificacion_verificada"])
        {
            Identificacion.InnerHtml = "Si";
        }
        else
        {
            Identificacion.InnerHtml = "No";
        }

        compania.InnerHtml = bdg["Nombre_compania"];
        ciudad.InnerHtml = bdg["Ciudad"];
        estado.InnerHtml = bdg["Estado"];
        fecha_visita.InnerHtml = Funciones.returnString(bdg["Fecha_inicio_Visita"]);

        if (bdg["Fecha_final_Visita"] == null)
        {
            fecha_limite.InnerHtml = "No aplica";
        }
        else
        {
            fecha_limite.InnerHtml = Funciones.returnString(bdg["Fecha_final_Visita"]);
        }

        if (bdg["Abierto"])
        {
            abierto.InnerHtml = "Si";
        }
        else
        {
            abierto.InnerHtml = "No";
        }

        proposito.InnerHtml = bdg["Proposito_Visita"];
        visitado.InnerHtml = bdg["Empleados_Visitados"];

        // Condicion para el area a visitar segun lo elija el usuario
        if (!IsPostBack)
        {
            if (bdg["Administrativo"])
            {
                area.InnerHtml += " Administrativo " + "<br />";
                checkAdministrativo.Checked = true;
            }
            if (bdg["Ingenieria"])
            {
                area.InnerHtml += " Ingenieria " + "<br />";
                CheckInge.Checked = true;
            }
            if (bdg["Fabrica"])
            {
                area.InnerHtml += " Fabrica " + "<br />";
                CheckFabrica.Checked = true;
            }
            if (bdg["Otro"] != "")
            {
                area.InnerHtml += bdg["Otro"] + "<br />";
                TextOtro.Text = bdg["Otro"];
            }
        }
        /*--------------------------------------------------*/
        if (bdg["Exportara_informacion"])
        {
            respuesta.InnerHtml = "Si";
            Divulgar.Checked = true;
        }
        else
        {
            respuesta.InnerHtml = "No";
        }

        if (bdg["No.Licencia_control_exportaciones"] != null)
        {
            NuLicencia.InnerHtml = "No aplica";
        }
        else
        {
            NuLicencia.InnerHtml = bdg["No.Licencia_control_exportaciones"];
        }
       
        lice.Text = bdg["No.Licencia_control_exportaciones"];

        if (bdg["Fecha_expiracion_licencia"] != null)
        {
            expiracion.InnerHtml = "No aplica";
        }
        else
        {
            expiracion.InnerHtml = Funciones.returnString(bdg["Fecha_expiracion_licencia"]);
        }
        fechaEx.Text = bdg["Fecha_expiracion_licencia"].ToString();

        if (!bdg["Abierto"])
        {
            fechain.InnerHtml = Funciones.returnString(DateTime.Parse(bdg["Fecha_investigacion"].ToString()));

            if (!IsPostBack)
            {
                if (bdg["Acompanante_Requerido"])
                {
                    IdentificacionVerific.InnerHtml += "'V' Acompañante requerido" + "<br />";
                }
                if (bdg["Vendedor_contratista"])
                {
                    IdentificacionVerific.InnerHtml += "'A' Vendedor/Contratista <br /> aprobado (Sin acompañante)" + "<br />";
                }
                if (bdg["Extranjero"])
                {
                    IdentificacionVerific.InnerHtml += "'F' Extranjero, acompañante requerido" + "<br />";
                }
                if (bdg["Otra_credencial"] != "")
                {
                    IdentificacionVerific.InnerHtml += bdg["Otra_credencial"] + "<br />";
                }
            }

            if (!bdg["Aprobado"])
            {
                credencial.Visible = false;
            }
        }
        comen.InnerHtml = bdg["Comentarios"];

    }
    /*========================================================BOTON ACEPTAR====================================================================*/
    protected void Aceptar_Click(object sender, EventArgs e)
    {
        var DBV = Database.Open("IDDVisitas");
        var idVisita = Request["idVisita"];

        /*****UPDATE para agregar los campos que le corresponden a el Aprobador (No.empleado y nombre se obtiene con el servicio)*****/
        var FechaInve = DateTime.Today.ToString("yyyy-MM-dd");
        bool vAR = false;
        if (AcompananteRequerido.Checked)
        {
            vAR = true;
        }

        bool fnVC = false;
        if (VendedorContratista.Checked)
        {
            fnVC = true;
        }

        bool fnE = false;
        if (Extranjero.Checked)
        {
            fnE = true;
        }

        var otroAp = Otro.Text;
        var com = Comentarios.Text;

        DBV.Execute(@"UPDATE [Visitantes] SET 
        [No.Empleado_Aprobador] = @0
        ,[NombreAprobador] = @1 
        ,[Apellido1Apro] = @2
        ,[Apellido2Apro] = @3
        ,[Fecha_investigacion] = @4
        ,[Acompanante_requerido] = @5
        ,[Vendedor_contratista] = @6
        ,[Extranjero] = @7
        ,[Otra_credencial] = @8
        ,[Comentarios] = @9
        ,[Abierto] = @10
        ,[Aprobado] = @11
        WHERE IdVisita = @12", idEmpleadoAp, nombreAp, apellido1Ap, apellido2Ap, FechaInve, vAR, fnVC, fnE,
        otroAp, com, false, true, idVisita);

        var CorreosAceptar = DBV.Query(@" SELECT DISTINCT tabla.Correos, tabla.Num_Empleados FROM
(
SELECT  DISTINCT [Correo] AS Correos, [No.Emple] AS Num_Empleados FROM [Administrativos] 
UNION ALL 
SELECT DISTINCT [email] AS Correos, [No.Empleado] AS Num_Empleados FROM [usuarios] 
where [No.Empleado] = (
SELECT [No.Empleado_Requi] FROM [Visitantes] WHERE IdVisita = @0
)
) AS tabla ", idVisita);
        var asuntoAcep = "Escaneo sobre Control de Exportacion de " + nombreReq + " " + apellido1Req + " " + apellido2Req + " ha sido APROBADO";
        var cuerpoAcep = "Por lo que se le brindara el acceso al edificio en los dias solicitados.";

        foreach (var email in CorreosAceptar)
        {
            var nombre = "";
            var apellido1 = "";
            var apellido2 = "";

            var serYG = new ServiceYG.servicioRHSoapClient();
            DataTable tableEx1 = serYG.infoEnfermeria(email["Num_Empleados"]);//Obtiene los datos por medio del IdEmpleado
            if (tableEx1.Rows.Count > 0) //Se obtiene columnas 
            {
                DataRow[] filas = tableEx1.Select();
                foreach (var x in filas)
                {
                    nombre = x["nombre"].ToString();
                    apellido1 = x["apellido1"].ToString();
                    apellido2 = x["apellido2"].ToString();
                }
            }

            if (nombre != "" && apellido1 != "" && apellido2 != "")
            {
                Funciones.enviarCorreos(
                    email["Correos"],
                    asuntoAcep,
                    cuerpoAcep,
                    nombre + " " + apellido1 + " " + apellido2);
            }
            else
            {
                Funciones.enviarCorreos(
                    email["Correos"],
                    asuntoAcep,
                    cuerpoAcep,
                    email["Num_Empleados"]);
            }
        }
        Response.Redirect("Imprimir.aspx?IdVisita="+idVisita);
    }

    /*========================================================BOTON RECHAZAR====================================================================*/
    protected void Rechazar_Click(object sender, EventArgs e)
    {
        var DBV = Database.Open("IDDVisitas");
        var idVisita = Request["idVisita"];

        //DBV.Execute(@"UPDATE [Visitantes] SET [Aprobado] = 0 WHERE IdVisita = @0", idVisita);
        //DBV.Execute(@"UPDATE [Visitantes] SET [Abierto] = 0 WHERE IdVisita = @0", idVisita);

        bool acom = false;
        bool vend = false;
        bool extr = false;
        var ot = "";
        var comen = Comentarios.Text;
        var FechaInve = DateTime.Today.ToString("yyyy-MM-dd");


        DBV.Execute(@"UPDATE [Visitantes] SET [No.Empleado_Aprobador] = @0,
        [NombreAprobador] = @1
,[Acompanante_requerido] = @2
,[Vendedor_contratista] = @3
,[Extranjero] = @4
,[Otra_credencial] = @5
,[Comentarios] = @6
,[Fecha_investigacion] = @7
,[Aprobado] = @8
,[Abierto] = @9
WHERE IdVisita = @10", idEmpleadoAp, nombreAp, acom, vend, extr, ot, comen, FechaInve, false, false, idVisita);

        var CorreosRechazar = DBV.Query(@"SELECT DISTINCT tabla.Correos, tabla.Num_Empleados FROM
(
SELECT  DISTINCT [Correo] AS Correos, [No.Emple] AS Num_Empleados FROM [Administrativos] 
UNION ALL 
SELECT DISTINCT [email]AS Correos, [No.Empleado] AS Num_Empleados FROM [usuarios] 
where [No.Empleado] = (
SELECT [No.Empleado_Requi] FROM [Visitantes] WHERE IdVisita = @0
)
) AS tabla", idVisita);
        var asuntoRech = "Escaneo sobre Control de Exportacion de " + nombreReq + " " + apellido1Req + " " + apellido2Req + " ha sido RECHAZADO";
        var cuerpoRech = "Una nueva requisicion fue rechazada, favor de revisar los datos correspondientes";

        foreach (var email in CorreosRechazar)
        {
            var nombre = "";
            var apellido1 = "";
            var apellido2 = "";

            var serYG = new ServiceYG.servicioRHSoapClient();
            DataTable tableEx1 = serYG.infoEnfermeria(email["Num_Empleados"]);//Obtiene los datos por medio del IdEmpleado
            if (tableEx1.Rows.Count > 0) //Se obtiene columnas 
            {
                DataRow[] filas = tableEx1.Select();
                foreach (var x in filas)
                {
                    nombre = x["nombre"].ToString();
                    apellido1 = x["apellido1"].ToString();
                    apellido2 = x["apellido2"].ToString();
                }
            }

            if (nombre != "" && apellido1 != "" && apellido2 != "")
            {
                Funciones.enviarCorreos(
                    email["Correos"],
                    asuntoRech,
                    cuerpoRech,
                    nombre + " " + apellido1 + " " + apellido2);
            }
            else
            {
                Funciones.enviarCorreos(
                email["Correos"],
                asuntoRech,
                cuerpoRech,
                email["Num_Empleados"]);
            }
        }
        Response.Redirect("Imprimir.aspx?IdVisita=" + idVisita);
    }

    protected void Button1Visita_Click(object sender, EventArgs e)
    {
        uno.Visible = true;
        dos.Visible = false;
        tres.Visible = false;
        cuatro.Visible = false;
    }

    protected void Button2Visita_Click(object sender, EventArgs e)
    {
        uno.Visible = false;
        dos.Visible = true;
        tres.Visible = false;
        cuatro.Visible = false;
    }

    protected void Button3Visita_Click(object sender, EventArgs e)
    {
        uno.Visible = false;
        dos.Visible = false;
        tres.Visible = true;
        cuatro.Visible = false;
    }

    protected void Button4Visita_Click(object sender, EventArgs e)
    {
        uno.Visible = false;
        dos.Visible = false;
        tres.Visible = false;
        cuatro.Visible = true;
    }

    /*========================================================BOTON EDITAR====================================================================*/
    protected void editar_Click(object sender, EventArgs e)
    {
        /*TextBox*/                         /*Labels*/
        NumeroTE.Visible = true;            NuTelefono.Visible = false;
        NombreVisi.Visible = true;          nombre_visitante.Visible = false;
        status.Visible = true;              estatus.Visible = false;
        Check_Identifi.Visible = true;      Identificacion.Visible = false;
        companiaEdit.Visible = true;        compania.Visible = false;
        ci.Visible = true;                  ciudad.Visible = false;
        es.Visible = true;                  estado.Visible = false;
        fechVisi.Visible = true;            fecha_visita.Visible = false;                                           
         propo.Visible = true;              proposito.Visible = false;       
        visitadoEdit.Visible = true;        visitado.Visible = false;
        Divulgar.Visible = true;            respuesta.Visible = false;
        lice.Visible = true;                NuLicencia.Visible = false;
        fechaEx.Visible = true;             expiracion.Visible = false;
        GuardarCambios.Visible = true;      fecha_limite.Visible = false;
        CancelarCambios.Visible = true;     hasta.Visible = false;
        TipoVLabel.Visible = true;          area.Visible = false;
        TipoVv.Visible = true;
        labelAdministrativo.Visible = true;
        checkAdministrativo.Visible = true;
        labelIngenieria.Visible = true;
        CheckInge.Visible = true;
        labelFabrica.Visible = true;
        CheckFabrica.Visible = true;
        labelOtro.Visible = true;
        TextOtro.Visible = true;
    }

    /*========================================================BOTON GUARDAR EDICION====================================================================*/
    protected void GuardarCambios_Click(object sender, EventArgs e)
    {
        if (NumeroTE.Text != "" || NombreVisi.Text != "" || status.Text != "" || companiaEdit.Text != "" ||
            ci.Text != "" || es.Text != "" || fechVisi.Text != null ||propo.Text != "" || 
            visitadoEdit.Text != "" || lice.Text != "" ||  fechaEx.Text != null){

            var actualizar = Database.Open("IDDVisitas");

            var idvisita = Request["idvisita"];

            var numeroT = NumeroTE.Text;
            var nomVisi = NombreVisi.Text;
            var ESTADO = status.Text;
            bool checkIdentificacion = false;
            if (Check_Identifi.Checked)
            {
                checkIdentificacion = true;
            }
            var compa = companiaEdit.Text; 
            var ciu = ci.Text;
            var est = es.Text;
            var tipoVisitaEdit = TipoVv.SelectedValue;
            DateTime fechavisit = DateTime.Parse(fechVisi.Text);
            DateTime? fechaFinVisita = fechavisit;
            if (TipoVv.SelectedValue == "   Permanente")
            {
                fechaFinVisita = null;
            }
            else if (TipoVv.SelectedValue == "Anual")
            {
                fechaFinVisita = fechavisit.AddYears(1);
            }
            var proposiTO = propo.Text;
            var vitadosE = visitadoEdit.Text;
            bool admini = false;
            if (checkAdministrativo.Checked)
            {
                admini = true;
            }
            bool ingenieria = false;
            if (CheckInge.Checked)
            {
                ingenieria = true;
            }
            bool fabric = false;
            if (CheckInge.Checked)
            {
                fabric = true;
            }
            var otraArea = TextOtro.Text;
            bool respuestaSi = false;
            if (Divulgar.Checked)
            {
                respuestaSi = true;
            }
            var licen = lice.Text;
            DateTime exFecha = DateTime.Parse(fechaEx.Text);
           
            actualizar.Execute(@"UPDATE [Visitantes] SET 
            [No.Telefono] = @0
            ,[Nombre_Visitante] = @1
            ,[Estatus_ciudadania] = @2
            ,[Identificacion_verificada] = @3
            ,[Nombre_compania] = @4
            ,[Ciudad] = @5
            ,[Estado] = @6
            ,[Fecha_inicio_Visita] = @7
            ,[Fecha_final_Visita] = @8
            ,[Proposito_Visita] = @9
            ,[Empleados_Visitados] = @10
            ,[Administrativo] = @11
            ,[Ingenieria]= @12
            ,[Fabrica] = @13
            ,[Otro] = @14
            ,[Exportara_informacion] = @15
            ,[No.Licencia_control_exportaciones] = @16
            ,[Fecha_expiracion_licencia] = @17
            WHERE IdVisita = @18", numeroT, nomVisi, ESTADO, checkIdentificacion, compa, ciu, est, fechavisit, fechaFinVisita,
            proposiTO, vitadosE, admini, ingenieria, fabric, otraArea, respuestaSi, licen, exFecha, idvisita);

            Response.Redirect("Visita.aspx?IdVisita=" + Request["idVisita"]);
        }
        else
        {
            NotaR.Visible = true;           
        }        
    }

    /*========================================================BOTON CANCELAR EDICION==================================================================*/
    protected void CancelarCambios_Click(object sender, EventArgs e)
    {
        Response.Redirect("Visita.aspx?IdVisita=" + Request["idVisita"]);

       /* NumeroTE.Visible = false;               NuTelefono.Visible = true;
        NombreVisi.Visible = false;             nombre_visitante.Visible = true;
        status.Visible = false;                 estatus.Visible = true;
        Check_Identifi.Visible = false;         Identificacion.Visible = true;
        companiaEdit.Visible = false;           compania.Visible = true;
        ci.Visible = false;                     ciudad.Visible = true;
        es.Visible = false;                     estado.Visible = true;
        fechVisi.Visible = false;               fecha_visita.Visible = true;
        propo.Visible = false;                  proposito.Visible = true;
        visitadoEdit.Visible = false;           visitado.Visible = true;
        Divulgar.Visible = false;               respuesta.Visible = true;
        lice.Visible = false;                   NuLicencia.Visible = true;
        fechaEx.Visible = false;                expiracion.Visible = true;
        GuardarCambios.Visible = false;         fecha_limite.Visible = true;
        CancelarCambios.Visible = false;        hasta.Visible = true;
        TipoVLabel.Visible = false;             area.Visible = true;
        TipoVv.Visible = false;
        NotaR.Visible = false;
        labelAdministrativo.Visible = false;
        checkAdministrativo.Visible = false;
        labelIngenieria.Visible = false;
        CheckInge.Visible = false;
        labelFabrica.Visible = false;
        CheckFabrica.Visible = false;
        labelOtro.Visible = false;
        TextOtro.Visible = false;

        NumeroTE.Text = NuTelefono.InnerHtml;
        NombreVisi.Text = nombre_visitante.InnerHtml;
        status.Text = estatus.InnerHtml;
        if (Identificacion.InnerHtml == "Si")
        {
            Check_Identifi.Checked = true;
        }
        else
        {
            Check_Identifi.Checked = false;
        }
        companiaEdit.Text = compania.InnerHtml;
        ci.Text = ciudad.InnerHtml;
        es.Text = estado.InnerHtml;
        fechVisi.Text = bdg["Fecha_inicio_Visita"].ToString();
        propo.Text = bdg["Proposito_Visita"];
        visitadoEdit.Text = bdg["Empleados_Visitados"];*/
    }
}