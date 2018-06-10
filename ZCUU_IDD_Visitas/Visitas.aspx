<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Visitas.aspx.cs" Inherits="Visitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="ConsultaAprobador">
         <%-- <div class="slideout-menu">
	        <h3>Menú <a href="#" class="slideout-menu-toggle">×</a></h3>
	        <ul>
		        <li><a href="NuevaVisita.aspx">Nueva Requisicion<i class="fa fa-angle-right"></i></a></li>
		        <li><a href="Visitas.aspx">Visitas <i class="fa fa-angle-right"></i></a></li>
		        <li id="liUsuario" runat="server"><a href="Usuarios.aspx">Usuarios <i class="fa fa-angle-right"></i></a></li>
                <!--<li><a href="administrador.aspx">Administración <i class="fa fa-angle-right"></i></a></li>-->
                <li><a href="login.aspx">Salir <i class="fa fa-angle-right"></i></a></li>
	        </ul>
        </div><!--.slideout-menu-->--%>

       
         <div class="jumbotron jumbotron-fluid">
            <%--  <a href="#" class="boton-menu slideout-menu-toggle"><i class="fa fa-bars"></i>Menú</a>--%>
             <div class="container">
                 <div class="titulo">
                 <h1>Visitas</h1>
                 <p class="lead">Vista previa de visitas pendientes y no pendientes.</p>
             </div>
             </div>
             
         </div>
        
        <div class="container wrap">
            <div class="contenido" id="divBotones">
                <center>
                    <nav id="botones" runat="server">
                        <asp:Button ID="Nopend" CssClass="btn btn-primary" runat="server" Text="No pendientes" OnClick="Nopend_Click"> </asp:Button>
                        <asp:Button ID="Pend" CssClass="btn btn-danger" runat="server" Text="Pendientes" OnClick="Pend_Click"></asp:Button>
                        <asp:Button ID="todo" runat="server" CssClass="btn btn-success" Text="Mostrar Todo" OnClick="todo_Click" /> <br /> <br />

                        <center>
                            <asp:DropDownList ID="RequisDrop" runat="server" ClientIDMode="Static" AutoPostBack="true" CssClass="DropDownListCSS textBoxHomeUnoUno redondo">
                                <asp:ListItem Value="todas">Todas las requisiciones</asp:ListItem>
                                <asp:ListItem Value="mias">Mis requisiciones</asp:ListItem>
                                <asp:ListItem Value="ajenas">Requisiciones ajenas</asp:ListItem>
                            </asp:DropDownList>
                        </center><br />
                    </nav>
                </center>
            </div>
            <div class="row">
                 <div class="contenido col-sm"  id="divNP" runat="server">
                 <!--El cuerpo del GridView se forma en el C# al momento de hacer la consulta-->
                <asp:Panel ID="PanelNP" runat="server" class="NoPendientesGV rounded">
                    <h4>No Pendientes</h4>
                    <asp:GridView ID="GridView1" CssClass="mGrid" runat="server">
                         
                    </asp:GridView>
                </asp:Panel>
            </div>
               <div class="contenido col-sm " id="divP" runat="server">
                    <!--El cuerpo del GridView se forma en el C# al momento de hacer la consulta-->
                <asp:Panel ID="PanelP" runat="server" class="PendientesGV rounded">
                      <h4>Pendientes</h4>
                    <asp:GridView ID="GridView2" CssClass="mGrid1" runat="server">
                                              
                    </asp:GridView><br/>
                </asp:Panel>
                
                <asp:Button ID="NuevaRequi" ClientIDMode="Static" runat="server" CssClass="btn btn-info" Text="Nueva requisicion" OnClick="NuevaRequi_Click" />
              </div>
            </div>
           

               
        </div>
    </section>
</asp:Content>

