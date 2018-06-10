<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Visita.aspx.cs" Inherits="Visita" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="Visita">
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
        <%-- <a href="#" class="boton-menu slideout-menu-toggle"><i class="fa fa-bars"></i>Menú</a>   --%>
         <div class="jumbotron jumbotron-fluid">
             <div class="container">
                 <div class="titulo">
                     <h1>Revisión de Visitas</h1>
                     <p class="lead">Revisión y Aprobación de visitas</p>
                 </div>
             </div>
         </div>
         <div class="container row wrap">
             <div class="contenido col-sm">
                
                 <h4>Revisión de Visita</h4>
                  <%--<a href="#"><i class="fa fa-pencil icono cambioIco"></i> Editar</a>--%>
                 <asp:button runat="server" ID="editar" Class="btn btn-info" OnClick="editar_Click" Visible="false" Text="Editar"/>
                 <center>
                   <asp:button runat="server" ID="Button1Visita" CssClass="btn btn-info" OnClick="Button1Visita_Click" Text=" 1" />
                   <asp:Button runat="server" ID="Button2Visita" CssClass="btn btn-info" OnClick="Button2Visita_Click" Text=" 2" />
                   <asp:Button runat="server" ID="Button3Visita" CssClass="btn btn-info" OnClick="Button3Visita_Click" Text=" 3" />
                   <asp:Button runat="server" ID="Button4Visita" CssClass="btn btn-info" OnClick="Button4Visita_Click" Text=" 4" />
                   <label id="NotaR" runat="server" visible="false">Es posible que existan campos que no han sido completados</label>
                    <br /> <br />
                </center
                 <!--Las etiquetas que contienen ID son en las que se desplegara el valor que este guardado en la ase de Datos-->
         
              <div id="uno" runat="server"> 

                 <label class="titulosVisita">Nombre del anfitrion de ZLS:</label>
                 <label id="nfZLS" runat="server" class="datosVisita"></label>                 
                        
                 <label class="titulosVisita">Num de telefono:</label>
                 <label id="NuTelefono" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="NumeroTE" runat="server" Visible="false"></asp:TextBox>
                        
                 <label class="titulosVisita">Fecha:</label>
                 <label id="fecha1" runat="server" class="datosVisita"></label>
                        
                 <label class="titulosVisita">Nombre completo del visitante:</label>
                 <label id="nombre_visitante" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="NombreVisi" runat="server" Visible="false"></asp:TextBox>
                        
                 <label class="titulosVisita">Estatus de ciudadania o pais de residencia permanente:</label>
                 <label id="estatus" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="status" runat="server" Visible="false"></asp:TextBox>
                       
                 <label class="titulosVisita">Identificación verificada:</label>
                 <label id="Identificacion" runat="server" class="datosVisita"></label>
                 <asp:CheckBox ID="Check_Identifi" runat="server" Visible="false"/>

             </div>

             <div id="dos" runat="server" visible="false">
                  
                 <label class="titulosVisita">Nombre de la compania (si hubiera):</label>
                 <label id="compania" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="companiaEdit" runat="server" Visible="false"></asp:TextBox>

                 <label class="titulosVisita">Ciudad:</label>
                 <label id="ciudad" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="ci" runat="server" Visible="false"></asp:TextBox>

                 <label class="titulosVisita">Estado:</label>
                 <label id="estado" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="es" runat="server" Visible="false"></asp:TextBox>

                 <label class="titulosVisita">Fecha (s) de la visita:</label>
                 <label id="fecha_visita" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="fechVisi" runat="server" TextMode="Date" Visible="false"></asp:TextBox>

                 <label class="titulosVisita" runat="server" id="hasta">Hasta:</label>
                 <label id="fecha_limite" runat="server" class="datosVisita"></label>
                 
                 <label class="titulosVisita" runat="server" id="TipoVLabel" visible="false">Tipo de visita</label>
                    <center>
                    <asp:DropDownList ID="TipoVv" runat="server" ClientIDMode="Static" Visible="false" CssClass="DropDownListCSS textBoxHomeUnoUno redondo" onchange="validarCampos()">
                        <asp:ListItem Value=""> </asp:ListItem>
                        <asp:ListItem Value="Anual">Anual</asp:ListItem>
                        <asp:ListItem Value="Permanente">Permanente</asp:ListItem>
                        <asp:ListItem Value="Unica">Unica</asp:ListItem>
                    </asp:DropDownList></center>

             </div>  
                
             <div id="tres" runat="server" visible="false">

                 <label class="titulosVisita">Abierto-Terminado:</label>
                 <label id="abierto" runat="server" class="datosVisita"></label>     

                 <label class="titulosVisita">Explicar propósito de la visita:</label>
                 <label id="proposito" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="propo" runat="server" Visible="false"/> 

                 <label class="titulosVisita">Empleado ZLS a ser visitado:</label>
                 <label id="visitado" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="visitadoEdit" runat="server" Visible="false"></asp:TextBox>

                 <label class="titulosVisita">Area a visitar:</label>
                 <label id="area" runat="server" class="datosVisita"></label>

                 <label ID="labelAdministrativo" runat="server" Visible="false">Administrativo</label>
                 <asp:CheckBox ID="checkAdministrativo" runat="server" Visible="false" />

                 <label ID="labelIngenieria" runat="server" Visible="false">Ingenieria</label>
                 <asp:CheckBox ID="CheckInge" runat="server" Visible="false" />

                 <label ID="labelFabrica" runat="server" Visible="false">Fabrica</label>
                 <asp:CheckBox ID="CheckFabrica" runat="server" Visible="false" />

                 <label ID="labelOtro" runat="server" Visible="false">Otro</label>
                 <asp:TextBox ID="TextOtro" runat="server" Visible="false" />

             </div>

             <div id="cuatro" runat="server" visible="false">

                 <label class="titulosVisita">Exportara la información tecnica controlada <br />sera analizada y/o divulgada?</label>
                 <label id="respuesta" runat="server" class="datosVisita"></label>
                 <asp:CheckBox ID="Divulgar" runat="server" Visible="false"/>

                 <label class="titulosVisita">En caso de ser si, proveer Num. de licencia:</label>
                 <label id="NuLicencia" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="lice" runat="server" Visible="false"></asp:TextBox>

                 <label class="titulosVisita">Fecha de expiración:</label>
                 <label id="expiracion" runat="server" class="datosVisita"></label>
                 <asp:TextBox ID="fechaEx" runat="server" TextMode="Date" Visible="false"></asp:TextBox>

             </div>
                  <center>
                    <asp:Button runat="server" ID="GuardarCambios" Text="Guardar Cambios" OnClick="GuardarCambios_Click" CssClass="btn btn-success" Visible="false"/>
                    <asp:Button runat="server" ID="CancelarCambios" Text="Cancelar Edicion" OnClick="CancelarCambios_Click" CssClass="btn btn-danger" Visible="false"/>
                 </center>
                  <a href="Visitas.aspx"><i class="fa fa-chevron-circle-left icono cambioIco"></i> Regresar</a>
             </div>

             <!--=====================================================================================================================================================================================-->

             <div class="contenido col-sm">
                    <h4>Aprobación de Visita</h4>
                     <asp:Panel ID="AprobadorIngresar" runat="server">
                         <label class="titulosVisitaAp">Nombre del aprobador:</label>
                         <label ID="NomApro" runat="server" class="datosVisita"></label>
             
                         <label class="titulosVisitaAp">Identificar credencial autorizada:</label>
                     <div class="row fila">
                         <div class="col-sm" >
                              <label class="lead">"V" Acompañante requerido</label>
                         </div>
                         <div class="col-sm" >
                              <asp:CheckBox runat="server" ID="AcompananteRequerido" placeholder="" CssClass="CheckVisita"></asp:CheckBox>
                         </div>
                          
                     </div>
                     <div class="row fila">
                         <div class="col-sm">
                             <label class="lead">"A" Vendedor/Contratista aprobado <br /> (Sin acompañante)</label>
                         </div>
                         <div class="col-sm">
                              <asp:CheckBox runat="server" ID="VendedorContratista" ></asp:CheckBox>
                         </div>
                     </div>

                    <div class="row fila">
                         <div class="col-sm">
                              <label class="lead">"FN" Extranjero, acompañante requerido</label>
                         </div>
                         <div class="col-sm">
                              <asp:CheckBox runat="server" ID="Extranjero"></asp:CheckBox> 
                         </div>
                     </div>


                           <label class="titulosVisitaAp">Otro:</label>
                           <asp:Textbox runat="server" ID="Otro" MaxLength="50"></asp:Textbox>

                         <label class="titulosVisitaAp">Comentarios:</label>

                         <center>
                         <asp:Textbox runat="server" ID="Comentarios" TextMode="MultiLine" style="resize:none"></asp:Textbox>
                         </center>

                         <br /><br />
                      </asp:Panel>
                     
                 
                     <asp:Panel ID="AprobarConsulta" runat="server" Visible="false">
                         <label class="titulosVisitaAp">Nombre del aprobador:</label>
                         <label ID="NombreAprob" runat="server" class="textBoxCSSVisita"></label>
                         <label id="AsignarAprobar" runat="server" class="textBoxCSSVisita" style="color: red" visible="false">SE ASIGNARA APROBADOR</label>
                         
                         <label class="titulosVisitaAp">Fecha de investigacion:</label>
                         <label ID="fechain" runat="server" class="textBoxCSSVisita"></label>

                         <label class="titulosVisitaAp" id="credencial" runat="server">Identificar credencial autorizada:</label>
                         <label id="IdentificacionVerific" runat="server" class="textBoxCSSVisita"></label>                         

                         <label class="titulosVisitaAp">Comentarios:</label>
                         <label id="comen" runat="server" class="textBoxCSSVisita"></label>

                     </asp:Panel>
                 <!--Esta parte solo se desplegara cuando el usuario que ingrese sea un Aprobador y que no sea su misma requisicion-->
                 
                 <asp:Panel ID="rechazarAceptar" runat="server">
                     <center>
                         <asp:Button ID="Aceptar" CssClass="btn btn-success" runat="server" Text="Aceptar" Visible="false" OnClick="Aceptar_Click"></asp:Button>
                         <asp:Button ID="Rechazar" CssClass="btn btn-danger" runat="server" Text="Rechazar" Visible="false" OnClick="Rechazar_Click"></asp:Button>
                     </center>
                 </asp:Panel>

             </div>

         </div>

     </section>

</asp:Content>

