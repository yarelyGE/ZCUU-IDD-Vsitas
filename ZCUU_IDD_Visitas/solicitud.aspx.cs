using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Net.Mail;

public partial class solicitud : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Nombre"] == null)
        {
            Response.Redirect("login.aspx");

        }

        requisitor.Value = Session["Nombre"].ToString();
        departamento.Value = Session["Departamento"].ToString();

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string folio = "";

        string connection = ConfigurationManager.ConnectionStrings["SSHFaro"].ConnectionString;
        using (SqlConnection conexion = new SqlConnection(connection))
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand("Generador_Folios", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("departamento", departamento.Value);
            cmd.Parameters.Add("@contador", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@folio", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            SqlDataReader dr = cmd.ExecuteReader();
            folio = Convert.ToString(cmd.Parameters["@folio"].Value);
            int contador = Convert.ToInt32(cmd.Parameters["@contador"].Value);

            conexion.Close();

        }
        var prioridad = "";

        if (departamento.Value == "Produccion")
        {
            prioridad = "Alta";
        }
        else
        {
            prioridad = "Baja";
        }

        string con = ConfigurationManager.ConnectionStrings["SSHFaro"].ConnectionString;
        SqlConnection conexion1 = new SqlConnection(con);
        conexion1.Open();

        SqlCommand enviarSolicitud = new SqlCommand("INSERT INTO solicitudes (folio, requisitor, departamento, num_parte, wo, tipo_peticion, dimensionado, dimensiones, reporte, status, fecha_solicitud, prioridad, email, revision, apu, estimado_hrs) VALUES (@folio, @requisitor, @departamento, @num_parte, @wo, @tipo_peticion, @dimensionado, @dimensiones, @reporte, @status, @fecha_solicitud, @prioridad, @email, @revision,@apu, @estimado_hrs)", conexion1);

        enviarSolicitud.CommandType = CommandType.Text;
        enviarSolicitud.Parameters.AddWithValue("@folio", folio);
        enviarSolicitud.Parameters.AddWithValue("@requisitor", requisitor.Value);
        enviarSolicitud.Parameters.AddWithValue("@departamento", departamento.Value);
        enviarSolicitud.Parameters.AddWithValue("@num_parte", n_parte.Value);
        enviarSolicitud.Parameters.AddWithValue("@wo", wo.Value);
        enviarSolicitud.Parameters.AddWithValue("@tipo_peticion", tipo.Value);
        enviarSolicitud.Parameters.AddWithValue("@dimensionado", dimensionado.Value);
        enviarSolicitud.Parameters.AddWithValue("@dimensiones", dimensiones.Value);
        enviarSolicitud.Parameters.AddWithValue("@reporte", reporte.Value);
        enviarSolicitud.Parameters.AddWithValue("@status", "New");
        enviarSolicitud.Parameters.AddWithValue("@fecha_solicitud", DateTime.Now.ToString("M/d/yyyy"));
        enviarSolicitud.Parameters.AddWithValue("@prioridad", prioridad);
        enviarSolicitud.Parameters.AddWithValue("@email", Session["Email"].ToString());
        enviarSolicitud.Parameters.AddWithValue("@revision", revision.Value);
        enviarSolicitud.Parameters.AddWithValue("@apu", apu.Value);
        enviarSolicitud.Parameters.AddWithValue("@estimado_hrs", "0");
        enviarSolicitud.ExecuteNonQuery();
        conexion1.Close();

        //correos ===========================================//

        string cuerpoMail = "Se ha solicitado una nueva Medición \n";
        cuerpoMail = cuerpoMail + "\n";
        cuerpoMail = cuerpoMail + "Fecha: " + DateTime.Now.ToString("M/d/yyyy") + "\n";
        cuerpoMail = cuerpoMail + "Folio: " + folio + "\n";
        cuerpoMail = cuerpoMail + "Requisitor: " + requisitor.Value + "\n";
        cuerpoMail = cuerpoMail + "No. Parte: " + n_parte.Value + "\n";
        cuerpoMail = cuerpoMail + "Revisión: " + revision.Value + "\n";
        cuerpoMail = cuerpoMail + "WO: " + wo.Value + "\n";
        cuerpoMail = cuerpoMail + "APU: " + apu.Value + "\n";
        cuerpoMail = cuerpoMail + "Tipo de Petición: " + tipo.Value + "\n";
        cuerpoMail = cuerpoMail + "Dimensionado: " + dimensionado.Value + "\n";
        cuerpoMail = cuerpoMail + "Dimensiones: " + dimensiones.Value + "\n";
        cuerpoMail = cuerpoMail + "Reporte: " + reporte.Value + "\n";
        cuerpoMail = cuerpoMail + "Prioridad: " + prioridad + "\n";
        cuerpoMail = cuerpoMail + "\n";
        cuerpoMail = cuerpoMail + "\n";
        cuerpoMail = cuerpoMail + "Link: http://schi-iis1zem/ZCUU_SSH_FARO/home.aspx \n";
        conexion1.Open();
        SqlCommand cmdEmail = new SqlCommand("SELECT email FROM usuarios WHERE usuario = '" + requisitor.Value + "' OR departamento = 'FARO'", conexion1);
        SqlDataReader Email = cmdEmail.ExecuteReader();

        List<string> Emaillist = new List<string>();
        while (Email.Read())
        {
            Emaillist.Add(Email["email"].ToString());
        }
        Email.Close();

        foreach (var email in Emaillist)
        {
            MailMessage objMailMessage = new MailMessage();
            System.Net.NetworkCredential objSMTPUserInfo = new System.Net.NetworkCredential();
            SmtpClient objSmtpClient = new SmtpClient();
            try
            {
                objMailMessage.From = new MailAddress("administrator.chihuahua@zodiacaerospace.com");
                objMailMessage.To.Add(new MailAddress(email));
                objMailMessage.Subject = "Nueva Medición " + folio;
                objMailMessage.Body = cuerpoMail;
                objSmtpClient.EnableSsl = false;
                objSmtpClient = new SmtpClient("11.26.16.2"); /// Server IP
                objSMTPUserInfo = new System.Net.NetworkCredential("administrator.chihuahua@zodiacaerospace.com", "Password", "Domain");
                objSmtpClient.Credentials = objSMTPUserInfo;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objMailMessage = null;
                objSMTPUserInfo = null;
                objSmtpClient = null;
            }
        }
    


    //===============================================================================//

        n_parte.Value = null;
        revision.Value = null;
        wo.Value = null;
        tipo.Value = "";
        dimensionado.Value = "";
        dimensiones.Value = "";
        reporte.Value = "";
        apu.Value = "";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Medicion enviada exitosamente!')", true);

    }
}
