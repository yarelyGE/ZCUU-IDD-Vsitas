using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.DirectoryServices;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetNoStore();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string con = ConfigurationManager.ConnectionStrings["IDDVisitas"].ConnectionString;
        SqlConnection conexion = new SqlConnection(con);
        conexion.Open();

        //Se realiza un conteo por la tabla para verificar que el usuario exista en la BD
        SqlCommand checarExistencia1 = new SqlCommand("SELECT COUNT(*) FROM usuarios WHERE usuario='" + usuario.Value + "'", conexion);
        int contadorChecarExistencia1 = int.Parse(checarExistencia1.ExecuteScalar().ToString());

        conexion.Close();

        /*Se busca el usuario que se ingreso en la base de datos, sino se encuentra se despliega el mensaje*/
        if (contadorChecarExistencia1 == 0)
        {
            errorLogin.InnerText = ("Usuario NO Registrado");
        }

        else
        {
            /*Los datos ingresados se guardan en las siguientes variables para comparar la contrasena*/
            var user = usuario.Value;
            var pwd = password.Value;

            if (getUsuario(user, pwd) == "Invalido")
            {
                errorLogin.InnerText = ("Contraseña Incorrecta");
            }
            /*Si los datos son correctos se hace la consuta de los datos del usuario que ingreso*/
            else if (getUsuario(user, pwd) == "usuario")
            {
                var db = Database.Open("IDDVisitas");
                var sel = db.QuerySingle("SELECT [usuario], [permisos], [email], [No.Empleado] FROM [usuarios] WHERE [usuario] = @0", usuario.Value);

                /*Se guardan los datos del usuario en variables de sesion, esto em caso de utilizar estos datos en otro lado*/
                Session["Usuario"] = sel["usuario"];
                Session["Permisos"] = sel["permisos"];
                Session["Email"] = sel["email"];
                Session["No.Empleado"] = sel["No.empleado"];

                /*Se redirecciona a la pagina correspondiente segun el permiso que tenga el usuario que ingreso*/
                if (Session["permisos"].ToString() == "Aprobador")
                {
                    Response.Redirect("Visitas.aspx");
                }
                if (Session["permisos"].ToString() == "Admin")
                {
                    Response.Redirect("Visitas.aspx");
                }
                if (Session["permisos"].ToString() == "Requi")
                {
                    Response.Redirect("Visitas.aspx");
                }
            }
            /*En caso de que no conruerden los datos se dirige nuevamente al login*/
            else
            {
                Response.Redirect("login.aspx");
            }


        }
    }

    public string getUsuario(string usuario, string pass)
    {

        //obtengo a que dominio pertenece
        DirectoryEntry entrada = default(DirectoryEntry);
        entrada = new DirectoryEntry("LDAP://Chihuahua-dc1", "corp\\" + usuario, pass);

        try
        {
            DirectorySearcher searcher = new DirectorySearcher(entrada);
            SearchResult result = searcher.FindOne();
            if (result == null)
            {
                return "Invalido";
            }
            else
            {
                return "usuario";

            }
        }
        catch (Exception ex)
        {
            return "Invalido";
        }
    }

}