<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Imprimir.aspx.cs" Inherits="Imprimir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="jumbotron jumbotron-fluid">
            <div class="container">
                <div class="titulo">
                     <h1>Descargar PDF de Visita</h1>
                    <p class="lead">Descarga en formato PDF</p>
                </div>
            </div>
        </div>
    <div class="container row wrap">
       <div class="contenido col-sm">
       <asp:Button runat="server" ID="descargar" CssClass="btn btn-success" OnClick="descargar_Click" Text="Descargar"/>
       <asp:Button runat="server" ID="Cancelar2" CssClass="btn btn-danger" OnClick="Cancelar2_Click" Text="Cancelar"/>
       </div>
   </div>
</asp:Content>

