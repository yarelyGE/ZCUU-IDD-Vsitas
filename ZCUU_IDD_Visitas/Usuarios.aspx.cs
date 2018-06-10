using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WebMatrix.Data;
using System.Configuration;


public partial class Usuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["No.Empleado"] == null)
        {
            Response.Redirect("login.aspx");
        }

        if (Session["permisos"].ToString() != "Admin")
        {
            Response.Redirect("login.aspx");
        }

        /*Elimina una celda del GridView, tambien lo elimina ese usuario de la Bd*/
        if (Request["eliminarUsuario"] != null)
        {
            var db = Database.Open("IDDVisitas");
            db.Execute("DELETE FROM [Usuarios] WHERE [Usuario] = @0", Request["eliminarUsuario"]);
            db.Close();
        }

        /*Llena el GridView con los datos de los usuarios que esten registrados en la BD*/
        string usuBD = ConfigurationManager.ConnectionStrings["IDDVisitas"].ConnectionString;
        SqlConnection conexion = new SqlConnection(usuBD);
        conexion.Open();
        string que3 = @"SELECT [usuario] AS [Usuario], [permisos] AS [Permiso], 
        [email] AS [E-mail] FROM [usuarios]";
        SqlCommand conxx = new SqlCommand(que3, conexion);
        SqlDataReader sqlreader = conxx.ExecuteReader();
        UsuariosGrid.DataSource = sqlreader;

        /*Se crea el boton con sus atributos y se va agregadno a cada celda cuandno se ingresa un usuario nuevo*/
        if (!IsPostBack)
        {
            BoundField columnaEliminar = new BoundField();
            columnaEliminar.HeaderText = "";
            UsuariosGrid.Columns.Add(columnaEliminar);
        }

        UsuariosGrid.DataBind();
        sqlreader.Close();

        sqlreader = conxx.ExecuteReader();
        var contador = 0;
        while (sqlreader.Read())
        {
            var celdaEliminar = UsuariosGrid.Rows[contador].Cells[0];
            System.Web.UI.HtmlControls.HtmlButton botonEliminar = new System.Web.UI.HtmlControls.HtmlButton();
            botonEliminar.Attributes.Add("value", sqlreader["Usuario"].ToString());
            botonEliminar.Attributes.Add("name", "eliminarUsuario");
            botonEliminar.Attributes.Add("class", "fa fa-trash buttonSpecial");
            // botonEliminar.InnerHtml = "Eliminar";
            celdaEliminar.Controls.Add(botonEliminar);
            contador++;
        }
        conexion.Close();

        /*Se llenan los Items del DropDowListe con datos de la BD de la tabla permisos*/
        if (!IsPostBack)
        {
            var permiso = Database.Open("IDDVisitas");
            var per = permiso.Query(@"
            SELECT '' AS [permisos], '' AS [descripcion]
            UNION
            SELECT * FROM [permisos]");
            foreach (var x in per)
            {
                ListItem item = new ListItem();
                item.Value = x["permisos"];
                item.Text = x["descripcion"];
                Permiso.Items.Add(item);
            }
            permiso.Close();
        }
    }

    protected void guardarUsu_Click(object sender, EventArgs e)
    {
        /*Validacion de que los campos no se encuentren vacios, y no se guarden en BD mientras esten asi*/
        if (Usuario.Text == "")
        {
            ErrorUsuario.Visible = true;
        }
        else
        {
            ErrorUsuario.Visible = false;
        }
        /*-------------------------------*/
        if (NumeroEm.Text == "")
        {
            ErrorNuEmpl.Visible = true;
        }
        else
        {
            ErrorNuEmpl.Visible = false;
        }
        /*--------------------------------*/
        if (email.Text == "")
        {
            ErrorEmail.Visible = true;
        }
        else
        {
            ErrorEmail.Visible = false;
        }
        /*---------------------------------*/
        if (Permiso.SelectedValue == "")
        {
            ErrorPermiso.Visible = true;
        }
        else
        {
            ErrorPermiso.Visible = false;
        }

        /*Guarda los datos de los campos en  la BD siempre y cuando estos esten llenos, para evitar guardarlos vacios*/
        if (Usuario.Text != "" && NumeroEm.Text != "" && email.Text != "" && Permiso.SelectedValue != "")
        {
            string conex = ConfigurationManager.ConnectionStrings["IDDVisitas"].ConnectionString;
            SqlConnection conexion1 = new SqlConnection(conex);
            conexion1.Open();

            SqlCommand checarUsuarios = new SqlCommand("SELECT COUNT(*) FROM usuarios WHERE usuario ='" + Usuario.Text + "'" + "OR email='" + email.Text + "'" + "OR [No.Empleado]=" + NumeroEm.Text, conexion1);
            int contadorUsuarios = int.Parse(checarUsuarios.ExecuteScalar().ToString());
            conexion1.Close();

            if (contadorUsuarios != 0)
            {
                ErrorRegisrto.Visible = true;
            }
            else
            { 
                var usuN = Database.Open("IDDVisitas");

                var Usu = Usuario.Text;
                var NE = NumeroEm.Text;
                var per = Permiso.SelectedValue;
                var mail = email.Text;

                usuN.Execute(@" INSERT INTO usuarios 
                ([usuario],
                [permisos],
                [email],
                [No.Empleado])
                VALUES (@0, @1, @2, @3)", Usu, per, mail, NE);

                Response.Redirect("Usuarios.aspx");
            }
        }

    }

}