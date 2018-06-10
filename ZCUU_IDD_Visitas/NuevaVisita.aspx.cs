using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using WebMatrix.Data;
using System.Web.UI.WebControls;

public partial class NuevaVisita : System.Web.UI.Page
{
    private static string idEmpleado;
    private static string nombre = "";
    private static string apellido1 = "";
    private static string apellido2 = "";
    private static string correos;

    private string nombreAprobar = "";
    private string apellido1Aprobar = "";
    private string apellido2Aprobar = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["No.Empleado"] == null)
        {
            Response.Redirect("login.aspx");
        }

        //if (Session["permisos"].ToString() != "Admin")
        //{
        //    liUsuarios.Visible = false;
        //}

        idEmpleado = Session["No.Empleado"].ToString();

        var serYG = new ServiceYG.servicioRHSoapClient();
        DataTable tableEx = serYG.infoEnfermeria(idEmpleado);//Obtiene los datos por medio del IdEmpleado
        if (tableEx.Rows.Count > 0) //Se obtiene columnas 
        {
            DataRow[] filas = tableEx.Select();
            foreach (var x in filas)
            {
                nombre = x["nombre"].ToString();
                apellido1 = x["apellido1"].ToString();
                apellido2 = x["apellido2"].ToString();
            }
        }
    }

    protected void enviar_Click(object sender, EventArgs e)
    {
        if (si.Checked && (NoLicencia.Text == "" || expiracion.Text == null))
        {
            Error.Visible = true;
        }
        else {            
            var dbC = Database.Open("IDDVisitas");

            /*Se establece una nueva varaiable para cada uno de los campos, en donse se guardar cada uno de los alores que se ingresen en estos mismo*/
            var telefono = NTelefono.Text;
            var fechaReq = DateTime.Today.ToString("yyy-MM-dd");
            var nombreVis = NombreVisitante.Text;
            var estat = Estatus.Text;
            bool identificV = false;              //indentificacionVerif
            if (IdentificacionVerif.Checked)
            {
                identificV = true;
            }
            var nombreCom = NombreCompania.Text;
            var ciud = Ciudad.Text;
            var esta = Estado.Text;
            var tipoVisi = TipoV.SelectedValue;
            DateTime fechaIn = DateTime.Parse(FechaIniciV.Text);
            DateTime? fechFin = fechaIn;
            if (TipoV.SelectedValue == "Permanente")
            {
                fechFin = null;
            }
            else if (TipoV.SelectedValue == "Anual")
            {
                fechFin = fechaIn.AddYears(1);
            }
            var proposi = Proposito.Text;
            var Visitado = NombreVisitado.Text;
            bool admini = false;                 //Administrativo
            if (Administrativo.Checked)
            {
                admini = true;
            }
            bool inge = false;                  //Ingenieria
            if (Ingenieria.Checked)
            {
                inge = true;
            }
            bool fabri = false;                 //Fabrica
            if (Fabrica.Checked)
            {
                fabri = true;
            }
            var ottro = Otro.Text;
            bool res = false;                   //si
            if (si.Checked)
            {
                res = true;
            }

            string noLice = null;
            string expi = null;
            if (si.Checked)
            {
                noLice = NoLicencia.Text;
                expi = expiracion.Text;
            }

            /*Se ejecuta el Query en donde se hace el insert de las datos a la base de datos, esto se hace con la libreria de WebMatrix
             *INSERT: Se ponene los nombe de los campos en la BD
             *VALUES: Se ponene los espacion de los campos iniciando en "0"
             *PARAMETREOS: Se esciben las variables anteriormente declaradas, estas deben de guardar los valores qu ele usuario ingrese*/

            dbC.Execute(@"INSERT INTO Visitantes 
            ([No.Empleado_Requi], 
            [nombreEmple],
            [apellido1],
            [apellido2],
            [No.Telefono], 
            [Fecha_requi],
            [TipoVisita],
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
            VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24)", idEmpleado, nombre, apellido1,
          apellido2, telefono, fechaReq, tipoVisi, nombreVis, estat, identificV,
          nombreCom, ciud, esta, fechaIn, fechFin, true, proposi, Visitado, admini,
          inge, fabri, ottro, res, noLice, expi);



            var correosAprobadores = dbC.Query(@"SELECT [email], [No.Empleado] FROM [usuarios] WHERE permisos = 'Aprobador'");
            var asuntos = "Nueva requisicion pendiente";
            var cuerpos = "El empleado " + nombre + " " + apellido1 + " " + apellido2 + " (" + idEmpleado + ") registró una nueva requisición de visita, favor de revisarla.";


            foreach (var email in correosAprobadores)
            {

                var serYG = new ServiceYG.servicioRHSoapClient();
                DataTable tableEx = serYG.infoEnfermeria(email["No.Empleado"]);//Obtiene los datos por medio del IdEmpleado
                if (tableEx.Rows.Count > 0) //Se obtiene columnas 
                {
                    DataRow[] filas = tableEx.Select();
                    foreach (var x in filas)
                    {
                        nombreAprobar = x["nombre"].ToString();
                        apellido1Aprobar = x["apellido1"].ToString();
                        apellido2Aprobar = x["apellido2"].ToString();
                    }
                }
                else
                {
                    nombreAprobar = "";
                    apellido1Aprobar = "";
                    apellido2Aprobar = "";
                }

                if (nombreAprobar != "" && apellido1Aprobar != "" && apellido2Aprobar != "")
                {
                    Funciones.enviarCorreos(
                        email["email"],
                        asuntos,
                        cuerpos,
                        nombreAprobar + " " + apellido1Aprobar + " " + apellido2Aprobar);
                }
                else
                {
                    Funciones.enviarCorreos(
                   email["email"],
                   asuntos,
                   cuerpos,
                  email["No.Empleado"]);
                }

            }

            /*Redirige a la pagina de consultas para que se verifica que la visitia fue registrada correctamente*/
            Response.Redirect("Visitas.aspx");
        }
    }
}
