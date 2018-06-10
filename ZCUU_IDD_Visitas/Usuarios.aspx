<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="stylesheet" href="css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="Usuarios">
       <%-- <div class="slideout-menu">
	        <h3>Menú <a href="#" class="slideout-menu-toggle">×</a></h3>
	        <ul>
		        <li><a href="NuevaVisita.aspx">Nueva Requisicion<i class="fa fa-angle-right"></i></a></li>
		        <li><a href="Visitas.aspx">Visitas <i class="fa fa-angle-right"></i></a></li>
		        <li id="liUsuario" runat="server"><a href="Usuarios.aspx">Usuarios <i class="fa fa-angle-right"></i></a></li>
                <!--<li><a href="administrador.aspx">Administración <i class="fa fa-angle-right"></i></a></li>-->
                <li><a href="login.aspx">Salir <i class="fa fa-angle-right"></i></a></li>
	        </ul>
        </div><!--.slideout-menu-->

        <a href="#" class="boton-menu slideout-menu-toggle"><i class="fa fa-bars"></i>Menú</a>--%>
        <div class="jumbotron jumbotron-fluid">
            <div class="container">
                <div class="titulo">
                    <h1>Usuarios</h1>
                    <p class="lead">Alta y Baja de usuarios</p>
                </div>
            </div>
        </div>
        
        <div class="container row wrap">
            <div class="contenido col-sm">
                <h4>Nuevo Usuario</h4>
                <label id="ErrorRegisrto" runat="server" visible="false" style="color:red">NUEVO USUARIO YA EXISTENTE</label>
                    <!--Las etiquetas con un id qu comience con "Error son las que despliegan que el campo es obligatorio"-->
                    <label class="titulos">Ingrese nuevo usuario</label>
                    <asp:TextBox ID="Usuario" runat="server" CssClass="textBoxCSS redondo"></asp:TextBox>
                  <center><asp:Label ID="ErrorUsuario" runat="server" CssClass="Error" Visible="false" style="color:red;">Campo obligatorio</asp:Label></center>  

                    <label class="titulos">Seleccione permiso</label>
                    <!--Los items del DropDownList se llenan por medio de la base de datos con la tbala permisos-->
                    <center><asp:DropDownList ID="Permiso" runat="server" CssClass="textBoxCSS DropDownListCSS redondo">
                    
                    </asp:DropDownList></center>  
                   <center><asp:Label id="ErrorPermiso" runat="server" CssClass="Error" Visible="false" Style="color:red">Campo obligatorio</asp:Label></center> 

                    <label class="titulos">Ingrese numero de empleado</label>
                    <asp:TextBox ID="NumeroEm" runat="server" CssClass="textBoxCSS redondo"></asp:TextBox>
                   <center><asp:Label id="ErrorNuEmpl" runat="server" CssClass="Error" Visible="false" style="color:red">Campo obligatorio</asp:Label></center> 
                
                    <label class="titulos">Ingrese E-mail</label>
                    <asp:TextBox ID="email" runat="server" CssClass="textBoxCSS redondo"></asp:TextBox>
                    <center><asp:Label id="ErrorEmail" runat="server" CssClass="Error" Visible="false" style="color:red">Campo obligatorio</asp:Label></center>

                    <center><asp:Button id="guardarUsu" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="guardarUsu_Click"/></center>
                   
               </div>
            <div class="contenido col-sm">
                <h4>Usuarios Activos</h4>
                <!--El GridView se llena por medio de la BD con la tabla usuarios-->
                <asp:GridView ID="UsuariosGrid" runat="server" CssClass="mGrid">

                </asp:GridView>
            </div>
        </div>
    </section>
</asp:Content>