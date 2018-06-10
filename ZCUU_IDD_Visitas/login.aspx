<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="login">
        <div class="contenido-login">

            <label>Usuario</label>
            <input type="text" id="usuario" runat="server" autofocus autocomplete="off" required placeholder="Ingresa tu usuario" />
            <label>Contraseña</label>
            <input type="password" id="password" runat="server" autocomplete="off" required placeholder="Ingresa tu contraseña" />
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Ingresar" OnClick="Button1_Click" />

           
            <!--Despliega cunando un susario o contrasena estan mal o no existen-->
            <label id="errorLogin" runat="server"></label>
        </div>
    </section>
</asp:Content>


